using System;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace SongBird.ViewModels
{
	public class CreateGreetingViewModel
	{
        public ICommand NextButtonClicked { get; }

        public CreateGreetingViewModel()
		{
            NextButtonClicked = new MvvmHelpers.Commands.Command(GoToNextSection);
        }

        private void GoToNextSection()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("GreetingsPage");
            });
        }

    }
}

