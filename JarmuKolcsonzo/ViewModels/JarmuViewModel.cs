using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace JarmuKolcsonzo.ViewModels
{
    public class JarmuViewModel : PagerViewModel
    {
        private JarmuRepository jarmuRepo;
        private ObservableCollection<Jarmu> _jarmuvek;
        public ObservableCollection<Jarmu> Jarmuvek
        {
            get { return _jarmuvek; }
            set { SetProperty(ref _jarmuvek, value); }
        }
        private Jarmu _selectedJarmu;
        public Jarmu SelectedJarmu
        {
            get { return _selectedJarmu; }
            set { SetProperty(ref _selectedJarmu, value); }
        }

        public JarmuViewModel()
        {
            var context = new JKContext();
            jarmuRepo = new JarmuRepository(context);
            LoadData();
        }

        protected override void LoadData()
        {
            var query = jarmuRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = jarmuRepo.TotalItems;
            Jarmuvek = new ObservableCollection<Jarmu>(query);
        }
    }
}