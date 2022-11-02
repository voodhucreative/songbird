using System;
namespace SongBird.Models
{
    public class Clip
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

        string image;
        public string Image
        {
            get => image;
            set
            {
                if (value == image)
                    return;
                image = value;
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
            }
        }

        public Clip()
        {

        }
    }
}

