﻿using Autodesk.Revit.UI;
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

namespace RevitAPITrainingUI
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData)
        {
            InitializeComponent();
            MainViewViewModel vm = new MainViewViewModel(commandData);

            //Вариант кода для однократного открытия окна
            vm.CloseRequest += (s, e) => this.Close(); //Закрытие окна

            //Вариант кода для многократного открытия окна
            //vm.HideRequest += (s, e) => this.Hide(); //Скрытие окна
            //vm.ShowRequest += (s, e) => this.Show(); //Показ скрытого окна

            DataContext = vm;
        }
    }
}
