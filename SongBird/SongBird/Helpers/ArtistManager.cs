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

        Artist selectedArtist;
        public Artist SelectedArtist
        {
            get => selectedArtist;
            set
            {
                selectedArtist = value;
                OnPropertyChanged();
            }
        }


        public bool Init()
        {

            SelectedArtist = new Models.Artist
            {
                Name = "Songy Bird",
                Image = "mathowlett1.png",
                Description = ""
            };

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

                    // update any missing artists
                    var lst2 = (await DataManager.fc.Child(DataManager.GREETINGS_TABLE).OnceAsync<Greeting>()).Select(x =>
                    new Greeting
                    {
                        Name = x.Object.Name,
                        Clip = x.Object.Clip,
                        Image = x.Object.Image,
                    }).ToList();

                    ObservableRangeCollection<Greeting> Greetings = new ObservableRangeCollection<Greeting>(lst2);

                    var lst = (await DataManager.fc.Child(DataManager.ARTISTS_TABLE).OnceAsync<Artist>()).Select(x =>
                    new Artist
                    {
                        Name = x.Object.Name,
                        Description = x.Object.Description,
                        Image = x.Object.Image
                    }).ToList();

                    Artists = new ObservableRangeCollection<Artist>(lst);

                    bool AddMissingArtists = true;
                    if (AddMissingArtists)
                    {
                        List<Artist> clipArtists = new List<Artist>();
                        foreach (Greeting greeting in Greetings)
                        {
                            bool exists = false;
                            foreach (Artist artist in Artists)
                            {
                                if (artist.Name == greeting.Clip.Artist.Name)
                                {
                                    exists = true;
                                }
                            }

                            if (!exists)
                            {
                                Console.WriteLine(greeting.Clip.Artist.Name + " needs adding");
                                Create(greeting.Clip.Artist.Name, greeting.Clip.Artist.Description, greeting.Clip.Image);
                                Artists.Add(greeting.Clip.Artist);
                            }
                            else
                            {
                                Console.WriteLine(greeting.Clip.Artist.Name + " already exists");
                            }
                        }
                    }

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

