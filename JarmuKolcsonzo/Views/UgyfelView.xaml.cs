using JarmuKolcsonzo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JarmuKolcsonzo.Views
{
    /// <summary>
    /// Interaction logic for UgyfelView.xaml
    /// </summary>
    public partial class UgyfelView : UserControl
    {
        public UgyfelView()
        {
            InitializeComponent();
            ugyfelekDGV.Sorting += UgyfelekDGV_Sorting;
        }

        private void UgyfelekDGV_Sorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            Vm.SortBy = column.SortMemberPath;
            // prevent the built-in sort from sorting
            e.Handled = true;
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            //set the sort order on the column
            column.SortDirection = direction;
        }

        private UgyfelViewModel Vm => this.DataContext as UgyfelViewModel;
    }
}
