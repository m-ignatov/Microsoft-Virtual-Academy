using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Windows.Input;
using DailyRituals.Commands;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace DailyRituals.DataModel
{
    public class Ritual : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<DateTime> Dates { get; set; }

        [IgnoreDataMember]
        public ICommand CompletedCommand { get; set; }

        public Ritual()
        {
            CompletedCommand = new CompletedButtonClick();
            Dates = new ObservableCollection<DateTime>();
        }

        public void AddDate()
        {            
            Dates.Add(DateTime.Today);
            NotifyPropertyChanged("Dates");
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class DataSource
    {
        private ObservableCollection<Ritual> _rituals;
        const string fileName = "rituals.json";

        public DataSource()
        {
            _rituals = new ObservableCollection<Ritual>();
        }

        public async Task<ObservableCollection<Ritual>> GetResults()
        {
            await ensureDataLoaded();
            return _rituals;
        }

        private async Task ensureDataLoaded()
        {
            if (_rituals.Count != 0) return;

            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Ritual>));

            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName))
                {
                    _rituals = (ObservableCollection<Ritual>)jsonSerializer.ReadObject(stream);
                }
            }
            catch
            {
                _rituals = new ObservableCollection<Ritual>();
            }
        }
        
        public async void AddRitual (string name, string description)
        {
            var ritual = new Ritual();
            ritual.Name = name;
            ritual.Description = description;
            ritual.Dates = new ObservableCollection<DateTime>();

            _rituals.Add(ritual);
            await saveRitualDataAsync();
        }

        private async Task saveRitualDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Ritual>));

            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(fileName,CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _rituals);
            }
        }

        public async void CompleteRitualToday(Ritual ritual)
        {
            int index = _rituals.IndexOf(ritual);
            _rituals[index].AddDate();
            await saveRitualDataAsync();
        }
    }
}
