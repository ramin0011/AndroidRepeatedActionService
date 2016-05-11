using System;
using Android.App;
using Android.Content;

namespace AndroidLocalService.AndroidService
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "androidlocalservice.BroadcastReceiver" })]
    public class AndroidLocalServiceBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
			string intentAction= intent.GetStringExtra(AndroidLocalServiceHelper.IntetntExtraKey);
			if (string.IsNullOrWhiteSpace (intentAction))
			{
			    string serviceType = intent.GetStringExtra(AndroidLocalServiceHelper.serviceTypeKey);
                AndroidLocalService.RunIntentInService(context, intentAction,Type.GetType(serviceType,false));
            }
        }
    }
  
}