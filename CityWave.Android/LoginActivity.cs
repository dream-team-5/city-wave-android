using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Widget;
using CityWave.Api;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CityWave.Android
{
    [Activity(Label = "Log In")]
    public class LoginActivity : Activity
    {
        private EditText _usernameEditText, _passwordEditText;
        private Button _loginButton, _skipButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            _usernameEditText = FindViewById<EditText>(Resource.Id.UsernameEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.PasswordEditText);
            _loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            _skipButton = FindViewById<Button>(Resource.Id.SkipButton);

            _loginButton.Click += LoginButton_Click;
            _skipButton.Click += SkipButton_Click; ;
        }

        private async void SkipButton_Click(object sender, System.EventArgs e)
        {
            _skipButton.Enabled = false;
            _loginButton.Enabled = false;

            var client = await Client.SignUp(OnLoginFailure);

            if (client != null)
                OnLoginSuccess(client);
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            _skipButton.Enabled = false;
            _loginButton.Enabled = false;

            var client = await Client.SignIn(_usernameEditText.Text, _passwordEditText.Text, OnLoginFailure);

            if (client != null)
                OnLoginSuccess(client);
        }

        private void OnLoginFailure(object error)
        {
            if (error is string description)
                Toast.MakeText(this, description, ToastLength.Long).Show();
            else
            {
                var dictionary = ((JObject)error).ToObject<Dictionary<string, string[]>>();

                if (dictionary.TryGetValue("username", out string[] usernameErrors))
                    AddError(_usernameEditText, usernameErrors[0]);

                if (dictionary.TryGetValue("password", out string[] passwordErrors))
                    AddError(_passwordEditText, passwordErrors[0]);
            }

            _skipButton.Enabled = true;
            _loginButton.Enabled = true;

            void AddError(EditText target, string message)
            {
                target.SetError(message, null);
                target.SetHintTextColor(Color.Red);
                target.SetTextColor(Color.Red);

                target.BeforeTextChanged += RemoveError;
            }

            void RemoveError(object sender, TextChangedEventArgs e)
            {
                ((EditText)sender).SetHintTextColor(Color.Argb(255, 122, 122, 122));
                ((EditText)sender).SetTextColor(Color.Black);

                ((EditText)sender).BeforeTextChanged -= RemoveError;
            }
        }

        private void OnLoginSuccess(Client client)
        {
            using (var prefs = new Preferences(this))
                prefs.Token = client.Token;

            StartActivity(typeof(HomeActivity));

            Finish();
        }
    }
}