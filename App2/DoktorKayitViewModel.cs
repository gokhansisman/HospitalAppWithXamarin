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
    public class DoktorKayitViewModel : INotifyPropertyChanged
    {
        private readonly IDoktorRepository _doktorRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private IEnumerable<Doktor> _doktorlar;
        private List<Hastane> hastanes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public DoktorKayitViewModel(IDoktorRepository doktorRepository,IHastaneRepository hastaneRepository)
        {
            _doktorRepository = doktorRepository;
            _hastaneRepository = hastaneRepository;

            hastanes = _hastaneRepository.GetHastaneAsync().GetAwaiter().GetResult();

            Hastanelerim = hastanes.Select(x => x.Adi).ToArray();
        }

        public int DoktorId { get; set; }
        public string DoktorAdi { get; set; }
        public string[] Hastanelerim { get; set; }
        public string DoktorAlani { get; set; }
        public int HastaneSelectedIndex { get; set; }
        public int DoktorHastaneId { get; set; }
        public IEnumerable<Doktor> Doktorlarim
        {
            get
            {
                return _doktorlar;
            }
            set
            {
                _doktorlar = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Doktorlarim = await _doktorRepository.GetDoktorAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if(hastanes.Any() && HastaneSelectedIndex != -1)
                    {
                        var doktor = new Doktor
                        {
                            Adi = DoktorAdi,
                           Alanı=DoktorAlani,
                           HastaneId = hastanes[HastaneSelectedIndex].Id,
                      
                        };
                        await _doktorRepository.AddDoktorAsync(doktor);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    }
                });

            }
        }



    }
}
