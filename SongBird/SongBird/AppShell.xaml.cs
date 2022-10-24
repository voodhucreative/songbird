using System;
using System.Collections.Generic;
using SongBird.ViewModels;
using SongBird.Views;
using Xamarin.Forms;

namespace SongBird
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

