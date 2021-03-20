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
    /// <summary>
    /// Логика взаимодействия для AddDeal.xaml
    /// </summary>
    public partial class AddDeal : Window
    {
        public AddDeal()
        {
            InitializeComponent();

            AddToCheck();

        }

        private void AddToCheck()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {
                    var agents = from r in model.Realtors
                                 select new
                                 {
                                     IDAgent = r.ID,
                                     FIO = r.FIO
                                 };

                    var apartaments = from a in model.Apartaments
                                 select a;

                    foreach (var agent in agents)
                    {
                        AgentsCombo.Items.Add(agent.FIO + " - ID: " + agent.IDAgent);

                    }

                    foreach (var apartament in apartaments)
                    {
                        ApartamentsCombo.Items.Add(apartament.StreetName + " - " + apartament.Price);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string STName = ApartamentsCombo.SelectedItem.ToString().Split('-')[0].TrimEnd(' ');
                decimal? price = decimal.Parse(ApartamentsCombo.SelectedItem.ToString().Split('-')[1]);
                int? IDAgent = int.Parse(AgentsCombo.SelectedItem.ToString().Split(':')[1].TrimStart(' '));

                if (STName == "" || IDAgent == null) MessageBox.Show("Не все поля заполненны!");
                else
                {
                    using (ModelBD model = new ModelBD())
                    {

                        var aquareAP = from a in model.Apartaments
                                       where a.StreetName == STName
                                       select new
                                       {
                                           IDAP = a.ID
                                       };

                        int id_ap = 0;
                        foreach (var sq in aquareAP)
                        {
                            id_ap = sq.IDAP;
                        }

                        Dealings dealing = new Dealings();
                        dealing.IDApartamemt = id_ap;
                        dealing.IDRealtor = IDAgent;

                        //model.Entry(dealing).State = System.Data.Entity.EntityState.Modified;
                        model.Dealings.Add(dealing);
                        model.SaveChanges();

                        MessageBox.Show("Сделка успешно добавлена");
                    }
                }

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
