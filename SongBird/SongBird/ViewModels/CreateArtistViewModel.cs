using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers.Commands;
using SongBird.Helpers;
using MvvmHelpers;
using Firebase.Storage;
using System.Diagnostics;

namespace SongBird.ViewModels
{
	public class CreateArtistViewModel : BaseViewModel
	{
        public ICommand AddClipCommand { get; }
        public ICommand CreateArtist { get; }
        public ICommand ReplaceImage { get; }

        private string artistName;
        public string ArtistName
        {
            get { return artistName; }
            set
            {
                artistName = value;
                OnPropertyChanged();
            }
        }

        private string shortBio;
        public string ShortBio
        {
            get { return shortBio; }
            set
            {
                shortBio = value;
                OnPropertyChanged();
            }
        }

        private string longBio;
        public string LongBio
        {
            get { return longBio; }
            set
            {
                longBio = value;
                OnPropertyChanged();
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged();
            }
        }

        private string uploadProgress;
        public string UploadProgress
        {
            get { return uploadProgress; }
            set
            {
                uploadProgress = value;
                OnPropertyChanged();
            }
        }


        public CreateArtistViewModel()
		{
            AddClipCommand = new AsyncCommand(AddClip);
            CreateArtist = new AsyncCommand(Create);
            //ReplaceImage = new MvvmHelpers.Commands.Command(PickNewImage);
            ReplaceImage = new MvvmHelpers.Commands.Command(PickNewAudio);
            UploadProgress = "PROGRESS 0%";
        }


        private async void PickNewImage()
        {
            var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            if (photo == null) return;

            ImageUrl = photo.FullPath;



            // put this lot in datamanager
            string fbStroageBucketUri = "songbirdsounds-ca07b.appspot.com";
            // double check this is the correct url for Firebase Storage
            var task = new FirebaseStorage(fbStroageBucketUri,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                }).Child("ArtistImages").Child(photo.FileName).PutAsync(await photo.OpenReadAsync());

            task.Progress.ProgressChanged += (s, args) =>
            {
                UploadProgress = "PROGRESS " + args.Percentage + "%";
            };

            //task.Progress
            var downloadLink = await task;

        }

        private async void PickNewAudio()
        {
            var file = await Xamarin.Essentials.FilePicker.PickAsync();

            if (file == null) return;

            if (!file.FileName.Contains(".mp3"))
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "PLEASE PICK AN AUDIO FILE", "Ok");
                return;
            }


            // put this lot in datamanager
            string fbStroageBucketUri = "songbirdsounds-ca07b.appspot.com";
            // double check this is the correct url for Firebase Storage
            var task = new FirebaseStorage(fbStroageBucketUri,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                }).Child("ArtistAudio").Child(file.FileName).PutAsync(await file.OpenReadAsync());

            task.Progress.ProgressChanged += (s, args) =>
            {
                UploadProgress = "PROGRESS " + args.Percentage + "%";
            };


            var downloadLink = await task;

        }

        async Task AddClip()
        {
            await Task.Delay(10);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("/CreateClipPage");
                //await Shell.Current.GoToAsync("../../CreateClipPage");
                //await Shell.Current.GoToAsync("/CreateClipPage");
                //await Shell.Current.GoToAsync("Tits");
                
            });
        }

        async Task Create()
        {
            await Task.Delay(10);
            if (ArtistManager.Create(ArtistName, ShortBio, ImageUrl))
            {
                Console.WriteLine("Artist created " + ArtistName); 
            }
            else
            {
                Console.WriteLine("Failed to create " + ArtistName);
            }
        }
    }
}

