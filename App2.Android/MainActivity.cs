using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SqliteApp;

namespace App2.Droid
{
    [Activity(Label = "App2", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            // var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "proje.db");
            var dbPath = Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "proje.db");
           
            ISorularRepository sorularRepository = new SorularRepository(dbPath);
            IKullaniciRepository kullaniciRepository = new KullaniciRepository(dbPath);
            IHastaneRepository hastaneRepository = new HastaneRepository(dbPath);
            IDoktorRepository doktorRepository = new DoktorRepository(dbPath);
            IHemsireRepository hemsireRepository = new HemsireRepository(dbPath);
            IOdaRepository odaRepository = new OdalarRepository(dbPath);
            IKoridorRepository koridorRepository = new KoridorRepository(dbPath);
            INesneRepository nesneRepository = new NesneRepository(dbPath);

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(sorularRepository, kullaniciRepository,
                hastaneRepository, doktorRepository, hemsireRepository,odaRepository,koridorRepository,nesneRepository));
        }
    }
}