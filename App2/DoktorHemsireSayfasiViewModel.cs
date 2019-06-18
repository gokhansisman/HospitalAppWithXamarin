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
    public class DoktorHemsireSayfasiViewModel : INotifyPropertyChanged
    {

        private readonly INavigation Navigation;
        private readonly ISorularRepository _soruRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private readonly IDoktorRepository _doktorRepository;
        private readonly IKoridorRepository _koridorRepository;
        

        private readonly IHemsireRepository _hemsireRepository;
        private readonly IOdaRepository _odalarRepository;
        private readonly INesneRepository _nesneRepository;
        private IEnumerable<Sorular> _sorular;
        private IEnumerable<Oda> _odalar;
        private IEnumerable<Koridor> _koridor;
        private IEnumerable<Nesne> _nesne;

        private IEnumerable<Doktor> _doktor;
        
        //Question
        public ICommand HastaneCommand { get; set; }
        public ICommand AnaEkranCommand { get; set; }
        public ICommand GunlukCommand { get; set; }
        public ICommand HaftalikCommand { get; set; }
        public ICommand HemsireCommand { get; set; }
        public ICommand SoruKayitCommand { get; set; }
        public ICommand OdaKayitCommand { get; set; }
        public ICommand KoridorKayitCommand { get; set; }
        public ICommand NesneKayitCommand { get; set; }
        public ICommand CevaplariGöster { get; set; }
        
        public ICommand AAAA { get; set; }

        Sorular _question;
        public Sorular Question
        {
            get
            {
                return _question;
            }

            set
            {
               
                _question = value;

                
                OnPropertyChanged("Question");

            }
        }
        

        string cevap;
        public string Cevap
        {
            get
            {
                return cevap;
            }

            set
            {
                
                cevap = value;


                OnPropertyChanged("Cevap");

            }
        }

        Oda _odalarim;
        public Oda Odalarim
        {
            get
            {
                return _odalarim;
            }

            set
            {
                _odalarim = value;


                var sorularim = _soruRepository.QuerySoruAsync(xd => xd.OdaId == _odalarim.Id).GetAwaiter().GetResult();

                Sorularim = sorularim.Where(x => !SoruyaCevapVerildiMi(x.ZamanPeridoyu, x.CevaplanmaZamani));

                OnPropertyChanged("Odalarim");
            }
        }

        int _selectedIndexOdalarim;
        public int OdalarimSelectedIndex
        {
            get
            {
                return _selectedIndexOdalarim;
            }

            set
            {
                _selectedIndexOdalarim = value;
                OnPropertyChanged("OdalarimSelectedIndex");
            }
        }

        Nesne _nesnelerim;
        public Nesne Nesnelerim
        {
            get
            {
                return _nesnelerim;
            }

            set
            {
                _nesnelerim = value;


               
                var sorularim = _soruRepository.QuerySoruAsync(xd => xd.NesneId == _nesnelerim.Id).GetAwaiter().GetResult();

                Sorularim = sorularim.Where(x => !SoruyaCevapVerildiMi(x.ZamanPeridoyu, x.CevaplanmaZamani));

                OnPropertyChanged("Nesnelerim");
            }
        }

        int _selectedIndexNesnelerim;
        public int NesnelerimSelectedIndex
        {
            get
            {
                return _selectedIndexNesnelerim;
            }

            set
            {
                _selectedIndexNesnelerim = value;
                OnPropertyChanged("NesnelerimSelectedIndex");
            }
        }

        Koridor _koridorlarim;
        public Koridor Koridorlarim
        {
            get
            {
                return _koridorlarim;
            }

            set
            {
                _koridorlarim = value;


               
                var sorularim = _soruRepository.QuerySoruAsync(xd => xd.KoridorId == _koridorlarim.Id).GetAwaiter().GetResult();

                Sorularim = sorularim.Where(x => !SoruyaCevapVerildiMi(x.ZamanPeridoyu, x.CevaplanmaZamani));

                OnPropertyChanged("Koridorlarim");
            }
        }

        int _selectedIndexKoridorlarim;
        public int KoridorlarimSelectedIndex
        {
            get
            {
                return _selectedIndexKoridorlarim;
            }

            set
            {
                _selectedIndexKoridorlarim = value;
                OnPropertyChanged("KoridorlarimSelectedIndex");
            }
        }

        Doktor _doktorlarim;
        public Doktor Doktorlarim
        {
            get
            {
                return _doktorlarim;
            }

            set
            {
                _doktorlarim = value;

                
                //Sorularim = _soruRepository.QuerySoruAsync(xd => xd.DoktorId == _doktorlarim.Id).GetAwaiter().GetResult();


                OnPropertyChanged("Doktorlarim");
            }
        }

        int _selectedIndexDoktorlarim;
        public int DoktorlarimSelectedIndex
        {
            get
            {
                return _selectedIndexDoktorlarim;
            }

            set
            {
                _selectedIndexDoktorlarim = value;
                OnPropertyChanged("DoktorlarimSelectedIndex");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public DoktorHemsireSayfasiViewModel(INavigation navigation, ISorularRepository sorularRepository, IDoktorRepository doktorRepository,
            IHastaneRepository hastaneRepository, IHemsireRepository hemsireRepository,
            IOdaRepository odalarRepository, IKoridorRepository koridorRepository
            , INesneRepository nesneRepository)
        {
            _doktorRepository = doktorRepository;
            _hastaneRepository = hastaneRepository;
            _odalarRepository = odalarRepository;
            _nesneRepository = nesneRepository;
            _soruRepository = sorularRepository;
            _koridorRepository = koridorRepository;

            Navigation = navigation;

            Sorularim = _soruRepository.QuerySoruAsync(x => !SoruyaCevapVerildiMi(x.ZamanPeridoyu, x.CevaplanmaZamani)).GetAwaiter().GetResult();

           

            Odalars = _odalarRepository.GetOdaAsync().GetAwaiter().GetResult();
            Koridors = _koridorRepository.GetKoridorAsync().GetAwaiter().GetResult();
            Doktors = _doktorRepository.GetDoktorAsync().GetAwaiter().GetResult();
            Nesnes = _nesneRepository.GetNesneAsync().GetAwaiter().GetResult();
            var hastanes = _hastaneRepository.GetHastaneAsync().GetAwaiter().GetResult();




            CevaplariGöster = new Command(x =>
            {
                Cevaplar cevapKayit = new Cevaplar();
                Navigation.PushModalAsync(cevapKayit).GetAwaiter();
                cevapKayit.BindingContext = new CevaplarModelViewModel(sorularRepository, doktorRepository);

            });


            AnaEkranCommand = new Command(x =>
            {
                AnaEkran anaekran = new AnaEkran();
                Navigation.PushModalAsync(new NavigationPage(anaekran)).GetAwaiter();

                //  anaekran.BindingContext = new AnaEkranViewModel(Navigation,_sorularRepository, _doktorRepository, _hastaneRepository);
            });



            GunlukCommand = new Command(x =>
            {
                Sorularim = _soruRepository.QuerySoruAsync(asd => asd.ZamanPeridoyu == "günlük" && !asd.CevaplandiMi).GetAwaiter().GetResult();
                //Günlük
            });

            HaftalikCommand = new Command(x =>
            {
                Sorularim = _soruRepository.QuerySoruAsync(asd => asd.ZamanPeridoyu == "haftalık" && !asd.CevaplandiMi).GetAwaiter().GetResult();
                //Haftalik
            });

            AAAA = new Command(x =>
            {
                if (_question != null)
                {
                    //_question.Cevap = Cevap;
                    _question.CevaplandiMi = true;
                    if (_question.ZamanPeridoyu == "haftalik")
                    {
                        _question.CevaplanmaZamani = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    }
                    else
                    {
                        _question.CevaplanmaZamani = DateTime.Now;
                        //   _question.CevapDate = DateTime.Now.;

                    }
                    _soruRepository.UpdateSoruAsync(_question);
                }
            });
        
        


    }

        //onload

        
        private bool SoruyaCevapVerildiMi(string periyot, DateTime cevaplananZaman)
        {
            if (periyot == "günlük")
            {
                return GünleriGetir(1, cevaplananZaman);
            }
            else if (periyot == "haftalik")
            {
                return GünleriGetir(7, cevaplananZaman);
            }
            
            else return false;
        }

        private bool GünleriGetir(int maxDate,DateTime cevaplananZaman)
        {
            if (DateTime.Now.Date >= cevaplananZaman.Date)
            {
                TimeSpan days = DateTime.Now.Subtract(cevaplananZaman.Date);
                if (days.TotalDays < maxDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
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
        public IEnumerable<Oda> Odalars
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
        public IEnumerable<Koridor> Koridors
        {
            get
            {
                return _koridor;
            }
            set
            {
                _koridor = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Nesne> Nesnes
        {
            get
            {
                return _nesne;
            }
            set
            {
                _nesne = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Doktor> Doktors
        {
            get
            {
                return _doktor;
            }
            set
            {
                _doktor = value;
                OnPropertyChanged();
            }
        }




    }
}
