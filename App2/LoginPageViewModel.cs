using App2;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Core.ViewModels
{
    public class LoginPageViewModel : BasePageViewModel
    {
        private readonly INavigation Navigation;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly ISorularRepository _sorularRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private readonly IDoktorRepository _doktorRepository;
        private readonly IHemsireRepository _hemsireRepository;
        private readonly IOdaRepository _odaRepository;
        private readonly IKoridorRepository _koridorRepository;
        private readonly INesneRepository _nesneRepository;





        public LoginPageViewModel(INavigation _navigation, IKullaniciRepository kullaniciRepository, ISorularRepository sorularRepository,
            IHastaneRepository hastaneRepository,
            IDoktorRepository doktorRepository,
            IHemsireRepository hemsireRepository,
            IOdaRepository odalarRepository,
            IKoridorRepository koridorRepository,
            INesneRepository nesneRepository)
        {

            _kullaniciRepository = kullaniciRepository;
            _sorularRepository = sorularRepository;
            _hastaneRepository = hastaneRepository;
            _doktorRepository = doktorRepository;
            _hemsireRepository = hemsireRepository;
            _odaRepository = odalarRepository;
            _koridorRepository = koridorRepository;
            _nesneRepository = nesneRepository;

            Navigation = _navigation;
            PageTitle = "Giriþ Sayfasýna Hoþgeldiniz";
           
        }

        #region Properties

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (SetPropertyValue(ref _email, value))
                {
                    ((Command)LoginCommand).ChangeCanExecute();                    
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (SetPropertyValue(ref _password, value))
                {
                    ((Command)LoginCommand).ChangeCanExecute();
                }
            }
        }

        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set { SetPropertyValue(ref _isShowCancel, value); }
        }

        #endregion

        public ICommand DoktorHemsireGirisCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var doktorHemsiresayfasi = new DoktorHemsireSayfasi();
                    Navigation.PushModalAsync(new NavigationPage(doktorHemsiresayfasi)).GetAwaiter();

                    doktorHemsiresayfasi.BindingContext = new DoktorHemsireSayfasiViewModel(Navigation, _sorularRepository,
                     _doktorRepository, _hastaneRepository,
                      _hemsireRepository, _odaRepository,
                      _koridorRepository, _nesneRepository);
                });
            }
        }
        #region Commands
        
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new Command(LoginAction, CanLoginAction); }
        }

        private ICommand _cancelLoginCommand;
        public ICommand CancelLoginCommand
        {
            get { return _cancelLoginCommand = _cancelLoginCommand ?? new Command(CancelLoginAction); }
        }

        private ICommand _forgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new Command(ForgotPasswordAction); }
        }

        private ICommand _newAccountCommand;
        public ICommand NewAccountCommand
        {
            get { return _newAccountCommand = _newAccountCommand ?? new Command(NewAccountAction); }
        }
        
        #endregion


        #region Methods

        bool CanLoginAction()
        {
            //Perform your "Can Login?" logic here...

           
            if (string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.Password))
                return false;

            return true;
        }

        async void LoginAction()
        {
            IsBusy = true;

            var SuankiKullanici = _kullaniciRepository.QueryKullaniciAsync(x => x.Adi == Email && x.Sifre == Password).GetAwaiter().GetResult().FirstOrDefault();
            if (SuankiKullanici.IsNull())
            {
                 var sorularPage = new SorularSayfasi();
                var HastaneKayitSayfasi = new HastaneKayit();
                var DoktorKayitSayfasi = new DoktorKayit();
                AnaEkran anaekran = new AnaEkran();
                EnteredUserInfos.Current = SuankiKullanici;
                IsBusy = false;
                var doktorHemsiresayfasi = new DoktorHemsireSayfasi();
                //Navigation.PushModalAsync(new NavigationPage(doktorHemsiresayfasi)).GetAwaiter();

                //doktorHemsiresayfasi.BindingContext = new DoktorHemsireSayfasiViewModel(Navigation, _sorularRepository,
                //  _doktorRepository, _hastaneRepository,
                //  _hemsireRepository, _odaRepository,
                //   _koridorRepository, _nesneRepository);


                //Navigation.PushModalAsync(DoktorKayitSayfasi).GetAwaiter();

                //  DoktorKayitSayfasi.BindingContext = new DoktorKayitViewModel(_doktorRepository,_hastaneRepository);
                //  ----
                Navigation.PushModalAsync(new NavigationPage(anaekran)).GetAwaiter();

                anaekran.BindingContext = new AnaEkranViewModel(Navigation, _sorularRepository,
                    _doktorRepository, _hastaneRepository,
                    _hemsireRepository, _odaRepository,
                    _koridorRepository, _nesneRepository,_kullaniciRepository);
               
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Hata", "Hatali giriþ yaptýnýz", "OK").GetAwaiter();
                IsBusy = false;
                // ------



                //   Navigation.PushModalAsync(HastaneKayitSayfasi).GetAwaiter();

                //  HastaneKayitSayfasi.BindingContext = new HastaneKayitViewModel(_hastaneRepository);

                //  Navigation.PushModalAsync(sorularPage).GetAwaiter();

                // sorularPage.BindingContext = new SorularViewModel(_sorularRepository);
            }

            //TODO - perform your login action + navigate to the next page

            //Simulate an API call to show busy/progress indicator            
            //  Task.Delay(20000).ContinueWith((t) => IsBusy = false);

            //Show the Cancel button after X seconds
            //Task.Delay(5000).ContinueWith((t) => IsShowCancel = true);
        }

        void CancelLoginAction()
        {
            //TODO - perform cancellation logic

            IsBusy = false;
            IsShowCancel = false;
        }

        void ForgotPasswordAction()
        {
            //TODO - navigate to your forgotten password page
            //Navigation.PushAsync(XXX);
        }

        void NewAccountAction()
        {
            //TODO - navigate to your registration page
            //Navigation.PushAsync(XXX);
        }

        #endregion
    }
}
