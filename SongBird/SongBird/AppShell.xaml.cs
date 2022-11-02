using System;
using System.Collections.Generic;
using SongBird.Views;
using Xamarin.Forms;

namespace SongBird
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ChirpsPage), typeof(ChirpsPage));
            Routing.RegisterRoute(nameof(ArtistsPage), typeof(ArtistsPage));


            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

