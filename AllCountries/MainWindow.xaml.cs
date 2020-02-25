using AllCountries.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllCountries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbConnection db = new DbConnection();
        DataView names;

        List<Country> ListAllCountries = new List<Country>();

        public MainWindow()
        {
            InitializeComponent();

            dbGrid1.ItemsSource = db.GetAllCountries();

            ListAllCountries = db.ListCountries();
            dbGrid2.ItemsSource = ListAllCountries;

            names = db.GetAllCountries();
            foreach (var name in names)
            {
                dbListbox.Items.Add(name);
                dbCombobox.Items.Add(name);
            }
        }

        private void dbCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedItem = ((DataRowView)dbCombobox.SelectedItem);
            try
            {
                string selectedCountry = selectedItem.Row["omschrijving"].ToString();
                if (selectedCountry.Length > 0)
                {
                    byte[] imageArr = db.GetImageByteArray(selectedCountry);

                    using(MemoryStream ms = new MemoryStream(imageArr))
                    {
                        BitmapImage imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.StreamSource = ms;
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.EndInit();

                        imgFlag.Source = imageSource;
                    }
                }
            }
            catch (Exception)
            {
                //Foutmelding kan je hier afhandelen
            }
        }
    }
}
