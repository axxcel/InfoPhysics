using System;
using System.Collections;
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
using Microsoft.Data.Sqlite;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using System.Xml;
using System.Drawing;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Button ContentBtn(string title, int id) // стили для динамического создания кнопки
        {
            var bc = new BrushConverter();
            Button btn = new Button
            {
                Content = title,
                FontSize = 16,
                Tag = id,
                Background = (Brush)bc.ConvertFrom("#FFECECED"),
                Padding = new Thickness(0, 10, 0, 10),
                Margin = new Thickness(0, 0, 0, 2),
            };
            btn.Click += OpenSubject;
            ListSubject.Children.Add(btn);
            return btn;
        }

        public void ContentFill(string title, string text, string image) // стили для динамического создания статьи
        {
            TextBlock Head = new TextBlock
            {
                FontSize = 22,
                Text = title,
                Margin = new Thickness(0, 0, 0, 20),
                FontWeight = FontWeights.SemiBold,
            };
            ContentSubject.Children.Add(Head);

            Image img = new Image() { 
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Left,
                Source = new BitmapImage(new Uri("/img/"+ image, UriKind.Relative)),
                Margin = new Thickness(0, 0, 0, 20)
            };
            ContentSubject.Children.Add(img);

            TextBlock Body = new TextBlock
            {
                FontSize = 16,
                Text = text
            };
            ContentSubject.Children.Add(Body);

        }

        public MainWindow()
        {
            InitializeComponent();
            int count;
            using (var con = new SqliteConnection("Data Source=database.db;")) // определяем сколько статей в бд
            {
                con.Open();
                SqliteCommand command = new SqliteCommand("SELECT count (*) FROM articles", con);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            count = reader.GetInt32(0);
                        }
                    }
                }
                con.Close();
            }
            using (var con = new SqliteConnection("Data Source=database.db;")) // создаем необходимое кол-во кнопок динамически
            {
                con.Open();
                SqliteCommand command = new SqliteCommand("SELECT articles.id,(SELECT titles.title FROM titles WHERE titles.idTitle = articles.idTitle) AS title, articles.text ,(SELECT images.image FROM[images] WHERE images.idImg = articles.idImg) AS image FROM [articles]", con);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        int i = 1;
                        while (reader.Read())   // построчно считываем данные
                        {
                            Button btn = ContentBtn(reader.GetString(1), i);
                            i++;
                        }
                    }
                }
                con.Close();
            }
        }
        private void OpenSubject(object sender, RoutedEventArgs e)
        {
            ListSubject.Visibility = Visibility.Collapsed;
            ContentSubject.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;

            var Button = (Button)sender; // получаем сендер нажатой кнопки
            using (var con = new SqliteConnection("Data Source=database.db;"))
            {
                con.Open();
                SqliteCommand command = new SqliteCommand("SELECT articles.id,(SELECT titles.title FROM titles WHERE titles.idTitle = articles.idTitle) AS title, articles.text ,(SELECT images.image FROM[images] WHERE images.idImg = articles.idImg) AS image FROM [articles]", con);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        int i = 1;
                        while (reader.Read())   // построчно считываем данные
                        {
                            if (i == int.Parse(Button.Tag.ToString()))
                            {
                                while(ContentSubject.Children.Count > 0)
                                {
                                    ContentSubject.Children.RemoveAt(0);
                                }

                                ContentFill(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                con.Close();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowCalculate(object sender, RoutedEventArgs e)
        {
            Calculate calculate = new Calculate();
            calculate.Show();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Back.Visibility = Visibility.Collapsed;
            ListSubject.Visibility = Visibility.Visible;
            ContentSubject.Visibility = Visibility.Collapsed;
        }
    }
}
