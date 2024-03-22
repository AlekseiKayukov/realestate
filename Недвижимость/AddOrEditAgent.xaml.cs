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
    /// Логика взаимодействия для AddOrEditAgent.xaml
    /// </summary>
    public partial class AddOrEditAgent : Window
    {
        RealEstateEntities DB = new RealEstateEntities();
        public AddOrEditAgent()
        {
            InitializeComponent();
            TitleWindow.Text = "Добавление нового риэлтора";
            var lastId = DB.Agents.Max(f => (int?)f.Id) ?? 0;
            IdTB.Text = (lastId + 1).ToString();
        }
        public AddOrEditAgent(Agents agents)
        {
            InitializeComponent();
            TitleWindow.Text = "Редактирование данных риэлтора";
            IdTB.Text = agents.Id.ToString();
            FirstNameTB.Text = agents.FirstName;
            MiddleNameTB.Text = agents.MiddleName;
            LastNameTB.Text = agents.LastName;
            DealShareTB.Text = agents.DealShare.ToString();
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
                if (string.IsNullOrEmpty(DealShareTB.Text))
                {
                    MessageBox.Show("Необходимо указать долю с продаж", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Convert.ToInt32(DealShareTB.Text) < 0 ||
                    Convert.ToInt32(DealShareTB.Text) > 100)
                {
                    MessageBox.Show("Доля продаже не может быть меньше 0 " +
                        "и больше 100", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var agent = new Agents
                {
                    Id = Convert.ToInt32(IdTB.Text),
                    FirstName = FirstNameTB.Text,
                    MiddleName = MiddleNameTB.Text,
                    LastName = LastNameTB.Text,
                    DealShare = Convert.ToInt32(DealShareTB.Text)
                };
                DB.Agents.AddOrUpdate(agent);
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
