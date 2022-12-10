using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Firebase.Database.Query;
using MvvmHelpers;
using SongBird.Models;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public class UserManager : INotifyPropertyChanged
    {
        static User currentUser;
        public static User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = new User
                    {
                        FirstName = "Current",
                        LastName = "User",
                        Email = "",
                        Password = "",
                        IsRegistered = false,
                        IsLoggedIn = false
                    };
                }
                return currentUser;
            }
            set
            {
                if (value == currentUser)
                    return;

                currentUser = value;
            }
        }

        ObservableRangeCollection<User> users;

        public ObservableRangeCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }

        public static async Task<bool> Create(string firstName, string lastName, string email, string password)
        {
            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            await DataManager.PostObject(DataManager.USERS_TABLE, newUser);

            return true;
        }

        //Read All    
        public async Task<ObservableRangeCollection<User>> GetAllUsers()
        {
            //IsBusy = true;
            try
            {
                Users = new ObservableRangeCollection<User>();

                if (DataManager.connectFirebase())
                {
                    var lst = (await DataManager.fc.Child(DataManager.USERS_TABLE).OnceAsync<User>()).Select(x =>
                    new User
                    {
                        FirstName = x.Object.FirstName,
                        LastName = x.Object.LastName,
                        Email = x.Object.Email
                    }).ToList();

                    Users = new ObservableRangeCollection<User>(lst);
                }
            }
            catch (Exception ex)
            {
                //Message = "Error occurred in getting products. Error:" + ex.Message.ToString();
            }

            return Users;

            //IsBusy = false;

            /*
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }*/
        }

        //Read     
        public async Task<User> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUsers();

                await DataManager.fc
                .Child("Users")
                .OnceAsync<User>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Inser a user    
        public static async Task<bool> AddUser(string email, string password)
        {
            try
            {
                User newUser = new User
                {
                    Email = email,
                    Password = password
                };
                await DataManager.PostObject(DataManager.USERS_TABLE, newUser);
                newUser.IsRegistered = true;
                newUser.IsLoggedIn = true;
                UserManager.CurrentUser = newUser;
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update     
        public static async Task<bool> UpdateUser(string email, string password)
        {
            try
            {
                var toUpdateUser = (await DataManager.fc
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await DataManager.fc
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new User() { Email = email, Password = password });
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {
                var toDeletePerson = (await DataManager.fc
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await DataManager.fc.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return false;
            }
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
