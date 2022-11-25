using System;
using System.Collections.Generic;
using SongBird.Helpers;
using SongBird.Views;
using Xamarin.Forms;

namespace SongBird
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GreetingsPage), typeof(GreetingsPage));
            Routing.RegisterRoute(nameof(ArtistsPage), typeof(ArtistsPage));
            Routing.RegisterRoute(nameof(CreateGreetingPage), typeof(CreateGreetingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));

            SetNavBarIsVisible(this, true);

            Device.InvokeOnMainThreadAsync(async () =>
            {
                await DataManager.PopulateDebugDatabase();
            });

            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}

