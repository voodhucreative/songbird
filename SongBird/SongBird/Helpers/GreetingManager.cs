using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MvvmHelpers;
using Newtonsoft.Json;
using SongBird.Models;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public class GreetingManager : INotifyPropertyChanged
    {
        public bool IsBusy = false;
        public string Message = "";
        public static string DebugImage = "Greeting1.jpeg";
        public string DebugMp3Url = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3";

        ObservableRangeCollection<Greeting> greetings;
        public ObservableRangeCollection<Greeting> Greetings
        {
            get => greetings;
            set
            {
                greetings = value;
                OnPropertyChanged();
            }

        }

        ObservableRangeCollection<Grouping<string, Greeting>> groupedGreetings;
        public ObservableRangeCollection<Grouping<string, Greeting>> GroupedGreetings
        {
            get => groupedGreetings;
            set
            {
                groupedGreetings = value;
                OnPropertyChanged();
            }
        }

        public bool Init()
        {
            Greetings = new ObservableRangeCollection<Greeting>();
            GroupedGreetings = new ObservableRangeCollection<Grouping<string, Greeting>>();

            Greetings.Clear();
            GroupedGreetings.Clear();

            /*
            Device.InvokeOnMainThreadAsync(async () =>
            { 
                await GetAllGreetings();
            });
            */

            GetAllGreetings();

            return true;
        }

        public static bool Create(string name, Clip clip)
        {
            Greeting newGreeting = new Greeting
            {
                Name = name,
                Clip = clip,
                Image = DebugImage
            };
            DataManager.PostObject(DataManager.GREETINGS_TABLE, newGreeting);

            return true;
        }

        public async Task DeleteGreeting(string name)
        {
            var deletedGreeting = (await DataManager.fc.Child(DataManager.GREETINGS_TABLE).OnceAsync<Greeting>()).FirstOrDefault(d => d.Object.Name == name);

            if (deletedGreeting == null)
            {
                Message = "Cannot find selected greeting";
                IsBusy = false;
                return;
            }

            await DataManager.fc.Child(DataManager.GREETINGS_TABLE + "/" + deletedGreeting.Key).DeleteAsync();

            await GetAllGreetings();
            Message = "Greeting deleted successfully";
        }

        public async Task UpdateGreetings(string name)
        {
            Greeting greeting = new Greeting // this will be the selected item
            {
                Name = name,
                Clip = new Clip
                {
                    Name = "Test Clip",
                    Artist = new Artist
                    {
                        Name = "Spubman",
                        Image = DebugImage,
                        Description = "Test Description",
                    },
                    Image = DebugImage,
                    SourceUrl = DebugMp3Url
                },
                Image = DebugImage

            };

            var updateGreeting = (await DataManager.fc.Child(DataManager.GREETINGS_TABLE).OnceAsync<Greeting>()).FirstOrDefault(x => x.Object.Name == name);

            if (updateGreeting == null)
            {
                Message = "Cannot find selected greeting";
                IsBusy = false;
                return;
            }

            await DataManager.fc.Child(DataManager.GREETINGS_TABLE + "/" + updateGreeting.Key).PatchAsync(JsonConvert.SerializeObject(greeting));
            await GetAllGreetings();

            Message = "Product updated successfully";

        }

        public async Task GetAllGreetings()
        {
            IsBusy = true;
            try
            {
                Greetings = new ObservableRangeCollection<Greeting>();
                GroupedGreetings = new ObservableRangeCollection<Grouping<string, Greeting>>();


                if (DataManager.connectFirebase())
                {
                    var lst = (await DataManager.fc.Child(DataManager.GREETINGS_TABLE).OnceAsync<Greeting>()).Select(x =>
                    new Greeting
                    {
                        Name = x.Object.Name,
                        Clip = x.Object.Clip,
                        Image = x.Object.Image
                    }).ToList();

                    Greetings = new ObservableRangeCollection<Greeting>(lst);


                    Grouping<string, Greeting> group = new Grouping<string, Greeting>("Popular",/*"T" + DateTime.Now.TimeOfDay,*/ new List<Greeting>());

                    foreach(Greeting greet in Greetings)
                    {
                        group.Add(greet);
                    }

                    GroupedGreetings.Add(group);
                    


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

