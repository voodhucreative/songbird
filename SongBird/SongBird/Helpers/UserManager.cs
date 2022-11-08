using System;
using SongBird.Models;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public static class UserManager
    {
        public static bool Create(string firstName, string lastName, string email)
        {
            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            DataManager.PostObject(DataManager.USERS_TABLE, newUser);

            return true;
        }
    }
}
