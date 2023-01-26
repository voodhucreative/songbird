using System;
namespace SongBird.Helpers
{
    public static class Session
    {
        private static Models.Artist currentArtist;
        public static Models.Artist CurrentArtist
        {
            get => currentArtist;
            set
            {
                if (value == currentArtist)
                    return;

                currentArtist = value;
            }
        }

        private static Models.Clip currentClip;
        public static Models.Clip CurrentClip
        {
            get => currentClip;
            set
            {
                if (value == currentClip)
                    return;

                currentClip = value;
            }
        }

        private static Models.Greeting currentGreeting;
        public static Models.Greeting CurrentGreeting
        {
            get => currentGreeting;
            set
            {
                if (value == currentGreeting)
                    return;

                currentGreeting = value;
            }
        }
    }
}

