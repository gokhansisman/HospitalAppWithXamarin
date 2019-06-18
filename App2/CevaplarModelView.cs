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
    public class CevaplarModelViewModel : INotifyPropertyChanged
    {
        private readonly ISorularRepository _soruRepository;
        private readonly IDoktorRepository _doktorRepository;
        private IEnumerable<Sorular> _sorular;
        private IEnumerable<Doktor> _doktorlar;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // var sad = _sorularRepository.QuerySoruAsync(x => x.Adi=="Gokhan").GetAwaiter().GetResult();
        }
        public CevaplarModelViewModel(ISorularRepository sorularRepository,IDoktorRepository doktorRepository)
        {
            _soruRepository = sorularRepository;
            _doktorRepository = doktorRepository;
            RefreshCommand.Execute("");
        }

        public string[] Doktors { get; set; }

        public IEnumerable<Sorular> Cevaplarim
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

        public int doktorSelectedIndex { get; set; }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    Cevaplarim = _soruRepository.GetSorularAsync().GetAwaiter().GetResult();
                    //Doktors = Cevaplarim.FirstOrDefault(x=>x.DoktorId == )


                });
            }
        }
        
        


    }
}
