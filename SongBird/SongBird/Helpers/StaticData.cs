using System;
namespace SongBird.Helpers
{
	public static class StaticData
	{
		public static string TEST_CLIP_URL = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3";
		//public static string TEST_IMAGE = "Greeting1.jpeg";
        public static string TEST_IMAGE = "mathowlett1.png";

        public static Models.Greeting DefaultGreeting = new Models.Greeting
        {
            Name = "Dreamer",
            Image = "mathowlett1.png",
            Clip = new Models.Clip
            {
                Artist = new Models.Artist
                {
                    Name = "Mat Howlett",
                    Image = StaticData.TEST_IMAGE,
                    Description = "A beautiful singing bird"
                },
                Description = "This message comes to say I love you",
                Name = "Dreamer",
                Image = StaticData.TEST_IMAGE,
                SourceUrl = StaticData.TEST_CLIP_URL
            }
        };

    }
}

