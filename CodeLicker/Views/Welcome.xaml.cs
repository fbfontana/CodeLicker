﻿using System;
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
using CodeLicker.Helpers;
using System.Configuration;
using System.IO;
using System.ComponentModel;
using System.Windows.Xps.Packaging;

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string myFile = System.IO.Path.Combine(applicationDirectory, ConfigurationManager.AppSettings["WelcomeUrl"]);

            //WelcomePage.Navigate(new Uri( myFile));
            XpsDocument doc = new XpsDocument(myFile, FileAccess.Read);

            WelcomePage.Document = doc.GetFixedDocumentSequence();
        }
    }
}
