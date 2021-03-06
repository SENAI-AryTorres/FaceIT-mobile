﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Plugin.Media;
using Plugin.Permissions;

namespace FaceIT.Droid
{
    [Activity(Label = "FaceIT", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc5NDYyQDMxMzgyZTMxMmUzMEkyTEFwc0V3a0c5Q21mTmxZekRtSDA5cFMrTi9TSUFNREJDMjdiS01YMHc9");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            await CrossMedia.Current.Initialize();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);

            LoadApplication(new App());

            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 64, 128));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}