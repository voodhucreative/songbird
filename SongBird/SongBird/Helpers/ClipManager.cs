using System;
using MvvmHelpers;
using SongBird.Models;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public static class ClipManager
    {
        public static ObservableRangeCollection<Clip> Clips;
        public static ObservableRangeCollection<Grouping<string, Greeting>> GroupedClips { get; }

        public static bool Init()
        {
            

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
    }
}

