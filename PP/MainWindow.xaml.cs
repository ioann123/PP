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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PPEntities1 context;
        public MainWindow()
        {
            InitializeComponent();
            context = new PPEntities1();
            ShowTable();
        }

        private void ShowTable()
        {
            DataGrid1.ItemsSource = context.Сделки.ToList();
        }

        private void BtnAddData_Click(object sender, RoutedEventArgs e)
        {
            var NewSdelka = new Сделки();
            context.Сделки.Add(NewSdelka);
            var BtnAddData = new BtnAddSdelka(context, NewSdelka);
            BtnAddData.ShowDialog();
            ShowTable();
        }

        private void BtnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            var сделки = DataGrid1.SelectedItem as Сделки;
            if (сделки == null)
            {
                MessageBox.Show("Выберите строку!");
                return;

            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удадить эту строку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                context.Сделки.Remove(сделки);
                context.SaveChanges();
                ShowTable();
            }
        }

        private void BtnEditData_Click(object sender, RoutedEventArgs e)
        {
            Button BtnEdit = sender as Button;
            var currentCar = BtnEdit.DataContext as Сделки;
            var EdiWindow = new BtnAddSdelka(context, currentCar);
            EdiWindow.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnPokup_Click(object sender, RoutedEventArgs e)
        {
            Button BtnPokup = sender as Button;
            var currentCar = BtnPokup.DataContext as Покупатели;
            var EdiWindow = new BtnAddPokupatel(context, currentCar);
            EdiWindow.ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
