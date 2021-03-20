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

namespace Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для AddApartament.xaml
    /// </summary>
    public partial class AddApartament : Window
    {
        public AddApartament()
        {
            InitializeComponent();
        }

        private void ClearTextBox()
        {
            STNameText.Text = "";
            HousNumText.Text = "";
            APNumText.Text = "";
            SquareText.Text = "";
            AmountRoomText.Text = "";
            PriceText.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {

                    Apartaments apartaments = new Apartaments
                    {
                        StreetName = STNameText.Text,
                        HouseNumber = HousNumText.Text,
                        ApartamentNumber = int.Parse(APNumText.Text),
                        SquareApartament = double.Parse(SquareText.Text),
                        AmountRoom = int.Parse(AmountRoomText.Text),
                        Price = decimal.Parse(PriceText.Text)
                    };

                    model.Apartaments.Add(apartaments);
                    model.SaveChanges();

                    MessageBox.Show("Квартира успешно добавлена");

                    ClearTextBox();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
