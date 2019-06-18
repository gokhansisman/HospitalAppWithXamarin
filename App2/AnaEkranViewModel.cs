using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class AnaEkranViewModel: INotifyPropertyChanged
    {

        private readonly INavigation Navigation;
        private readonly ISorularRepository _soruRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private readonly IDoktorRepository _doktorRepository;
        private readonly IHemsireRepository _hemsireRepository;
        private readonly IOdaRepository _odalarRepository;
        private readonly INesneRepository _nesneRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        
        private IEnumerable<Sorular> _sorular;


        public ICommand HastaneCommand { get; set; }
        public ICommand AnaEkranCommand { get; set; }
        public ICommand GunlukCommand { get; set; }
        public ICommand HaftalikCommand { get; set; }
        public ICommand HemsireCommand { get; set; }
        public ICommand SoruKayitCommand { get; set; }
        public ICommand DoktorKayitCommand { get; set; }
        public ICommand OdaKayitCommand { get; set; }
        public ICommand KoridorKayitCommand { get; set; }
        public ICommand NesneKayitCommand { get; set; }
        public ICommand KullaniciKayitCommand { get; set; }
        


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public AnaEkranViewModel(INavigation navigation,ISorularRepository sorularRepository,IDoktorRepository doktorRepository,
            IHastaneRepository hastaneRepository,IHemsireRepository hemsireRepository,
            IOdaRepository odalarRepository,IKoridorRepository koridorRepository
            ,INesneRepository nesneRepository,IKullaniciRepository kullaniciRepository)
        {
            _doktorRepository = doktorRepository;
            _hastaneRepository = hastaneRepository;
            _odalarRepository = odalarRepository;
            _nesneRepository = nesneRepository;
            _soruRepository = sorularRepository;
            _kullaniciRepository = kullaniciRepository;
            Navigation = navigation;
            Sorularim = _soruRepository.GetSorularAsync().GetAwaiter().GetResult();
           var hastanes = _hastaneRepository.GetHastaneAsync().GetAwaiter().GetResult();
            
            HastaneCommand = new Command(x =>
            {
                HastaneKayit hastaneKayit = new HastaneKayit();
                Navigation.PushModalAsync(hastaneKayit).GetAwaiter();
                hastaneKayit.BindingContext = new HastaneKayitViewModel(hastaneRepository);

            });


            NesneKayitCommand = new Command(x =>
            {
                NesneKayit nesneKayit = new NesneKayit();
                Navigation.PushModalAsync(nesneKayit).GetAwaiter();
                nesneKayit.BindingContext = new NesneKayitViewModel(odalarRepository, koridorRepository,nesneRepository);

            });
            DoktorKayitCommand = new Command(x =>
            {
                DoktorKayit doktorKayit = new DoktorKayit();
                Navigation.PushModalAsync(doktorKayit).GetAwaiter();
                doktorKayit.BindingContext = new DoktorKayitViewModel(doktorRepository,hastaneRepository);

            });

            OdaKayitCommand = new Command(x =>
            {
                OdaKayit odaKayit = new OdaKayit();
                Navigation.PushModalAsync(odaKayit).GetAwaiter();
                odaKayit.BindingContext = new OdaKayitViewModel(odalarRepository,koridorRepository);

            });
           

            KoridorKayitCommand = new Command(x =>
                {
                    KoridorKayit koridorKayit = new KoridorKayit();
                    Navigation.PushModalAsync(koridorKayit).GetAwaiter();
                    koridorKayit.BindingContext = new KoridorKayitViewModel(hastaneRepository,koridorRepository);

                });
            HemsireCommand = new Command(x =>
                {
                    HemsireKayit hemsireKayit = new HemsireKayit();
                    Navigation.PushModalAsync(hemsireKayit).GetAwaiter();
                    hemsireKayit.BindingContext = new HemsireKayitViewModel(hemsireRepository,hastaneRepository);

                });
            SoruKayitCommand = new Command(x =>
            {
                SorularSayfasi sorularKayit = new SorularSayfasi();
                Navigation.PushModalAsync(sorularKayit).GetAwaiter();
                sorularKayit.BindingContext = new SorularViewModel(sorularRepository,odalarRepository,koridorRepository,nesneRepository);

            });
            KullaniciKayitCommand = new Command(x =>
            {
                KullaniciKayit kullaniciKayit = new KullaniciKayit();
                Navigation.PushModalAsync(kullaniciKayit).GetAwaiter();
                kullaniciKayit.BindingContext = new KullaniciKayitViewModel(kullaniciRepository);

            });
            AnaEkranCommand = new Command(x =>
            {
                AnaEkran anaekran = new AnaEkran();
                Navigation.PushModalAsync(new NavigationPage(anaekran)).GetAwaiter();

              //  anaekran.BindingContext = new AnaEkranViewModel(Navigation,_sorularRepository, _doktorRepository, _hastaneRepository);
            });



            GunlukCommand = new Command(x =>
            {
                //Günlük
            });

            HaftalikCommand = new Command(x =>
            {
                //Haftalik
            });

        }
      
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
       
       
        
       

    }
}
