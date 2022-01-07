using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.ViewModels
{
    public abstract class PagerViewModel : ObservableObject
    {
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            set { SetProperty(ref _searchKey, value); page = 1; }
        }

        private string _totalItems;
        public string TotalItemsLabel
        {
            get { return _totalItems; }
            set { SetProperty(ref _totalItems, value); }
        }

        private string _currentPage;
        public string CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); ; }
        }

        // Oldaltördelés
        protected int page = 1;
        protected int itemsPerPage = 20;
        private int pageCount;
        public int TotalItems
        {
            set
            {
                TotalItemsLabel = "Összesen: " + value;
                pageCount = (value - 1) / itemsPerPage + 1;
                CurrentPage = page + "/" + pageCount;
            }
        }

        public RelayCommand LoadDataCmd { get; set; }
        public RelayCommand FirstPageCmd { get; set; }
        public RelayCommand PreviousPageCmd { get; set; }
        public RelayCommand NextPageCmd { get; set; }
        public RelayCommand LastPageCmd { get; set; }

        //private string _sortBy;
        //public string SortBy
        //{
        //    get { return _sortBy; }
        //    set { SetProperty(ref _sortBy, value); }
        //}

        public PagerViewModel()
        {
            LoadDataCmd = new RelayCommand(() => LoadData());
            FirstPageCmd = new RelayCommand(() => FirstPage());
            PreviousPageCmd = new RelayCommand(() => PrevPage());
            NextPageCmd = new RelayCommand(() => NextPage());
            LastPageCmd = new RelayCommand(() => LastPage());
        }

        protected abstract void LoadData();

        protected void FirstPage()
        {
            page = 1;
            LoadData();
        }

        protected void PrevPage()
        {
            if (page != 1)
            {
                page--;
                LoadData();
            }
        }

        protected void NextPage()
        {
            if (page != pageCount)
            {
                page++;
                LoadData();
            }
        }

        protected void LastPage()
        {
            page = pageCount;
            LoadData();
        }
    }
}
