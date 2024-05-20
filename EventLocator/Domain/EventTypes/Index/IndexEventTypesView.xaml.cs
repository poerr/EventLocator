﻿using Microsoft.Win32;
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

namespace EventLocator.Domain.EventTypes.Index
{
    /// <summary>
    /// Interaction logic for IndexEventTypesView.xaml
    /// </summary>
    public partial class IndexEventTypesView : Page
    {
        public IndexEventTypesView()
        {
            DataContext = new IndexEventTypesViewModel();
            InitializeComponent();
        }
    }
}
