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

namespace PoprizhenokApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Demo13Entities db = new Demo13Entities();
        List<AgentsForList> agentsForLists = new List<AgentsForList>();
        public MainWindow()
        {
            InitializeComponent();
            agentList.Items.Clear();
            AllFilter();
            
           // agentList.Items.Clear();
           // agentList.ItemsSource = agentsForLists;
        }
        public void RefreshListBox()
        {
            agentsForLists.Clear();
            Demo13Entities db = new Demo13Entities();
            foreach(Agent agent in db.Agent)
            {
                
                AgentsForList agentforlist = new AgentsForList();
                foreach(AgentType type in db.AgentType)
                {
                    if (type.ID == agent.AgentTypeID)
                    {
                        agentforlist.TypeName = type.Title + " | " + agent.Title;
                    }
                }
                agentforlist.Name = agent.Title;
                agentforlist.Id = agent.ID;
                agentforlist.Priority = agent.Priority;
                agentforlist.ImageSource = agent.Logo;
                agentforlist.Phone = agent.Phone;
                if (agent.Logo == null)
                {
                    agentforlist.ImageSource = "Resources/picture.png";
                }
                agentsForLists.Add(agentforlist);
           
            }
            
        }
        public class AgentsForList
        {
            public int Id { get; set; }
            public string TypeName { get; set; }
            public int Priority { get; set; }
            public string ImageSource { get; set; }
            public string Phone { get; set; }
            public string Name { get; set; }
        }
        public void AllFilter()
        {
            Demo13Entities db = new Demo13Entities();
            RefreshListBox();
            List<AgentsForList> searchagentlist = new List<AgentsForList>();
            searchagentlist = agentsForLists;
            searchagentlist = searchagentlist.Where(prod => prod.Name.StartsWith(SearchTb.Text) || prod.Phone.StartsWith(SearchTb.Text)).ToList();
            if (ComboBoxSort.SelectedIndex == 2)
            {
                searchagentlist = searchagentlist.OrderBy(prod => prod.Priority).ToList();
            }
            if (ComboBoxSort.SelectedIndex == 1)
            {
                searchagentlist = searchagentlist.OrderByDescending(prod => prod.Priority).ToList();
            }
            agentList.ItemsSource = searchagentlist;

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFilter();
            
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentsForList agent =new AgentsForList();
            agent=(sender as Button).DataContext as AgentsForList;
            EditAgentWindow wind = new EditAgentWindow(agent,this);
            wind.Show();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AgentsForList agent = new AgentsForList();
            agent = (sender as Button).DataContext as AgentsForList;
            AddAgent wind = new AddAgent(this);
            wind.Show();
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilter();
        }
    }
}
