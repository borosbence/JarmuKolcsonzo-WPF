using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JarmuKolcsonzo.ViewModels
{
    public class UgyfelViewModel : PagerViewModel
    {
        private UgyfelRepository ugyfelRepo;
        private ObservableCollection<Ugyfel> _ugyfelek;
        public ObservableCollection<Ugyfel> Ugyfelek
        {
            get { return _ugyfelek; }
            set { SetProperty(ref _ugyfelek, value); }
        }
        private Ugyfel _selectedUgyfel = new Ugyfel();
        public Ugyfel SelectedUgyfel
        {
            get { return _selectedUgyfel; }
            set { SetProperty(ref _selectedUgyfel, value); }
        }

        public RelayCommand NewCommand { get; set; }
        public RelayCommand SaveCmd { get; set; }
        public RelayCommand DeleteCmd { get; set; }

        public UgyfelViewModel()
        {
            var context = new JKContext();
            ugyfelRepo = new UgyfelRepository(context);
            NewCommand = new RelayCommand(() => New());
            SaveCmd = new RelayCommand(() => Save(SelectedUgyfel));
            DeleteCmd = new RelayCommand(() => Delete(SelectedUgyfel));
            LoadData();
        }

        protected override void LoadData()
        {
            var query = ugyfelRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = ugyfelRepo.TotalItems;
            Ugyfelek = new ObservableCollection<Ugyfel>(query);
        }

        private void New()
        {
            SelectedUgyfel = new Ugyfel();
        }

        private void Save(Ugyfel ugyfel)
        {
            if (ugyfelRepo.Exist(ugyfel.id))
            {
                ugyfelRepo.Update(ugyfel);
            }
            else
            {
                ugyfelRepo.Insert(ugyfel);
                Ugyfelek.Insert(0, ugyfel);
            }
        }

        private void Delete(Ugyfel ugyfel)
        {
            ugyfelRepo.Delete(ugyfel.id);
            Ugyfelek.Remove(ugyfel);
        }
    }
}