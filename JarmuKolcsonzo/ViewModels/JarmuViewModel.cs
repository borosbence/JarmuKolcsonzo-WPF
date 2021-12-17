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
        private GenericRepository<Jarmu, JKContext> jarmuRepo;
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

        public ICollectionView JarmuvekCW { get; }

        public JarmuViewModel(JKContext context)
        {
            jarmuRepo = new JarmuRepository<JKContext>(context);
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuRepo.GetAll());

            _searchKey = string.Empty;
        }

        public void Search(string searchKey)
        {
            searchKey = searchKey.ToLower();
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuRepo.GetAll().Where(x =>
                x.rendszam.ToLower().Contains(searchKey) ||
                x.tipus.megnevezes.ToLower().Contains(searchKey)));
        }
    }
}