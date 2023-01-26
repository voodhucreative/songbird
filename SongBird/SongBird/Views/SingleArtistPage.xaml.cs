using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SongBird.Views
{
    public partial class SingleArtistPage : ContentPage
    {
        public SingleArtistPage()
        {
            InitializeComponent();
        }

        void GoToSpotify(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("https://open.spotify.com/artist/6JgeJ74wRx2YERWutTKfKd?si=UhdKYCuPRNqv_eVFhVDctw"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToBandcamp(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("https://mathowlett.bandcamp.com"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToSoundcloud(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("https://soundcloud.com/mat-howlett"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToYoutube(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("http://www.youtube.com"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToFacebook(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("https://www.facebook.com/mathowlettmusic"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToTwitter(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("http://www.twitter.com"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToTiktok(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("http://www.tiktok.com"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GoToInstagram(object sender, EventArgs args)
        {
            try
            {
                Launcher.OpenAsync(new Uri("http://www.instagram.com"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



