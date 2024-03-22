using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using Недвижимость.Data;

namespace Недвижимость
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditClient.xaml
    /// </summary>
    public partial class AddOrEditClient : Window
    {
        RealEstateEntities DB = new RealEstateEntities();
        public AddOrEditClient()
        {
            InitializeComponent();
            TitleWindow.Text = "Добавление нового клиента";
            var lastId = DB.Clients.Max(f => (int?)f.Id) ?? 0;
            IdTB.Text = (lastId + 1).ToString();
        }
        public AddOrEditClient(Clients clients)
        {
            InitializeComponent();
            TitleWindow.Text = "Редактирование данных клиента";
            IdTB.Text = clients.Id.ToString();
            FirstNameTB.Text = clients.FirstName;
            MiddleNameTB.Text = clients.MiddleName;
            LastNameTB.Text = clients.LastName;
            EmailTB.Text = clients.Email;
            PhoneTB.Text = clients.Phone;
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(FirstNameTB.Text))
                {
                    MessageBox.Show("Необходимо указать фамилию", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(MiddleNameTB.Text))
                {
                    MessageBox.Show("Необходимо указать имя", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(LastNameTB.Text))
                {
                    MessageBox.Show("Необходимо указать отчество", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var client = new Clients
                {
                    Id = Convert.ToInt32(IdTB.Text),
                    FirstName = FirstNameTB.Text,
                    MiddleName = MiddleNameTB.Text,
                    LastName = LastNameTB.Text,
                    Email = EmailTB.Text,
                    Phone = PhoneTB.Text
                };
                DB.Clients.AddOrUpdate(client);
                DB.SaveChanges();
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
