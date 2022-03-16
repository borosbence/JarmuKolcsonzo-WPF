using JarmuKolcsonzo.Commands;
using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace JarmuKolcsonzo.ViewModels
{
    public class UgyfelViewModel : PagerViewModel, IEditCommands<Ugyfel>
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
            set { SetProperty(ref _selectedUgyfel, value); DeleteCmd.NotifyCanExecuteChanged(); }
        }

        public RelayCommand<Ugyfel> NewCmd { get; }
        public RelayCommand<Ugyfel> SaveCmd { get; }
        public RelayCommand<Ugyfel> DeleteCmd { get; }

        public UgyfelViewModel()
        {
            var context = new JKContext();
            ugyfelRepo = new UgyfelRepository(context);
            NewCmd = new RelayCommand<Ugyfel>(New);
            SaveCmd = new RelayCommand<Ugyfel>(ugyfel => Save(ugyfel));
            DeleteCmd = new RelayCommand<Ugyfel>(ugyfel => Delete(ugyfel), CanDelete);
            LoadData();
        }

        protected override void LoadData()
        {
            var query = ugyfelRepo.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = ugyfelRepo.TotalItems;
            Ugyfelek = new ObservableCollection<Ugyfel>(query);
        }

        private void New(Ugyfel ugyfel)
        {
            ugyfel = new Ugyfel();
            SelectedUgyfel = ugyfel;
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

        private bool CanDelete(Ugyfel ugyfel)
        {
            // return ugyfel != null;
            if (ugyfel != null)
            {
                return ugyfel.id > 0;
            }
            return false;
        }

        private void Delete(Ugyfel ugyfel)
        {
            ugyfelRepo.Delete(ugyfel.id);
            Ugyfelek.Remove(ugyfel);
        }
    }
}