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

        public static FirebaseClient fc;

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

        public static bool PostObject(string target, Object obj)
        {
            try
            {
                if (connectFirebase())
                {

                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        await fc.Child(target).PostAsync(obj);
                        return true;
                    });
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        static bool populate = false;

        public static async Task PopulateDebugDatabase()
        {
            await Task.Delay(1000);

            if (populate)
            {
                UserManager.Create(
                    "Mat",
                    "Howlett",
                    "mathowlett@gmail.com");

                UserManager.Create(
                    "Neil",
                    "Chatterton",
                    "neilchatt@gmail.com");

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
                    Artist = new Artist
                    {
                        Name = "Fallen Fields"
                    },
                    Image = "https://picsum.photos/200",
                    SourceUrl = "https://www.voodhu.com/songbird/artists/fallenfields/clips/ff_unbreakable_heart.mp3"
                };

                ClipManager.Create("New Song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Good Song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Daft Song", c1.SourceUrl, c1.Artist, c1.Image);
                ClipManager.Create("Silly Song", c1.SourceUrl, c1.Artist, c1.Image);

                GreetingManager.Create("Great Clip", c1);
                GreetingManager.Create("Funky Clip", c1);
                GreetingManager.Create("Dopey Clip", c1);

            }
        }
    }
}

