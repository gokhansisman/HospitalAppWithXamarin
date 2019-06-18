using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HastaneKayit : ContentPage
    {
        public HastaneKayit()
        {
            InitializeComponent();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Deneme", "Deneme Tıklandi", "OK");
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("Deneme2", "Deneme2 Tıklandi", "OK");

        }
    }
}