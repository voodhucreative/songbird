using System;
using SongBird.Models;

namespace SongBird.Helpers
{
    public static class ArtistManager
    {
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
    }
}

