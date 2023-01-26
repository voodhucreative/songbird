using System;
using System.Collections.ObjectModel;
using MvvmHelpers;
using Xamarin.Forms;

namespace SongBird.Helpers
{
	public static class HouseKeeper
	{
        static GreetingManager GreetingManager = new GreetingManager();

        public static bool CleanUp()
		{

            GreetingManager.Init();

            return true;
		}

		public static bool UpdateArtists()
		{

			return false;
		}

	}
}

