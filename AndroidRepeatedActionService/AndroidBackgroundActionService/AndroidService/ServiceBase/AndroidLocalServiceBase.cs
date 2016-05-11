using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace AndroidLocalService
{
    [Preserve(AllMembers = true)]
    public abstract class AndroidLocalService : IntentService
    {
        private static object LOCK = new object();
        private static int serviceId = 10;
        private const string WAKELOCK_KEY = "Costume_LIB";
        private static PowerManager.WakeLock sWakeLock;
        private const int MAX_BACKOFF_MS = 3600000;
		string serviceName;
        long milliseconds = 0;
        public AndroidLocalService(string name,long milliseconds)
          : base("IntentService-" + name)
        {
            this.milliseconds = milliseconds;
			serviceName = name;
        }

        protected abstract void OnAction();

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Context applicationContext = this.ApplicationContext;
                string action = intent.Action;

				this.handleRegistration(applicationContext, serviceName,milliseconds);
       
            }
            finally
            {
                object obj = AndroidLocalService.LOCK;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    if (AndroidLocalService.sWakeLock != null)
                    {
                        AndroidLocalService.sWakeLock.Release();
                    }
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(obj);
                }
            }
        }

        internal static void RunIntentInService(Context context, string serviceInstent, Type type)
        {
            object obj = AndroidLocalService.LOCK;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                if (AndroidLocalService.sWakeLock == null)
                    AndroidLocalService.sWakeLock = PowerManager.FromContext(context).NewWakeLock(WakeLockFlags.Partial, "Costume_LIB");
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
            AndroidLocalService.sWakeLock.Acquire();
            var intent=new Intent(serviceInstent);
            if(type!=null)
            intent.SetClass(context, type);
            context.StartService(intent);
        }

		private void handleRegistration(Context context, string intentAction, long millisecond)
        {
            OnAction();
			Intent intent1 =new Intent("androidlocalservice.BroadcastReceiver");
            intent1.PutExtra ("intentService", intentAction);
            PendingIntent broadcast = PendingIntent.GetBroadcast(context, 0, intent1, PendingIntentFlags.OneShot);
            AlarmManager.FromContext(context)
                .Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + (long) millisecond, broadcast);
        }
    }
}
