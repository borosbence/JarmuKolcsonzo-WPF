using JarmuKolcsonzo.Commands;
using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using JarmuKolcsonzo.Validators;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace JarmuKolcsonzo.ViewModels
{
    public class JarmuViewModel : PagerViewModel, IEditCommands<Jarmu>
    {
        private JarmuRepository jarmuRepo;
        private JarmuTipusRepository jarmuTipusRepo;

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
            set { 
                SetProperty(ref _selectedJarmu, value);
                DeleteCmd.NotifyCanExecuteChanged();
            }
        }

        public List<JarmuTipus> JarmuTipusok { get; set; }

        public RelayCommand<Jarmu> NewCmd { get; }
        public RelayCommand<Jarmu> SaveCmd { get; }
        public RelayCommand<Jarmu> DeleteCmd { get; }

        public JarmuViewModel()
        {
            var context = new JKContext();
            jarmuRepo = new JarmuRepository(context);
            jarmuTipusRepo = new JarmuTipusRepository(context);
            NewCmd = new RelayCommand<Jarmu>((jarmu) => New(jarmu));
            SaveCmd = new RelayCommand<Jarmu>(jarmu => Save(jarmu));
            DeleteCmd = new RelayCommand<Jarmu>(jarmu => Delete(jarmu), CanDelete);
            LoadData();
        }

        protected override void LoadData()
        {
            var query = jarmuRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = jarmuRepo.TotalItems;
            Jarmuvek = new ObservableCollection<Jarmu>(query);
            JarmuTipusok = jarmuTipusRepo.GetAll();
        }

        private void New(Jarmu jarmu)
        {
            jarmu = new Jarmu();
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

        private bool CanDelete(Jarmu jarmu)
        {
            // return jarmu != null;
            if (jarmu != null)
            {
                return jarmu.id > 0;
            }
            return false;
        }

        private void Delete(Jarmu jarmu)
        {
            jarmuRepo.Delete(jarmu.id);
            Jarmuvek.Remove(jarmu);
        }
    }
}