﻿using Android.App;
using Android.OS;
using Android.Widget;

namespace AndroidLocalServiceSample
{
	[Activity (Label = "AndroidActionBackgroundServiceSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
			    AndroidLocalService.AndroidLocalServiceHelper.RunService(this, "MainService");
			};
		}
	}
}


