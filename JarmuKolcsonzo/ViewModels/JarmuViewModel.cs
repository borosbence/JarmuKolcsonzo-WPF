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
        private GenericRepository<JarmuTipus,JKContext> jarmuTipusRepo;
        private ObservableCollection<Jarmu> _jarmuvek;
        public ObservableCollection<Jarmu> Jarmuvek
        {
            get { return _jarmuvek; }
            set { SetProperty(ref _jarmuvek, value); }
        }
        private Jarmu _selectedJarmu = new Jarmu();
        public Jarmu SelectedJarmu
        {
            get { return _selectedJarmu; }
            set { SetProperty(ref _selectedJarmu, value); }
        }

        public ObservableCollection<JarmuTipus> JarmuTipusok { get; set; }

        public RelayCommand NewCommand { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public JarmuViewModel()
        {
            var context = new JKContext();
            jarmuRepo = new JarmuRepository(context);
            jarmuTipusRepo = new GenericRepository<JarmuTipus,JKContext>(context);
            NewCommand = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedJarmu));
            DeleteCmd = new RelayCommand(() => Delete(SelectedJarmu));
            LoadData();
        }

        protected override void LoadData()
        {
            var query = jarmuRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = jarmuRepo.TotalItems;
            Jarmuvek = new ObservableCollection<Jarmu>(query);
            JarmuTipusok = new ObservableCollection<JarmuTipus>(jarmuTipusRepo.GetAll());
        }

        private void New()
        {
            SelectedJarmu = new Jarmu();
        }

        private void Save(Jarmu jarmu)
        {
            if (jarmuRepo.Exist(jarmu.id))
            {
                jarmuRepo.Update(jarmu);
            }
            else
            {
                jarmuRepo.Insert(jarmu);
                Jarmuvek.Insert(0, jarmu);
            }
        }

        private void Delete(Jarmu jarmu)
        {
            jarmuRepo.Delete(jarmu.id);
            Jarmuvek.Remove(jarmu);
        }
    }
}