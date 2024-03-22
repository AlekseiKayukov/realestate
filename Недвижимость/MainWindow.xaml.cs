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
using Недвижимость.Data;

namespace Недвижимость
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RealEstateEntities DB = new RealEstateEntities();
        public MainWindow()
        {
            InitializeComponent();
            ClientsDG.ItemsSource = DB.Clients.ToList();
            AgentsDG.ItemsSource = DB.Agents.ToList();
        }

        private void DeleteClientBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?"
                    , "Вопрос", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    var selectRow = (Clients)ClientsDG.SelectedItem;
                    if (selectRow != null)
                    {
                        DB.Clients.Remove(selectRow);
                        DB.SaveChanges();
                        DB = new RealEstateEntities();
                        ClientsDG.ItemsSource = DB.Clients.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Строка не выбрана", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nВозможно вы пытаетесь" +
                    " удалить данные клиента, который имеется в записях",
                    "Ошибка удаления",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAgentBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?"
                    , "Вопрос", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    var selectRow = (Agents)AgentsDG.SelectedItem;
                    if (selectRow != null)
                    {
                        DB.Agents.Remove(selectRow);
                        DB.SaveChanges();
                        DB = new RealEstateEntities();
                        AgentsDG.ItemsSource = DB.Clients.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Строка не выбрана", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + 
                    "\nВозможно вы пытаетесь удалить данные риэлтора, который" +
                    " имеется в записях", "Ошибка удаления", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditAgentBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectRow = (Agents)AgentsDG.SelectedItem;
                if (selectRow != null)
                {
                    var addOrEditAgent = new AddOrEditAgent(selectRow);
                    addOrEditAgent.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Строка не выбрана", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddAgentBT_Click(object sender, RoutedEventArgs e)
        {
            var addOrEditAgent = new AddOrEditAgent();
            addOrEditAgent.Show();
            this.Close();
        }

        private void AddClientBT_Click(object sender, RoutedEventArgs e)
        {
            var addOrEditClient = new AddOrEditClient();
            addOrEditClient.Show();
            this.Close();
        }

        private void EditClientBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectRow = (Clients)ClientsDG.SelectedItem;
                if (selectRow != null)
                {
                    var addOrEditClient = new AddOrEditClient(selectRow);
                    addOrEditClient.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Строка не выбрана", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchClientTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtered = DB.Clients.ToList();
            var resultFilter = filtered.Where(x => x.GetType().GetProperties().
            Any(p => p.GetValue(x, null)?.ToString().ToLower().
            Contains(SearchClientTB.Text) ?? false)).ToList();
            ClientsDG.ItemsSource = resultFilter;
            if (ClientsDG.Items.Count == 1)
            {
                ResultClientSearch.Text = "По результатам поиска ничего не найдено.";
            }
            else
            {
                ResultClientSearch.Text = "";
            }
        }

        private void SearchAgentTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtered = DB.Agents.ToList();
            var resultFilter = filtered.Where(x => x.GetType().GetProperties().
            Any(p => p.GetValue(x, null)?.ToString().ToLower().
            Contains(SearchClientTB.Text) ?? false)).ToList();
            AgentsDG.ItemsSource = resultFilter;
            if (AgentsDG.Items.Count == 1)
            {
                ResultAgentSearch.Text = "По результатам поиска ничего не найдено.";
            }
            else
            {
                ResultAgentSearch.Text = "";
            }
        }
    }
}
