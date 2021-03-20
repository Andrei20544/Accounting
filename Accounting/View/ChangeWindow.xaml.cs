using Accounting.Model;
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
    public partial class ChangeWindow : Window
    {
        public ChangeWindow()
        {
            InitializeComponent();

            APComb.Items.Clear();
            FIOComb.Items.Clear();
            SetComboBox();          

        }

        public void SetComboBox()
        {
            try
            {               

                using (ModelBD model = new ModelBD())
                {

                    var agents = from ag in model.Realtors
                                 select ag;
                    var apartaments = from ap in model.Apartaments
                                      select ap;

                    foreach (var agent in agents)
                    {
                        FIOComb.Items.Add(agent.FIO + " - ID: " + agent.ID);
                    }
                    foreach (var apartament in apartaments)
                    {
                        APComb.Items.Add(apartament.StreetName + " - ID: " + apartament.ID);
                    }

                }

            }
            catch(Exception ex)
            {

            }
        }

        private void BordTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AllSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void SetChangeComboBox(Realtors realtors, Apartaments apartament)
        {
            try
            {

                APComb.Items.Clear();
                FIOComb.Items.Clear();

                SetComboBox();

                using (ModelBD model = new ModelBD())
                {
                    FIOComb.SelectedItem = realtors.FIO + " - ID: " + realtors.ID;
                    APComb.SelectedItem = apartament.StreetName + " - ID: " + apartament.ID;
                }

            }
            catch(Exception ex)
            {
                
            }
        }

        private void SaveAP(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {
                    int? idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                    Apartaments apartament = model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault();
                    int? idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                    Realtors realtors = model.Realtors.Where(p => p.ID == idComb).FirstOrDefault();

                    apartament.StreetName = STNameText.Text;
                    apartament.HouseNumber = HouseNumText.Text;
                    apartament.ApartamentNumber = int.Parse(APNumText.Text);
                    apartament.SquareApartament = double.Parse(SquareText.Text);
                    apartament.AmountRoom = int.Parse(AmountRoomText.Text);
                    apartament.Price = decimal.Parse(PriceText.Text);

                    model.Entry(apartament).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    SetChangeComboBox(realtors, apartament);

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveAgent(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {
                    int? idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                    Realtors realtors = model.Realtors.Where(p => p.ID == idComb).FirstOrDefault();
                    int? idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                    Apartaments apartament = model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault();

                    realtors.FIO = FIOText.Text;
                    realtors.Percent = int.Parse(PercentText.Text);

                    model.Entry(realtors).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    SetChangeComboBox(realtors, apartament);

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FIOComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (APComb.SelectedItem != null && FIOComb.SelectedItem != null)
                {
                    using (ModelBD model = new ModelBD())
                    {
                        int? idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                        Realtors realtors = model.Realtors.Where(p => p.ID == idComb).FirstOrDefault();

                        FIOText.Text = realtors.FIO;
                        PercentText.Text = realtors.Percent.ToString();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void APComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (APComb.SelectedItem != null && FIOComb.SelectedItem != null)
                {
                    using (ModelBD model = new ModelBD())
                    {
                        int? idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                        Apartaments apartament = model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault();

                        STNameText.Text = apartament.StreetName;
                        HouseNumText.Text = apartament.HouseNumber;
                        APNumText.Text = apartament.ApartamentNumber.ToString();
                        SquareText.Text = apartament.SquareApartament.ToString();
                        AmountRoomText.Text = apartament.AmountRoom.ToString();
                        PriceText.Text = apartament.Price.ToString();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int GetAgentID()
        {

            using (ModelBD model = new ModelBD())
            {
                int idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                return model.Realtors.Where(p => p.ID == idComb).FirstOrDefault().ID;
            }

        }
        public int GetAPID()
        {

            using (ModelBD model = new ModelBD())
            {
                int idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                return model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault().ID;
            }

        }

        private void FIOText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, true, false);
        }

        private void PercentText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void PercentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int? _percent = 0;

                if (PercentText.Text != "") _percent = int.Parse(PercentText.Text);

                if (_percent > 100)
                    ExaptionPrecent("100", "больше");
                else if (_percent < 0)
                    ExaptionPrecent("0", "меньше");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExaptionPrecent(string precent, string exText)
        {
            MessageBox.Show($"Процент не может быть {exText} {precent}!");
            PercentText.Text = precent;
        }

        private void STNameText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, true, false);
        }

        private void HouseNumText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void APNumText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void SquareText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void AmountRoomText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void PriceText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, false, true);
        }

        private void DeleteAgent(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {
                    int idAgent = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);

                    Realtors realtor = model.Realtors.Where(p => p.ID == idAgent).FirstOrDefault();
                    model.Realtors.Remove(realtor);

                    model.SaveChanges();

                    int? idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                    Realtors realtors = model.Realtors.Where(p => p.ID == idComb).FirstOrDefault();
                    int? idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                    Apartaments apartament = model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault();

                    SetChangeComboBox(realtors, apartament);
                    ClearTextBoxAgent();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBoxAP()
        {
            STNameText.Text = "";
            HouseNumText.Text = "";
            APNumText.Text = "";
            SquareText.Text = "";
            AmountRoomText.Text = "";
            PriceText.Text = "";
        }
        private void ClearTextBoxAgent()
        {
            FIOText.Text = "";
            PercentText.Text = "";
        }

        private void DeleteAP(object sender, RoutedEventArgs e)
        {
            try
            {

                using (ModelBD model = new ModelBD())
                {
                    int idAP = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);

                    Apartaments apartaments = model.Apartaments.Where(p => p.ID == idAP).FirstOrDefault();
                    model.Apartaments.Remove(apartaments);

                    model.SaveChanges();

                    int? idComb = int.Parse(FIOComb.SelectedItem.ToString().Split(':')[1]);
                    Realtors realtors = model.Realtors.Where(p => p.ID == idComb).FirstOrDefault();
                    int? idAPart = int.Parse(APComb.SelectedItem.ToString().Split(':')[1]);
                    Apartaments apartament = model.Apartaments.Where(p => p.ID == idAPart).FirstOrDefault();

                    SetChangeComboBox(realtors, apartament);
                    ClearTextBoxAP();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
