using System;
namespace SongBird.Models
{
    public class Artist
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
            }
        }

        public Artist()
        {
        }
    }
}

