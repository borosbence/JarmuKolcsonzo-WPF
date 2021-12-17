using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace JarmuKolcsonzo.ViewModels
{
    public class JarmuViewModel : ObservableObject
    {
        private GenericRepository<Jarmu, JKContext> jarmuRepo;
        public ObservableCollection<Jarmu> Jarmuvek { get; set; }

        public ICollectionView _jarmuvekCW;
        public ICollectionView JarmuvekCW
        {
            get { return _jarmuvekCW; }
            set { SetProperty(ref _jarmuvekCW, value); }
        }
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            set 
            { 
                SetProperty(ref _searchKey, value);
                JarmuvekCW.Refresh(); 
            }
        }


        public JarmuViewModel(JKContext context)
        {
            jarmuRepo = new JarmuRepository<JKContext>(context);
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuRepo.GetAll());

            _searchKey = string.Empty;
            JarmuvekCW = CollectionViewSource.GetDefaultView(Jarmuvek);
            JarmuvekCW.Filter =  new Predicate<object>(Filter);
        }

        public bool Filter(object obj)
        {
            var search = SearchKey.ToLower();
            Jarmu jarmu = obj as Jarmu;
            return jarmu.rendszam.ToLower().Contains(search) || jarmu.tipus.megnevezes.ToLower().Contains(search);
        }

    }
}