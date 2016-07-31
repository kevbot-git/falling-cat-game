using Android.App;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
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
    public class GameActivity : AndroidGameActivity, ISensorEventListener
    {
        private static readonly object syncLock = new object();
        private SensorManager sensorManager;
        private GameMain game;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) { }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (syncLock) // Syncronized to make sure this is the only block updating accel at any one time
            {
                if (game != null)
                {
                    game.Accelerometer = new Vector3(e.Values[0], e.Values[1], e.Values[2]);
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            sensorManager = (SensorManager) GetSystemService(SensorService);
            game = new GameMain();
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Game);
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }
    }
}

