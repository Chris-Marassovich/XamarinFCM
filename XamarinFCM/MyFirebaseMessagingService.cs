
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// All sample code taken from walk-through at
/// https://docs.microsoft.com/en-au/xamarin/android/data-cloud/google-messaging/remote-notifications-with-fcm?tabs=windows#fcm-notifications-overview
/// </summary>
namespace XamarinFCM
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnNewToken(string newToken)
        {
            base.OnNewToken(newToken);
            Log.Debug(TAG, "Firebase Token: " + newToken);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            if (message.Data != null && message.Data.Any())
            {
                Log.Debug(TAG, "Notification has data so doing something else.");
            }
            else
            {
                SendNotification(message.GetNotification().Title, message.GetNotification().Body, message.Data);
            }
        }

        void SendNotification(string messageTitle, string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                                                          MainActivity.NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.common_google_signin_btn_icon_dark)
                                      .SetContentTitle(messageTitle)
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}