using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SongBird.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;
using Command = MvvmHelpers.Commands.Command;

namespace SongBird.ViewModels
{
    public class ChirpsViewModel : BaseViewModel
    {

        public ObservableRangeCollection<Chirp> Chirps { get; }
        public ObservableRangeCollection<Grouping<string, Chirp>> GroupedChirps { get; }

        int count = 0;

        string countDisplay = "";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
        }

        public ICommand ButtonClicked { get; }
        public ICommand CallServer { get; }
        public ICommand DelayLoadMoreCommand { get; }

        public AsyncCommand RefreshCommand { get; }

        string im0 = "https://picsum.photos/200";
        string im = "chirp1.jpeg";

        Chirp selectedChirp;

        public Chirp SelectedChirp
        {
            get => selectedChirp;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "Ok");
                    PlayClip();
                    value = null;
                }
                selectedChirp = value;
                OnPropertyChanged();
            }
        }

        public void PlayClip()
        {

        }

        public ChirpsViewModel()
        {
            Chirps = new ObservableRangeCollection<Chirp>();
            GroupedChirps = new ObservableRangeCollection<Grouping<string, Chirp>>();

            ButtonClicked = new Command(IncrementCount);

            CallServer = new AsyncCommand(CallLiveServer);

            RefreshCommand = new AsyncCommand(Refresh);

            DelayLoadMoreCommand = new AsyncCommand(DelayLoadMore);

            Title = "Browse Chirps";

            //Chirps.Add(new Chirp { Name = "I Love You", Image = im, Artist = new Artist { Name = "John Devaloy" } });
            //Chirps.Add(new Chirp { Name = "Missing You", Image = im, Artist = new Artist { Name = "John Devaloy" } });
            //Chirps.Add(new Chirp { Name = "Wish You Were Here", Image = im, Artist = new Artist { Name = "John Devaloy" } });

            var moreChirps = new List<Chirp>
            {
                new Chirp { Name = "I Love You Too", Image = im },
                new Chirp { Name = "Missing You Too", Image = im },
                new Chirp { Name = "Wish You Were Here Too", Image = im }
            };

            Chirps.AddRange(moreChirps);

            GroupedChirps.Add(new Grouping<string, Chirp>("Top Picks", new List<Chirp>
            {
                new Chirp
                {
                    Clip = new Clip
                    {
                        Name = "Unbreakable",
                        SourceUrl = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3",
                        Artist = new Artist { Name = "Fallen Fields" },
                        Image = im
                    }
                }
                //new Chirp{ Name = "Unbreakable", Image = im, Artist = new Artist { Name = "Fallen Fields" } },
                //new Chirp{ Name = "New Chirp 2", Image = im, Artist = new Artist { Name = "Aubrey Sentinel" } }
            }));

        }

        

        async Task CallLiveServer()
        {
            await Task.Delay(100);

            Chirps.Clear();
            GroupedChirps.Clear();

            Chirps.Add(new Chirp { Name = "I Love You", Image = im });
            Chirps.Add(new Chirp { Name = "Missing You", Image = im });
            Chirps.Add(new Chirp { Name = "Wish You Were Here", Image = im });

            var moreChirps = new List<Chirp>
            {
                new Chirp { Name = "I Love You Too", Image = im },
                new Chirp { Name = "Missing You Too", Image = im },
                new Chirp { Name = "Wish You Were Here Too", Image = im }
            };

            Chirps.AddRange(moreChirps);

            GroupedChirps.Add(new Grouping<string, Chirp>("Top Picks", new List<Chirp>
            {
                //new Chirp{ Name = "Love You More", Image = im, Artist = new Artist { Name = "Daisy Lore" } },
                //new Chirp{ Name = "You're The One", Image = im, Artist = new Artist { Name = "Ellie Cook" } },
                //new Chirp{ Name = "I'm Sorry", Image = im, Artist = new Artist { Name = "Ellie Cook" }  },
                //new Chirp{ Name = "We Belong Together", Image = im, Artist = new Artist { Name = "Ryan Tate" } }
            }));

            GroupedChirps.Add(new Grouping<string, Chirp>("Valentines", new List<Chirp>
            {
                new Chirp
                {
                    Clip = new Clip
                    {
                        Name = "Unbreakable",
                        SourceUrl = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3",
                        Artist = new Artist { Name = "Fallen Fields" },
                        Image = im
                    }
                }
                //new Chirp{ Name = "Crazy About You", Image = im, Artist = new Artist { Name = "Ryan Tate" }  },
                //new Chirp{ Name = "Missing You", Image = im, Artist = new Artist { Name = "Ryan Tate" }  },
                //new Chirp{ Name = "You're The Greatest", Image = im, Artist = new Artist { Name = "Daisy Lore" }  },  
                //new Chirp{ Name = "Say That You Love Me", Image = im, Artist = new Artist { Name = "Daisy Lore" }  },
                //new Chirp{ Name = "Miss Your Kiss", Image = im, Artist = new Artist { Name = "Brook Saunders" } },
                //new Chirp{ Name = "I Need Your Love", Image = im, Artist = new Artist { Name = "Sam Knight" } },
            }));

            GroupedChirps.Add(new Grouping<string, Chirp>("Encouragement", new List<Chirp>
            {
                
                //new Chirp{ Name = "The Best Of Luck", Image = im, Artist = new Artist { Name = "Sam Knight" } },
                //new Chirp{ Name = "Upwards & Onwards ", Image = im, Artist = new Artist { Name = "Sam Knight" } },
                //new Chirp{ Name = "The Sky Is The Limit", Image = im, Artist = new Artist { Name = "Pat Clarke" } },
                //new Chirp{ Name = "Don't Look Back", Image = im, Artist = new Artist { Name = "Ellie Cook" } },
                //new Chirp{ Name = "Reach For The Sky", Image = im, Artist = new Artist { Name = "Dan Hammond" } },
                //new Chirp{ Name = "You're A Star", Image = im, Artist = new Artist { Name = "Brook Saunders" } }
            }));
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

        private void IncrementCount()
        {   
            count++;
            CountDisplay = $"You clicked me {count} times";
        }

    }
}

