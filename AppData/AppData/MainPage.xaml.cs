using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AppData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private const string JSONFILENAME = "data.json";

        private async void writeButton_Click(object sender, RoutedEventArgs e)
        {
            await writeJsonAsync();
        }

        private async void readButton_Click(object sender, RoutedEventArgs e)
        {
            await deserializeJsonAsync();
        }   

        private List<Car> buildObjectGraph()
        {
            var myCars = new List<Car>();

            myCars.Add(new Car() { Id = 1, Make = "Porsche", Model = "911", Year = 2014 });
            myCars.Add(new Car() { Id = 2, Make = "Renault", Model = "GT", Year = 1995 });
            myCars.Add(new Car() { Id = 3, Make = "Peugeot", Model = "207", Year = 2007 });

            return myCars;
        }

        private async Task writeJsonAsync()
        {
            // Notice that the write is ALMOST identical ... except for the serializer.

            var myCars = buildObjectGraph();

            var serializer = new DataContractJsonSerializer(typeof(List<Car>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          JSONFILENAME,
                          CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, myCars);
            }

            resultTextBlock.Text = "Write succeeded";
        }

        private async Task deserializeJsonAsync()
        {
            string content = String.Empty;

            List<Car> myCars;
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Car>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);

            myCars = (List<Car>)jsonSerializer.ReadObject(myStream);

            foreach (var car in myCars)
            {
                content += String.Format("{0}. {1} {2}\n", car.Id, car.Make, car.Model);
            }

            resultTextBlock.Text = content;
        }
    }
}
