using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;


namespace Cfe.Mobile.App.Ordenes.Droid
{
    [Activity(Label = "Cfe.Mobile.App.Ordenes", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static readonly string CHANNEL_ID = "ordenes-ab907";
        internal static readonly int NOTIFICATION_ID = 100;
        const string TAG = "MyFirebaseIIDService";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //Este se agrega en Android
            Cfe.Mobile.App.Ordenes.Core.Core.OS.DependencyService.Register<OS.OS, Cfe.Mobile.App.Ordenes.Core.Core.OS.IOS>();
            Cfe.Mobile.App.Ordenes.Core.Core.OS.DependencyService.Register<OS.DB, Cfe.Mobile.App.Ordenes.Core.Core.OS.IDB >();

            
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);


            //FIREBASE

            if (Intent.Extras != null) {
                foreach (var key in Intent.Extras.KeySet()) {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            IsPlayServicesAvailable();

            CreateNotificationChannel();
            
            
            //Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);


            FirebaseMessaging.Instance.SubscribeToTopic("9l0bd");
            //Log.Debug(TAG, "Subscribed to remote notifications");

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable() {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success) {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.Write( GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else {
                    Console.Write("This device is not supported");
                    Finish();
                }
                return false;
            } else {
                Console.Write("Google Play Services is available.");
                return true;
            }
        }

        void CreateNotificationChannel() {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O) {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default) {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}