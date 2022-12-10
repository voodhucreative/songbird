using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Database;
using Firebase.Database.Query;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SongBird.Helpers;
using SongBird.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;
using Command = MvvmHelpers.Commands.Command;

namespace SongBird.ViewModels
{
    public class GreetingsViewModel : BaseViewModel
    {
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

        public AsyncCommand NextButtonClicked { get; }

        public AsyncCommand RefreshCommand { get; }

        GreetingManager GreetingManager;

        Greeting selectedGreeting;
        public Greeting SelectedGreeting
        {
            get => selectedGreeting;
            set
            {
                if (value != null)
                {
                    
                    MessagingCenter.Send(this, "hello", value);

                    //Application.Current.MainPage.DisplayAlert("Selected", value.Name, "Ok");
                    //PlayClip();
                    value = null;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("///CreateGreetingPage");
                    });
                }
                selectedGreeting = value;
                OnPropertyChanged();
            }
        }

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

        public void PlayClip()
        {

        }

        public GreetingsViewModel()
        {
            GreetingManager = new GreetingManager();

            UpdateGreetings();
            
            ButtonClicked = new Command(IncrementCount);

            CallServer = new AsyncCommand(CallLiveServer);

            RefreshCommand = new AsyncCommand(Refresh);

            NextButtonClicked = new MvvmHelpers.Commands.AsyncCommand(TestFunction);

            DelayLoadMoreCommand = new AsyncCommand(DelayLoadMore);

            Title = "Browse Greetings";
 
        }

        async Task TestFunction()
        {
            await Task.Delay(10);

            GreetingManager.Create("Test "+DateTime.Now.Millisecond,
                new Clip
                {
                    Name = "Random Song " + DateTime.Now.Second,
                    Description = "Random song, thingy thingy",
                    Artist = new Artist
                    {
                        Name = "Fallen Fields"
                    },
                    Image = StaticData.TEST_IMAGE,
                    SourceUrl = StaticData.TEST_CLIP_URL
                });
            UpdateGreetings();
        }

        public void UpdateGreetings()
        {
            GreetingManager.Init();
            Greetings = GreetingManager.Greetings;
            GroupedGreetings = GreetingManager.GroupedGreetings;
            Console.WriteLine("Updated");
        }


        async Task CallLiveServer()
        {
            await Task.Delay(10);
            UpdateGreetings();


            //await GreetingManager.UpdateGreetings("Supery Clip");

            //await GreetingManager.DeleteGreeting("Funky Clip");
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

