using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using SongBird.Helpers;
using Xamarin.Forms;

namespace SongBird.ViewModels
{
    public class CreateGreetingViewModel
    {
        public ICommand NextButtonClicked { get; }
        public ICommand ReplaceImage { get; }
        public ICommand SwapButtonClicked { get; }
        public ICommand PlaySelectedClip { get; }

        public Models.Greeting CurrentGreeting { set;  get; }

        public CreateGreetingViewModel()
		{
            MessagingCenter.Subscribe<GreetingsViewModel, Models.Greeting>(this, "hello", async (sender, param) =>
            {
                Session.CurrentGreeting = JsonConvert.DeserializeObject<Models.Greeting>(JsonConvert.SerializeObject(param));
            });

            CurrentGreeting = Session.CurrentGreeting;

            NextButtonClicked = new MvvmHelpers.Commands.Command(GoToNextSection);
            ReplaceImage = new MvvmHelpers.Commands.Command(PickNewImage);
            SwapButtonClicked = new MvvmHelpers.Commands.Command(GoToClipSelection);
            PlaySelectedClip = new MvvmHelpers.Commands.Command(SelectClip);
        }

        private void PickNewImage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            });
        }

        private void PlayClip(object obj)
        {
            throw new NotImplementedException();
        }

        public void OnImageButtonClicked(object sender, EventArgs e)
        {
            ClipManager.Play(CurrentGreeting.Clip.SourceUrl);
        }

        private void GoToNextSection()
        {
            string targetPage = "GreetingsPage";

            // DEBUG
            Helpers.UserManager.CurrentUser.IsLoggedIn = true;

            if (Helpers.UserManager.CurrentUser.IsLoggedIn)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Tools.ShareText("sharing a thinbg from song bird");
                });
                
                targetPage = "SingleGreetingPage";
            }
            else
            {
                targetPage = "RegisterPage";
                if (Helpers.UserManager.CurrentUser.IsRegistered)
                {
                    targetPage = "LoginPage";
                }
            }

            /*
            MessagingCenter.Send(this, "artistselect", CurrentGreeting.Clip.Artist);
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("SingleArtistPage");
            });
            */

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync(targetPage);
            });
        }

        private void GoToClipSelection()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("///GreetingsPage");
            });
        }

        private void SelectClip()
        {
            ClipManager.Play(CurrentGreeting.Clip.SourceUrl);
        }

    }
}

