using MapNotes.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MapNotes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMapNote : Page
    {
        private MapNote mapNote;
        private bool isViewing = false;

        public AddMapNote()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Geopoint myPoint;

            if (e.Parameter == null)
            {
                isViewing = false;
                var locator = new Geolocator();
                locator.DesiredAccuracyInMeters = 50;

                var position = await locator.GetGeopositionAsync();
                myPoint = position.Coordinate.Point;
            }

            else
            {
                isViewing = true;
                mapNote = (MapNote)e.Parameter;
                titleTextBox.Text = mapNote.Title;
                noteTextBox.Text = mapNote.Note;
                addButton.Content = "Delete";

                var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
                myPosition.Latitude = mapNote.Latitude;
                myPosition.Longitude = mapNote.Longitude;

                myPoint = new Geopoint(myPosition);
            }

            await myMap.TrySetViewAsync(myPoint, 16D);
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (isViewing)
            {
                var msg = new Windows.UI.Popups.MessageDialog("Are you sure?");
                msg.Commands.Add(new UICommand(
                    "Delete",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                msg.Commands.Add(new UICommand(
                     "Cancel",
                     new UICommandInvokedHandler(this.CommandInvokedHandler)));
                msg.DefaultCommandIndex = 0;
                msg.CancelCommandIndex = 1;
                await msg.ShowAsync();
            }

            else
            {
                MapNote newMapNote = new MapNote();
                newMapNote.Title = titleTextBox.Text;
                newMapNote.Note = noteTextBox.Text;
                newMapNote.Created = DateTime.Now;
                newMapNote.Latitude = myMap.Center.Position.Latitude;
                newMapNote.Longitude = myMap.Center.Position.Longitude;
                App.DataModel.AddMapNote(newMapNote);
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Delete")
            {
                App.DataModel.DeleteMapNote(mapNote);
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
