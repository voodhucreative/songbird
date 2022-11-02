using System;
namespace SongBird.Models
{
    public class Chirp
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

        public Chirp()
        {
           
        }
    }
}

