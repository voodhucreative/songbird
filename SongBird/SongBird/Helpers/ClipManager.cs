using System;
using MvvmHelpers;
using Plugin.SimpleAudioPlayer;
using SongBird.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public static class ClipManager
    {
        public static ObservableRangeCollection<Clip> Clips;
        public static ObservableRangeCollection<Grouping<string, Greeting>> GroupedClips { get; }
        public static ISimpleAudioPlayer AudioPlayer;

        public static bool Init()
        {
            AudioPlayer = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

            return true;
        }

        public static bool Create(string name, string description, string sourceUrl, Artist artist, string imageUrl)
        {
            Clip newClip = new Clip
            {
                Name = name,
                Description = description,
                SourceUrl = sourceUrl,
                Artist = artist,
                Image = imageUrl
            };

            DataManager.PostObject(DataManager.CLIPS_TABLE, newClip);

            return true;
        }

        public static ObservableRangeCollection<Clip> GetClips()
        {
            if (Clips == null)
            {
                Clips = new ObservableRangeCollection<Clip>();
            }

            return Clips;
        }

        public static void Play(string url)
        {
            try
            {
                if (AudioPlayer == null)
                {
                    AudioPlayer = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                }

                AudioPlayer.Load(url);
                AudioPlayer.Play();

                /*MediaElement media = new MediaElement
                {
                    Source = url,
                    ShowsPlaybackControls = true,
                    AutoPlay = true,
                    BackgroundColor = Color.Aqua,
                    HeightRequest = 80,
                    WidthRequest = 120,
                    IsVisible = true
                };

                media.Play();*/

            }
            catch (Exception e)
            {

            }
        }

        public static void Stop()
        {
            try
            {
                if (AudioPlayer == null)
                {
                    AudioPlayer = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                }

                AudioPlayer.Stop();
            }
            catch (Exception e)
            {

            }
        }


    }
}

