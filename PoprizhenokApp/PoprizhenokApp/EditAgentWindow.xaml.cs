using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static PoprizhenokApp.MainWindow;

namespace PoprizhenokApp
{
    /// <summary>
    /// Логика взаимодействия для EditAgentWindow.xaml
    /// </summary>
    public partial class EditAgentWindow : Window
    {
        Agent agent;
        MainWindow window;
        Demo13Entities Db = new Demo13Entities();
        string imagename;
        public EditAgentWindow(AgentsForList _agent,MainWindow _window)
        {
            InitializeComponent();
            
            foreach(Agent ag in Db.Agent)
            {
                if (ag.ID == _agent.Id) agent = ag;
            }
            window = _window;
            CheckAgent();
        }

        private void EditAdd_Click(object sender, RoutedEventArgs e)
        {
            foreach (Agent agent in Db.Agent)
            {
                if (agent.ID == this.agent.ID)
                {
                    try
                    {
                        agent.Title = tbName.Text;
                        agent.Logo = null;
                        agent.AgentType = (AgentType)cbType.SelectedItem;
                        agent.Address = tbAddress.Text;
                        agent.INN = tbINN.Text;
                        agent.KPP = tbKPP.Text;
                        agent.DirectorName = tbNameDir.Text;
                        agent.Logo = imagename;
                        agent.Phone = tbPhone.Text;
                        agent.Email = tbEmail.Text;
                        agent.Priority = int.Parse(tbPrio.Text);

                        if (tbINN.Text.Length != 10)
                        {
                            MessageBox.Show("Инн содержит 10 цифр, проверьте правильность заполнения");
                            return;
                        }
                        if (tbKPP.Text.Length != 9)
                        {
                            MessageBox.Show("Инн содержит 9 цифр, проверьте правильность заполнения");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Какие-то данные не заполнены!", "Предупреждение");
                        return;
                    }
                }
            }
            MessageBox.Show("Агент изменен");
            Db.SaveChanges();
            window.AllFilter();
            this.Close();
        }
        private void CheckAgent()
        {
            cbType.ItemsSource=Db.AgentType.ToList();
            try { ImgLogo.Source = new BitmapImage(new Uri(agent.Logo, UriKind.Relative)); }
            catch { ImgLogo.Source = null; }

            tbName.Text = agent.Title;
            tbAddress.Text = agent.Address;
            tbINN.Text = agent.INN;
            tbKPP.Text = agent.KPP;
            tbNameDir.Text = agent.DirectorName;
            tbPhone.Text = agent.Phone;
            tbEmail.Text = agent.Email;
            tbPrio.Text = agent.Priority.ToString();

            if (agent.AgentType == null)
            {
                cbType.SelectedIndex = 0;
            }
            else
            {
                cbType.SelectedItem = agent.AgentType;
            }
        }

        private void btnChangeImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Files = new OpenFileDialog();
            Files.Filter = "All files (*.*)|*.*|Image Files(*.png; *.JPG)|*.BMP;*.GIF; *.JPG";
            Files.ShowDialog();
            imagename = Files.FileName;
            FileInfo FileInfo = new FileInfo(Files.FileName);
            ImgLogo.Source = new BitmapImage(new Uri(Files.FileName));
            MessageBox.Show("Фото выбрано");
        }

    
    }
}
