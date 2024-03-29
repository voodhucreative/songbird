﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SongBird.Models
{
    public class User : INotifyPropertyChanged
    {
        string first_name;
        public string FirstName
        {
            get => first_name;
            set
            {
                if (value == first_name)
                    return;

                first_name = value;
                OnPropertyChanged();
            }
        }

        string last_name;
        public string LastName
        {
            get => last_name;
            set
            {
                if (value == last_name)
                    return;

                last_name = value;
                OnPropertyChanged();
            }
        }

        string email;
        public string Email
        {
            get => email;
            set
            {
                if (value == email)
                    return;

                email = value;
                OnPropertyChanged();
            }
        }

        string password;
        public string Password
        {
            get => password;
            set
            {
                if (value == password)
                    return;

                password = value;
                OnPropertyChanged();
            }
        }

        bool isRegistered;
        public bool IsRegistered
        {
            get => isRegistered;
            set
            {
                if (value == isRegistered)
                    return;

                isRegistered = value;
                OnPropertyChanged();
            }
        }

        bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                if (value == isLoggedIn)
                    return;

                isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public User()
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

