using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmHelpers;
using SongBird.Models;

namespace SongBird.Helpers
{
    public class ArtistManager : INotifyPropertyChanged
    {
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

