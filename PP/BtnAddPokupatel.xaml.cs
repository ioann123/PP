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

namespace PP
{
    /// <summary>
    /// Логика взаимодействия для BtnAddPokupatel.xaml
    /// </summary>
    public partial class BtnAddPokupatel : Window
    {
        PPEntities1 context;
        string currentLetter = "";
        public BtnAddPokupatel(PPEntities1 context, Покупатели newpokup)
        {
            InitializeComponent();
            this.context = context;
            this.DataContext = newpokup;
            ShowTable();
            ShowLetters();
        }

        private void ShowLetters()
        {
            for (char i = 'А'; i <= 'Я'; i++)
            {
                TextBlock letter = new TextBlock
                {
                    Text = i.ToString(),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(10, 1, 5, 1)
                };
                letter.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                StackLetters.Children.Add(letter);
            }
        }

        private void ShowTable()
        {
            DataGrid2.ItemsSource = context.Покупатели.ToList();
            if (TxtEmail.Text == null && TxtPhone.Text == null)
                return;
            List<Покупатели> listClient = context.Покупатели.ToList();
            listClient = listClient.Where(x => x.Фамилия.ToLower().Contains(TxtEmail.Text.ToLower())).ToList();
            listClient = listClient.Where(x => x.Телефон.ToLower().Contains(TxtPhone.Text.ToLower())).ToList();
            if (currentLetter.Count() == 1)
            {
                listClient = listClient.Where(x => x.Фамилия.Contains(currentLetter)).ToList();
            }
            DataGrid2.ItemsSource = listClient.OrderBy(x => x.Фамилия).ToList();
        }

        private void BtnAddPokup_Click(object sender, RoutedEventArgs e)
        {
            var NewPokup = new Покупатели();
            context.Покупатели.Add(NewPokup);
            var BtnAddData = new AddPokup(context, NewPokup);
            BtnAddData.ShowDialog();
            ShowTable();
        }

        private void BtnDeletePokup_Click(object sender, RoutedEventArgs e)
        {
            var покупатели = DataGrid2.SelectedItem as Покупатели;
            if (покупатели == null)
            {
                MessageBox.Show("Выберите строку!");
                return;

            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удадить эту строку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                context.Покупатели.Remove(покупатели);
                context.SaveChanges();
                ShowTable();

            }
        }

            private void BtnEditPokup_Click(object sender, RoutedEventArgs e)
        {

            Button AddPokup = sender as Button;
            var currentCar = AddPokup.DataContext as Покупатели;
            var EdiWindow = new AddPokup(context, currentCar);
            EdiWindow.ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var label = (TextBlock)sender;
            currentLetter = label.Text;
            foreach (TextBlock letter in StackLetters.Children)
            {
                letter.Foreground = Brushes.Black;
            }
            label.Foreground = Brushes.Red;
            ShowTable();
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTable();
        }

        private void TxtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTable();
        }
    }
}
