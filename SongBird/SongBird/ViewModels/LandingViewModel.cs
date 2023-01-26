using System;
using Xamarin.Forms;

namespace SongBird.ViewModels
{
	public class LandingViewModel
	{
		public LandingViewModel()
		{
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await Shell.Current.GoToAsync("GreetingsPage");
            });  
        }
	}
}

