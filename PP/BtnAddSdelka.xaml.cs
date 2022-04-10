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
    /// Логика взаимодействия для BtnAddSdelka.xaml
    /// </summary>
    public partial class BtnAddSdelka : Window
    {
        PPEntities1 context;
        public BtnAddSdelka(PPEntities1 context, Сделки newSdelka)
        {
            InitializeComponent();
            this.context = context;
            CmbKK.ItemsSource = context.Квартиры.ToList();
            CmbR.ItemsSource = context.Риэлторы.ToList();
            CmbP.ItemsSource = context.Покупатели.ToList();
            this.DataContext = newSdelka;
        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            this.Close();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
