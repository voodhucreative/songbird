using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MvvmHelpers;
using SongBird.Models;

namespace SongBird.Helpers
{
    public class ArtistManager : INotifyPropertyChanged
    {
        public bool IsBusy = false;
        public string Message = "";

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

        public bool Init()
        {
            Artists = new ObservableRangeCollection<Artist>();
            GroupedArtists = new ObservableRangeCollection<Grouping<string, Artist>>();

            Artists.Clear();
            GroupedArtists.Clear();

            /*
            Device.InvokeOnMainThreadAsync(async () =>
            { 
                await GetAllGreetings();
            });
            */

            GetAllArtists();

            return true;
        }

        public static bool Create(string name, string description, string imageUrl)
        {
            Artist newArtist = new Artist
            {
                Name = name,
                Description = description,
                Image = imageUrl
            };

            DataManager.PostObject(DataManager.ARTISTS_TABLE, newArtist);

            return true;
        }

        public async Task GetAllArtists()
        {
            IsBusy = true;
            try
            {
                Artists = new ObservableRangeCollection<Artist>();
                GroupedArtists = new ObservableRangeCollection<Grouping<string, Artist>>();


                if (DataManager.connectFirebase())
                {
                    var lst = (await DataManager.fc.Child(DataManager.ARTISTS_TABLE).OnceAsync<Artist>()).Select(x =>
                    new Artist
                    {
                        Name = x.Object.Name,
                        Description = x.Object.Description,
                        Image = x.Object.Image
                    }).ToList();

                    Artists = new ObservableRangeCollection<Artist>(lst);


                    Grouping<string, Artist> group = new Grouping<string, Artist>("Popular",/*"T" + DateTime.Now.TimeOfDay,*/ new List<Artist>());

                    foreach (Artist artist in Artists)
                    {
                        group.Add(artist);
                    }

                    GroupedArtists.Add(group);
                }
            }
            catch (Exception ex)
            {
                Message = "Error occurred in getting products. Error:" + ex.Message.ToString();
            }
            IsBusy = false;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

