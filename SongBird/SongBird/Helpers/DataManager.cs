using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using SongBird.Models;
using Xamarin.Forms;

namespace SongBird.Helpers
{
    public static class DataManager
    {
        public static string USERS_TABLE = "users";
        public static string CLIPS_TABLE = "clips";
        public static string ARTISTS_TABLE = "artists";
        public static string GREETINGS_TABLE = "greetings";

        public static string FirebaseClient = "https://songbirdsounds-ca07b-default-rtdb.europe-west1.firebasedatabase.app/";
        public static string FirebaseSecret = "S622KcLVR9m388JxoGlbBK2ShBcE7A7v31cuPPQ7";

        public static string FILE_ACCESS_TOKEN = "108f8229-07c7-4523-936b-88eb81536039";

        public static FirebaseClient fc;

        static bool populate = false;

        public static bool connectFirebase()
        {
            try
            {
                if (fc == null)
                {
                    fc = new FirebaseClient(FirebaseClient,
                               new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecret) });
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<bool> PostObject(string target, Object obj)
        {
            try
            {
                if (connectFirebase())
                {
                    await fc.Child(target).PostAsync(obj);
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }


        

        public static async Task PopulateDebugDatabase()
        {
            await Task.Delay(1000);

            if (populate)
            {
                await UserManager.Create(
                    "Mat",
                    "Howlett",
                    "mathowlett@gmail.com", "test");

                await UserManager.Create(
                    "Neil",
                    "Chatterton",
                    "neilchatt@gmail.com", "test");

                ArtistManager.Create(
                    "Fallen Fields",
                    "Alt rock band from Hull",
                    "");

                ArtistManager.Create(
                    "Matmoo",
                    "Singer Songwriter",
                    "");

                Clip c1 = new Clip
                {
                    Name = "Unbreakable",
                    Description = "You'll never break me. You'll never break an unbreakable heart.",
                    Artist = new Artist
                    {
                        Name = "Fallen Fields"
                    },
                    Image = "https://picsum.photos/200",
                    SourceUrl = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3"
                };

                ClipManager.Create("New Song", "This is a new song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Good Song", "This is a good song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Daft Song", "This is a daft song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Silly Song", "This is a silly song", c1.SourceUrl, c1.Artist, c1.Image);

                GreetingManager.Create("Great Clip", c1);
                GreetingManager.Create("Funky Clip", c1);
                GreetingManager.Create("Dopey Clip", c1);

            }
        }
    }
}

