using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using SongBird.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SongBird.ViewModels
{
    public class SingleArtistViewModel
    { 
        public Models.Artist CurrentArtist { set; get; }

        public SingleArtistViewModel()
        {
            MessagingCenter.Subscribe<ArtistsViewModel, Models.Artist>(this, "artistselect", async (sender, param) =>
            {
                Session.CurrentArtist = JsonConvert.DeserializeObject<Models.Artist>(JsonConvert.SerializeObject(param));

            });
            CurrentArtist = Session.CurrentArtist;
        }
    }
}


