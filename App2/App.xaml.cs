using App.Core.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App2
{
    public partial class App : Application
    {
        public static string _dbPath { get; set; }

     

        public App(ISorularRepository sorularRepository,IKullaniciRepository kullaniciRepository,
            IHastaneRepository hastaneRepository,
            IDoktorRepository doktorRepository,
            IHemsireRepository hemsireRepository,
            IOdaRepository odalarRepository,
            IKoridorRepository koridorRepository,
            INesneRepository nesneRepository)
        {
            // _dbPath = dbPath;
            
            InitializeComponent();

            //MainPage = new ProductsPage()
            //{
            //    BindingContext = new ProductsViewModel(productRepository),
            //};
            //MainPage = new SorularSayfasi()
            //{
            //    BindingContext = new SorularViewModel(sorularRepository),
            //};
      


            var a = new LoginPage();
            MainPage = a;

            a.BindingContext = new LoginPageViewModel(Application.Current.MainPage.Navigation, kullaniciRepository, sorularRepository, 
            hastaneRepository, doktorRepository,hemsireRepository,odalarRepository,koridorRepository,nesneRepository);
        
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
