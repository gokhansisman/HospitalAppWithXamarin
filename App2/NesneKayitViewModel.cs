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
    public class NesneKayitViewModel : INotifyPropertyChanged
    {
        private readonly IKoridorRepository _koridorRepository;
        private readonly IOdaRepository _odaRepositroy;
        private readonly INesneRepository _nesneRepository;
        private IEnumerable<Nesne> _nesneler;
        private List<Oda> odalars;
        private List<Koridor> koridors; 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public NesneKayitViewModel(IOdaRepository odaRepository,IKoridorRepository koridorRepository,INesneRepository nesneRepository)
        {
            _koridorRepository = koridorRepository;
            _odaRepositroy = odaRepository;
            _nesneRepository = nesneRepository;
            odalars = _odaRepositroy.GetOdaAsync().GetAwaiter().GetResult();

            Odalarim = odalars.Select(x => x.Numara).ToArray<Int32>();

            koridors = _koridorRepository.GetKoridorAsync().GetAwaiter().GetResult();

            Koridorlarim = koridors.Select(x => x.Numara).ToArray<Int32>();

        }

        public int KoridorId { get; set; }
        public int KoridorNumarasi { get; set; }
        public string NesneAdi { get; set; }
        public int[] Odalarim { get; set; }
        public int[] Koridorlarim { get; set; }
        public int OdaSelectedIndex { get; set; }
        public int KoridorSelectedIndex { get; set; }

        public IEnumerable<Nesne> Nesnelerim
        {
            get
            {
                return _nesneler;
            }
            set
            {
                _nesneler = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Nesnelerim = await _nesneRepository.GetNesneAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if (odalars.Any() && koridors.Any() && OdaSelectedIndex != -1)
                    {
                        var nesne = new Nesne
                        {
                            KoridorId = koridors[KoridorSelectedIndex].Id,
                            OdaId = odalars[OdaSelectedIndex].Id,
                            Adi = NesneAdi,

                        };
                        await _nesneRepository.AddNesneAsync(nesne);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    }
                });

            }
        }



    }
}
