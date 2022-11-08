using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SongBird.Models
{
    public class Clip : INotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == name)
                    return;

                name = value;
                OnPropertyChanged();
            }
        }

        string image;
        public string Image
        {
            get => image;
            set
            {
                if (value == image)
                    return;
                image = value;
                OnPropertyChanged();
            }
        }

        Artist artist;
        public Artist Artist
        {
            get => artist;
            set
            {
                if (value == artist)
                    return;

                artist = value;
                OnPropertyChanged();
            }
        }

        string sourceUrl;

        public string SourceUrl
        {
            get => sourceUrl;
            set
            {
                if (value == sourceUrl)
                    return;

                sourceUrl = value;
                OnPropertyChanged();
            }
        }

        public Clip()
        {

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

