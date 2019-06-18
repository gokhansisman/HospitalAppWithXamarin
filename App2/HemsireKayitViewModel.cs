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
    public class HemsireKayitViewModel : INotifyPropertyChanged
    {
        private readonly IHemsireRepository _hemsireRepository;
        private readonly IHastaneRepository _hastaneRepository;
        private IEnumerable<Hemsire> _hemsireler;
        private List<Hastane> hastanes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public HemsireKayitViewModel(IHemsireRepository hemsireRepository, IHastaneRepository hastaneRepository)
        {
            _hemsireRepository = hemsireRepository;
            _hastaneRepository = hastaneRepository;

            hastanes = _hastaneRepository.GetHastaneAsync().GetAwaiter().GetResult();

            Hastanelerim = hastanes.Select(x => x.Adi).ToArray();
        }

        public int HemsireId { get; set; }
        public string HemsireAdi { get; set; }
        public string[] Hastanelerim { get; set; }
        
        public int HastaneSelectedIndex { get; set; }
        public int HemsireHastaneId { get; set; }
        public IEnumerable<Hemsire> Hemsirelerim
        {
            get
            {
                return _hemsireler;
            }
            set
            {
                _hemsireler = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Hemsirelerim = await _hemsireRepository.GetHemsireAsync();
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
                        var hemsire = new Hemsire
                        {
                            Adı = HemsireAdi,
                            HastaneId = hastanes[HastaneSelectedIndex].Id,

                        };
                        await _hemsireRepository.AddHemsireAsync(hemsire);
                        RefreshCommand.Execute("");
                        // Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage()).GetAwaiter();
                    }
                });

            }
        }



    }
}
