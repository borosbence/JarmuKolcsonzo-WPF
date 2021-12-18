using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace JarmuKolcsonzo.ViewModels
{
    public class JarmuViewModel : ObservableObject
    {
        private JarmuRepository jarmuRepo;
        private ObservableCollection<Jarmu> _jarmuvek;
        public ObservableCollection<Jarmu> Jarmuvek
        {
            get { return _jarmuvek; }
            set { SetProperty(ref _jarmuvek, value); }
        }
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            set {  SetProperty(ref _searchKey, value); Search(value);}
        }

        public JarmuViewModel()
        {
            var context = new JKContext();
            jarmuRepo = new JarmuRepository(context);
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuRepo.GetAll());

            _searchKey = string.Empty;
        }

        public void Search(string searchKey)
        {
            var query = jarmuRepo.GetAll(searchKey);
            Jarmuvek = new ObservableCollection<Jarmu>(query);
        }
    }
}