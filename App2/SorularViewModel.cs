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
    public class SorularViewModel : INotifyPropertyChanged
    {
        private readonly ISorularRepository _sorularRepository;
        private readonly IOdaRepository _odaRepository;
        private readonly IKoridorRepository _koridorRepository;
        private readonly INesneRepository _nesneRepository;
        private IEnumerable<Sorular> _sorular;
        private List<Koridor> koridors;
        private List<Oda> odalars;
        private List<Nesne> nesnes;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
           // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public SorularViewModel(ISorularRepository sorularRepository,IOdaRepository odaRepository
            ,IKoridorRepository koridorRepository,INesneRepository nesneRepository)
        {
            _koridorRepository = koridorRepository;
            _odaRepository = odaRepository;
            _nesneRepository = nesneRepository;
            _sorularRepository = sorularRepository;

            koridors = _koridorRepository.GetKoridorAsync().GetAwaiter().GetResult();

            Koridorlarim = koridors.Select(x => x.Numara).ToArray<Int32>();

            odalars = _odaRepository.GetOdaAsync().GetAwaiter().GetResult();

            Odalarim = odalars.Select(x => x.Numara).ToArray<Int32>();

            nesnes = _nesneRepository.GetNesneAsync().GetAwaiter().GetResult();

            Nesnelerim = nesnes.Select(x => x.Adi).ToArray();

        }
        public string[] Nesnelerim { get; set; }
        public int[] Odalarim { get; set; }
        public int[] Koridorlarim { get; set; }
        public int KoridorSelectedIndex { get; set; }
        public int OdaSelectedIndex { get; set; }
        public int NesneSelectedIndex { get; set; }
        public int SoruId { get; set; }
        public string SoruAdi { get; set; }
        public string SoruCevap { get; set; }
        public bool SoruCevaplandiMi { get; set; }
        public int SoruNesneId { get; set; }
        public int SoruDoktorId { get; set; }
        public int SoruOdaId { get; set; }
        public int SoruKoridorId { get; set; }
        public string SoruZamanPeridoyu { get; set; }
        public IEnumerable<Sorular> Sorularim
        {
            get
            {
                return _sorular;
            }
            set
            {
                _sorular = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Sorularim = await _sorularRepository.GetSorularAsync();
                });
            }
        }
        public ICommand AddCommand
        {

            get
            {
                return new Command(async () =>
                {
                    var soru = new Sorular
                    {

                Adi = SoruAdi,
                Cevap = null,
                CevaplandiMi = false,
                DoktorId = -1,
                HemsireId = -1,
                NesneId = nesnes[NesneSelectedIndex].Id,
                OdaId = odalars[OdaSelectedIndex].Id,
                KoridorId = koridors[KoridorSelectedIndex].Id,
                ZamanPeridoyu = SoruZamanPeridoyu
                };
                await _sorularRepository.AddSoruAsync(soru);
                RefreshCommand.Execute("");
                   // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
            });

            }
        }



    }
}
