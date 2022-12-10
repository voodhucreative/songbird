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
        public ICommand SwapButtonClicked { get; }

        public Models.Greeting CurrentGreeting { set;  get; }

        public CreateGreetingViewModel()
		{
            MessagingCenter.Subscribe<GreetingsViewModel, Models.Greeting>(this, "hello", async (sender, param) =>
            {
                Models.Greeting t = JsonConvert.DeserializeObject<Models.Greeting>(JsonConvert.SerializeObject(param));


                if (t.Clip.Description == null)
                {
                    t.Clip.Description = "Some lyrics";
                }

                CurrentGreeting.Name = t.Name;
                CurrentGreeting.Image = t.Image;
                CurrentGreeting.Clip = t.Clip;

                if (CurrentGreeting.Clip.Artist.Image == null)
                {
                    CurrentGreeting.Clip.Artist.Image = StaticData.TEST_IMAGE;
                }

                if (CurrentGreeting.Clip.Artist.Description == null)
                {
                    CurrentGreeting.Clip.Artist.Description = "A fabulous SongBird artist!";
                }
                

            });


            NextButtonClicked = new MvvmHelpers.Commands.Command(GoToNextSection);
            SwapButtonClicked = new MvvmHelpers.Commands.Command(GoToClipSelection);

            CurrentGreeting = new Models.Greeting
            {
                Name = "A Sweet Greet",
                Image = "Greeting1.jpeg",
                Clip = new Models.Clip
                {
                    Artist = new Models.Artist
                    {
                        Name = "Song Bird",
                        Image = StaticData.TEST_IMAGE,
                        Description = "A beautiful singing bird"
                    },
                    Description = "This message comes to say I love you",
                    Name = "A Sweet Song",
                    Image = StaticData.TEST_IMAGE,
                    SourceUrl = StaticData.TEST_CLIP_URL
                }
            };
        }

        private void GoToNextSection()
        {
            string targetPage = "GreetingsPage"; 

            if (Helpers.UserManager.CurrentUser.IsLoggedIn)
            {
                targetPage = "GreetingsPage";
            }
            else
            {
                targetPage = "RegisterPage";
                if (Helpers.UserManager.CurrentUser.IsRegistered)
                {
                    targetPage = "LoginPage";
                }
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync(targetPage);
            });
        }

        private void GoToClipSelection()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("GreetingsPage");
            });
        }

    }
}

