using System;
using Android.App;
using Android.Content;
using Android.Media;

namespace AndroidLocalService
{
   public class AndroidLocalServiceHelper
    {
        internal static string IntetntExtraKey = "AndroidLocalServiceIntentServiceName",
            BroadCastName = "baseServiceBroadCast",
            serviceTypeKey="serviceTypeKey";

        public static void RunService(Context context, string serviceName,Type service = null)
        {
            Intent intent=new Intent("androidlocalservice.BroadcastReceiver");
            intent.PutExtra(IntetntExtraKey, "ListerMainService");
            if (service != null) intent.PutExtra(serviceTypeKey, service.FullName);
            context.SendBroadcast(intent);
        }
        public static void PushNotification(Context context,string title, string message,int drawableIcon,Type activityResult)
        {
            var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

            var uiIntent = new Intent(context,activityResult);

            var notification = new Android.App.Notification(drawableIcon, title)
            {
                Sound = RingtoneManager.GetDefaultUri(RingtoneType.Notification),
                Flags = NotificationFlags.AutoCancel
            };

            notification.SetLatestEventInfo(context, title, message, PendingIntent.GetActivity(context, 0, uiIntent, 0));

            if (notificationManager != null) notificationManager.Notify(2, notification);
        }
    }
   
    
}