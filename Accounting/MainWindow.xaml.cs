using Accounting.Model;
using Accounting.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DopDeal> dealList = new List<DopDeal>();
        public MainWindow()
        {
            InitializeComponent();

            AsSetDeal();
        }

        private async void AsSetDeal()
        {
            await Dispatcher.BeginInvoke(new ThreadStart(() => SetDeal()));
        }

        private void SetDeal()
        {
            GridDeal.ItemsSource = null;
            dealList.Clear();
            using (ModelBD model = new ModelBD())
            {
                var queryDeal = from d in model.Dealings
                                join a in model.Apartaments on d.IDApartamemt equals a.ID
                                join r in model.Realtors on d.IDRealtor equals r.ID
                                select new
                                {
                                    FIO = r.FIO,
                                    Square = a.SquareApartament,
                                    Price = a.Price,
                                    Percent = r.Percent,
                                    IDAP = a.ID, 
                                    IDD = d.IDDeal, 
                                    IDR = r.ID
                                };

                foreach (var deal in queryDeal)
                {
                    DopDeal dop = new DopDeal(deal.FIO, deal.Square, deal.Price, deal.Percent, deal.IDAP, deal.IDD, deal.IDR);
                    dealList.Add(dop);
                }

                GridDeal.ItemsSource = dealList;
            }
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e) { this.DragMove(); }
        private void Button_Click(object sender, RoutedEventArgs e) { Application.Current.MainWindow.Close(); }
        private void Button_Click_1(object sender, RoutedEventArgs e) { Application.Current.MainWindow.WindowState = WindowState.Minimized; }

        private void AddDeal(object sender, RoutedEventArgs e)
        {
            AddDeal add = new AddDeal();
            add.ShowDialog();

            AsSetDeal();
        }

        private void AddAgentWindow(object sender, RoutedEventArgs e)
        {
            AddAgent addAgent = new AddAgent();
            addAgent.ShowDialog();
        }

        private void AddApartamentWindow(object sender, RoutedEventArgs e)
        {
            AddApartament addApartament = new AddApartament();
            addApartament.ShowDialog();
        }

        private void GridDeal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {

                    if (GridDeal.SelectedItems.Count > 0)
                    {

                        for (int i = 0; i < GridDeal.SelectedItems.Count; i++)
                        {

                            DopDeal deal = GridDeal.SelectedItems[i] as DopDeal;
                            ChangeWindow change = new ChangeWindow();

                            change.FIOText.Text = deal.FIORieltor;
                            change.PercentText.Text = deal.Percent.ToString();
                            change.FIOComb.SelectedItem = deal.FIORieltor + " - ID: " + deal.IDR;

                            var apartament = from a in model.Apartaments
                                             where a.ID == deal.IDAP
                                             select a;

                            foreach (var item in apartament)
                            {
                                change.STNameText.Text = item.StreetName;
                                change.HouseNumText.Text = item.HouseNumber;
                                change.APNumText.Text = item.ApartamentNumber.ToString();
                                change.SquareText.Text = item.SquareApartament.ToString();
                                change.AmountRoomText.Text = item.AmountRoom.ToString();
                                change.PriceText.Text = item.Price.ToString();

                                change.APComb.SelectedItem = item.StreetName + " - ID: " + item.ID;
                            }

                            if (deal != null)
                            {


                                if (change.ShowDialog() == true)
                                {

                                    Dealings dealings = model.Dealings.Where(p => p.IDDeal == deal.IDD).FirstOrDefault();
                                    dealings.IDRealtor = change.GetAgentID();
                                    dealings.IDApartamemt = change.GetAPID();

                                    model.Entry(dealings).State = System.Data.Entity.EntityState.Modified;

                                }
                                model.SaveChanges();
                                SetDeal();
                            }

                        }

                    }         
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteDeal_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {

                    if (GridDeal.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < GridDeal.SelectedItems.Count; i++)
                        {
                            DopDeal idDeal = GridDeal.SelectedItems[i] as DopDeal;
                            if (idDeal != null)
                            {
                                MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo);
                                switch (result)
                                {
                                    case MessageBoxResult.Yes:
                                        Dealings id = model.Dealings.Where(p => p.IDDeal == idDeal.IDD).FirstOrDefault();
                                        model.Dealings.Remove(id);
                                        break;
                                    case MessageBoxResult.No:
                                        break;
                                }                                
                            }
                        }
                        model.SaveChanges();
                        AsSetDeal();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
