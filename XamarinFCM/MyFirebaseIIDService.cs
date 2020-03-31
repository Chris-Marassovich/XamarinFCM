
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;

/// <summary>
/// All sample code taken from walk-through at
/// https://docs.microsoft.com/en-au/xamarin/android/data-cloud/google-messaging/remote-notifications-with-fcm?tabs=windows#fcm-notifications-overview
/// </summary>
namespace XamarinFCM
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }
    }
}