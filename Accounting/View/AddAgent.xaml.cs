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
    /// Логика взаимодействия для AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        public AddAgent()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddAgentIntoBD(object sender, RoutedEventArgs e)
        {
            try
            {
                string _fio = FIOText.Text;
                int? _percent = int.Parse(PercentText.Text);

                if (_fio == "" && _percent == null || _fio != "" && _percent == null || _fio == "" && _percent != null) 
                    MessageBox.Show("Не все поля заполненны!");
                else
                {
                    using (ModelBD model = new ModelBD())
                    {
                        Realtors realtors = new Realtors
                        {
                            FIO = _fio,
                            Percent = _percent
                        };

                        model.Realtors.Add(realtors);
                        model.SaveChanges();

                        MessageBox.Show("Агент успешно добавлен");
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ExaptionPrecent(string precent, string exText)
        {
            MessageBox.Show($"Процент не может быть {exText} {precent}!");
            PercentText.Text = precent;
        }

        private void FIOText_KeyDown(object sender, KeyEventArgs e)
        {
            CheckingTextBox.CheckButtPress(e, true, false);
        }
    }
}
