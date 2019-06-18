using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App2
{
    public class KullaniciKayitViewModel : INotifyPropertyChanged
    {
        
        private readonly IKullaniciRepository _kullaniciRepository;
        private IEnumerable<Kullanici> _kullancilar;
       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public KullaniciKayitViewModel(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;

              _kullancilar = _kullaniciRepository.GetKullaniciAsync().GetAwaiter().GetResult();

            //Kullanicilarim = _kullancilar.Select(x => x.Adi).ToArray();
        }


        //public string[] Kullanicilarim { get; set; }

        public string kullaniciAdi { get; set; }
        public string kullaniciSifre { get; set; }
        public IEnumerable<Kullanici> Kullanicilarim
        {
            get
            {
                return _kullancilar;
            }
            set
            {
                _kullancilar = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Kullanicilarim = await _kullaniciRepository.GetKullaniciAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                   
                        var kullanici = new Kullanici
                        {
                            Adi = kullaniciAdi,
                           Sifre = kullaniciSifre,

                        };
                        await _kullaniciRepository.AddKullaniciAsync(kullanici);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    
                });

            }
        }



    }
}
