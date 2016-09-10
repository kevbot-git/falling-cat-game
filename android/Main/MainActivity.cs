using Android.App;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

namespace FallingCatGame.Main
{
    [Activity(Label = "Falling Cat" // This is what shows on the app window title and launcher icon
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : AndroidGameActivity
    {
        private SensorManager sensorManager;
        private Main game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            sensorManager = (SensorManager)GetSystemService(SensorService);
            game = new Main();
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
    }
}

