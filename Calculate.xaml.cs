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

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для Calculate.xaml
    /// </summary>
    public partial class Calculate : Window
    {
        public Calculate()
        {
            InitializeComponent();
        }

        private void BtnCalculate(object sender, RoutedEventArgs e)
        {
            if(ValueU != null && !string.IsNullOrWhiteSpace(ValueU.Text) && ValueR != null && ValueR.Text != "0" && !string.IsNullOrWhiteSpace(ValueR.Text))
            {
                float i = float.Parse(ValueU.Text) / float.Parse(ValueR.Text);
                ValueI.Text = i.ToString();

            }
            else if(ValueU != null && !string.IsNullOrWhiteSpace(ValueU.Text) && ValueI != null && !string.IsNullOrWhiteSpace(ValueI.Text))
            {
                float r = float.Parse(ValueU.Text) / float.Parse(ValueI.Text);
                ValueR.Text = r.ToString();
            }
            else if (ValueR != null && !string.IsNullOrWhiteSpace(ValueR.Text) && ValueI != null && !string.IsNullOrWhiteSpace(ValueI.Text))
            {
                float u = float.Parse(ValueI.Text) * float.Parse(ValueR.Text);
                ValueU.Text = u.ToString();
            }
            else
                wrong.Visibility = Visibility.Visible;
        }
        private void ValueU_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrong.Visibility = Visibility.Hidden;
        }
        private void ValueR_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrong.Visibility = Visibility.Hidden;
        }
        private void ValueI_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrong.Visibility = Visibility.Hidden;
        }
        private void Converting(object sender, RoutedEventArgs e)
        {
            if (Combo.Text == "Килоампер" && Origin1 != null && !string.IsNullOrWhiteSpace(Origin1.Text))
            {
                long k = long.Parse(Origin1.Text) * 1000;
                Convert1.Text = k.ToString();
            }
            else if (Combo.Text == "Миллиампер" && Origin1 != null && !string.IsNullOrWhiteSpace(Origin1.Text))
            {
                long k = long.Parse(Origin1.Text) / 1000;
                Convert1.Text = k.ToString();
            }
            else if (Combo.Text == "Микроампер" && Origin1 != null && !string.IsNullOrWhiteSpace(Origin1.Text))
            {
                long k = long.Parse(Origin1.Text) / 1000000;
                Convert1.Text = k.ToString();
            }
        }
    }
}
