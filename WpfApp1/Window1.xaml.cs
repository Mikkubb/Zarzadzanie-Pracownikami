using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //Database1Entities db = new Database1Entities();
            //var docs = from d in db.Pracownicies
            //           select new
            //           {
            //               Imie = d.Imie,
            //               Nazwisko = d.Nazwisko

            //            };

            //foreach (var item in docs)
            //{
            //    Console.WriteLine(item.Imie);
            //    Console.WriteLine(item.Nazwisko); 
            //}

            //this.gridPracownicy.ItemsSource = docs.ToList();
            Database1Entities db = new Database1Entities();
            this.gridPracownicy.ItemsSource = db.Pracownicies.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            Pracownicy pracownikObject = new Pracownicy()
            {
                Imie = txtImie.Text,
                Nazwisko = txtNazwisko.Text,
                Telefon = txtTelefon.Text,
                Adres_Zamieszkania = txtAdresZamieszkania.Text
            };

            db.Pracownicies.Add(pracownikObject);
            db.SaveChanges();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            this.gridPracownicy.ItemsSource = db.Pracownicies.ToList();
        }

        private int updatingPracownikID = 0;
        private void gridPracownicy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridPracownicy.SelectedIndex >= 0)
            {
                if (this.gridPracownicy.SelectedItems.Count >= 0)
                {
                    if (this.gridPracownicy.SelectedItems[0].GetType() == typeof(Pracownicy))
                    {
                        Pracownicy d = (Pracownicy)this.gridPracownicy.SelectedItems[0];
                        this.txtImie2.Text = d.Imie;
                        this.txtNazwisko2.Text = d.Nazwisko;
                        this.txtTelefon2.Text = d.Telefon;
                        this.txtAdresZamieszkania2.Text = d.Adres_Zamieszkania;
                        this.updatingPracownikID = d.Id;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            var r = from d in db.Pracownicies
                    where d.Id == this.updatingPracownikID
                    select d;

            Pracownicy obj = r.SingleOrDefault();

            if(obj != null)
            {
                obj.Imie = this.txtImie2.Text;
                obj.Nazwisko = this.txtNazwisko2.Text;
                obj.Telefon = this.txtTelefon2.Text;
                obj.Adres_Zamieszkania = this.txtAdresZamieszkania2.Text;
                db.SaveChanges();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy napewno chcesz usunąć?", "Usuń Pracownika", MessageBoxButton.YesNo,
                MessageBoxImage.Warning, MessageBoxResult.No);

            if(msgBoxResult == MessageBoxResult.Yes) {

            Database1Entities db = new Database1Entities();
            var r = from d in db.Pracownicies
                    where d.Id == this.updatingPracownikID
                    select d;

            Pracownicy obj = r.SingleOrDefault();

            if (obj != null)
            {
                db.Pracownicies.Remove(obj);
                db.SaveChanges();
            }
            }

        }
    }
}
