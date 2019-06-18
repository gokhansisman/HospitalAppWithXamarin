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
    public class OdaKayitViewModel : INotifyPropertyChanged
    {
        
        private readonly IOdaRepository _odaRepository;
        private readonly IKoridorRepository _koridorRepository;
        private IEnumerable<Oda> _odalar;
        private List<Koridor> koridors;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public OdaKayitViewModel(IOdaRepository odaRepository,IKoridorRepository koridorRepository)
        {

            _odaRepository = odaRepository;
            _koridorRepository = koridorRepository;
           
            koridors = _koridorRepository.GetKoridorAsync().GetAwaiter().GetResult();

            Koridorlarim = koridors.Select(x => x.Numara).ToArray<Int32>();
        }

        public int OdaId { get; set; }
        public int OdaNumarasi { get; set; }
        public int[] Koridorlarim { get; set; }

        public int KoridorSelectedIndex { get; set; }
        public int OdaKoridorId { get; set; }
        public IEnumerable<Oda> Odalarim
        {
            get
            {
                return _odalar;
            }
            set
            {
                _odalar = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Odalarim = await _odaRepository.GetOdaAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if (koridors.Any() && KoridorSelectedIndex != -1)
                    {
                        var oda = new Oda
                        {
                            Numara = OdaNumarasi,
                            KoridorId = koridors[KoridorSelectedIndex].Id,

                        };
                        await _odaRepository.AddOdaAsync(oda);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    }
                });

            }
        }



    }
}
