using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App2
{
    public class HastaneKayitViewModel : INotifyPropertyChanged
    {
        private readonly IHastaneRepository _hastaneRepository;
        private IEnumerable<Hastane> _hasteneler;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public HastaneKayitViewModel(IHastaneRepository hastaneRepository)
        {
            _hastaneRepository = hastaneRepository;
        }
        public int HastaneId { get; set; }
        public string HastaneAdi { get; set; }
        public string HastaneSube { get; set; }
       
        public IEnumerable<Hastane> Hastanelerim 
        {
            get
            {
                return _hasteneler;
            }
            set
            {
                _hasteneler = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Hastanelerim = await _hastaneRepository.GetHastaneAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    var hastane = new Hastane
                    {
                       
                        Adi = HastaneAdi,
                        Sube = HastaneSube,
                    };
                    await _hastaneRepository.AddHastaneAsync(hastane);
                    RefreshCommand.Execute("");
                    // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                });

            }
        }



    }
}
