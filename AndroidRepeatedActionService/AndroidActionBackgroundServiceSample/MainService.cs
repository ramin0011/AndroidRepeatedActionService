using Android;
using Android.App;

namespace AndroidLocalServiceSample
{
    [Service(Exported = true)]
    [IntentFilter(new string[] {"MainService"})]
    public class MainService: AndroidLocalService.AndroidLocalService
    {
        public MainService() : base("MainService",10000)
        {
        }
        
        protected override void OnAction()
        {
            AndroidLocalService.AndroidLocalServiceHelper.
                PushNotification(ApplicationContext,"main service","hello dear user",Resource.Drawable.localServiceIcon,typeof(MainActivity));
        }
    }

}