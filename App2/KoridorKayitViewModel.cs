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
    public class KoridorKayitViewModel : INotifyPropertyChanged
    {
        private readonly IKoridorRepository _koridorRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private IEnumerable<Koridor> _koridorlar;
        private List<Hastane> hastanes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public KoridorKayitViewModel(IHastaneRepository hastaneRepository,IKoridorRepository koridorRepository)
        {
            _koridorRepository = koridorRepository;
            _hastaneRepository = hastaneRepository;

            hastanes = _hastaneRepository.GetHastaneAsync().GetAwaiter().GetResult();

            Hastanelerim = hastanes.Select(x => x.Adi).ToArray();
        }

        public int KoridorId { get; set; }
        public int KoridorNumarasi { get; set; }
        public string[] Hastanelerim { get; set; }

        public int HastaneSelectedIndex { get; set; }
        public int KoridorHastaneId { get; set; }
        public IEnumerable<Koridor> Koridorlarim
        {
            get
            {
                return _koridorlar;
            }
            set
            {
                _koridorlar = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Koridorlarim = await _koridorRepository.GetKoridorAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if (hastanes.Any() && HastaneSelectedIndex != -1)
                    {
                        var koridor = new Koridor
                        {
                            Numara = KoridorNumarasi,
                            HastaneId = hastanes[HastaneSelectedIndex].Id,

                        };
                        await _koridorRepository.AddKoridorAsync(koridor);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    }
                });

            }
        }



    }
}
