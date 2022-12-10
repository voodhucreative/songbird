using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace SongBird.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand SignUpCommand { get; }
        public AsyncCommand LoginCommand { get; }

        /*
        public ICommand SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Password == ConfirmPassword)
                        SignUp();
                    else
                        App.Current.MainPage.DisplayAlert("", "Password must be same as above!", "OK");
                });
            }
        }

        */

        public AccountViewModel()
        {
            SignUpCommand = new MvvmHelpers.Commands.AsyncCommand(SignUp);
            LoginCommand = new MvvmHelpers.Commands.AsyncCommand(LogIn);
        }

        async Task LogIn()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                await App.Current.MainPage.DisplayAlert("Login Successful", "You are now logged in", "OK");
                /*
                //call AddUser function which we define in Firebase helper class    
                var user = await Helpers.UserManager.AddUser(Email, Password);
                //AddUser return true if data insert successfuly     
                if (user)
                {
                    await App.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");
                    //Navigate to Wellcom page after successfuly SignUp    
                    //pass user email to welcom page    
                    await App.Current.MainPage.Navigation.PushAsync(new Views.LandingPage());
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");
                */


            }
            await Shell.Current.GoToAsync("LoginPage");
        }

        async Task SignUp()
        {
            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {

                //call AddUser function which we define in Firebase helper class    
                var user = await Helpers.UserManager.AddUser(Email, Password);
                //AddUser return true if data insert successfuly     
                if (user)
                {
                    await App.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");
                    //Navigate to Wellcom page after successfuly SignUp    
                    //pass user email to welcom page    
                    await App.Current.MainPage.Navigation.PushAsync(new Views.LandingPage());
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

            }
        }
    }
}

