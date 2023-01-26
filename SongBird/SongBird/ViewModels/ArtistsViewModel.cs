using System;
using MvvmHelpers;
using SongBird.Models;
using SongBird.Helpers;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Newtonsoft.Json;

namespace SongBird.ViewModels
{
	public class ArtistsViewModel : BaseViewModel
    {
        Artist selectedArtist;
        public Artist SelectedArtist
        {
            get => selectedArtist;
            set
            {
                if (value != null)
                {
                    Session.CurrentArtist = value;
                    MessagingCenter.Send(this, "artistselect", value);
                    value = null;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("/SingleArtistPage");
                    });
                }
                selectedArtist = value;
                OnPropertyChanged();
            }
        }


        ObservableRangeCollection<Artist> artists;
        public ObservableRangeCollection<Artist> Artists
        {
            get => artists;
            set
            {
                artists = value;
                OnPropertyChanged();
            }

        }

        ObservableRangeCollection<Grouping<string, Artist>> groupedArtists;
        public ObservableRangeCollection<Grouping<string, Artist>> GroupedArtists
        {
            get => groupedArtists;
            set
            {
                groupedArtists = value;
                OnPropertyChanged();
            }
        }

        public ICommand CallServer { get; }
        public ICommand DelayLoadMoreCommand { get; }

        public AsyncCommand RefreshCommand { get; }
        ArtistManager ArtistManager;

        public ArtistsViewModel()
		{
            /*
            MessagingCenter.Subscribe<GreetingsViewModel, Models.Greeting>(this, "artistselect", async (sender, param) =>
            {
                Models.Greeting t = JsonConvert.DeserializeObject<Models.Greeting>(JsonConvert.SerializeObject(param));


                if (t.Clip.Description == null)
                {
                    t.Clip.Description = "Some lyrics";
                }

                SelectedArtist.Name = t.Name;

            });*/

            ArtistManager = new ArtistManager();

            UpdateArtists();

            CallServer = new AsyncCommand(CallLiveServer);

            RefreshCommand = new AsyncCommand(Refresh);

            DelayLoadMoreCommand = new AsyncCommand(DelayLoadMore);
        }

        public void UpdateArtists()
        {
            ArtistManager.Init();
            Artists = ArtistManager.Artists;
            GroupedArtists = ArtistManager.GroupedArtists;
            Console.WriteLine("Updated");
        }

        async Task CallLiveServer()
        {
            await Task.Delay(10);
            UpdateArtists();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await CallLiveServer();
            IsBusy = false;
        }

        async Task DelayLoadMore()
        {
            IsBusy = true;
            await CallLiveServer();
            IsBusy = false;
        }


    }
}

