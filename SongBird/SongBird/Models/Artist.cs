using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SongBird.Models
{
    public class Artist : INotifyPropertyChanged
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

        string description;
        public string Description
        {
            get => description;
            set
            {
                if (value == description)
                    return;

                description = value;
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

        public Artist()
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

