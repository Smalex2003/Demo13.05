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

namespace PoprizhenokApp
{
    /// <summary>
    /// Логика взаимодействия для AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        Demo13Entities Db=new Demo13Entities();
        string imagename;
        MainWindow wind;
        public AddAgent(MainWindow _wind)
        {
            InitializeComponent();
            cbType.ItemsSource = Db.AgentType.ToList();
            wind=_wind;
            
        }
        private void btnChangeImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Files = new OpenFileDialog();
            Files.Filter = "All files (*.*)|*.*|Image Files(*.png; *.JPG)|*.BMP;*.GIF; *.JPG";
            Files.ShowDialog();
            imagename=Files.FileName;
            FileInfo FileInfo = new FileInfo(Files.FileName);
            ImgLogo.Source= new BitmapImage(new Uri(Files.FileName));
            MessageBox.Show("Фото выбрано");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Agent agent = new Agent();
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
                Db.Agent.Add(agent);
            MessageBox.Show("Агент добавлен");
            Db.SaveChanges();
            wind.AllFilter();
            this.Close();
            
        }

    }
}
