using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SongBird.Models
{
    public class Greeting : INotifyPropertyChanged
    {
        Clip clip;
        public Clip Clip
        {
            get => clip;
            set
            {
                if (value == clip)
                    return;

                clip = value;
                OnPropertyChanged();
            }
        }

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

        public Greeting()
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

