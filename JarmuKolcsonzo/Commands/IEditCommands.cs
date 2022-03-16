using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Commands
{
    public interface IEditCommands<T>
    {
        RelayCommand<T> NewCmd { get; }
        RelayCommand<T> SaveCmd { get; }
        RelayCommand<T> DeleteCmd { get; }

        void New() { }
        void Save(T entity) { }
        bool CanDelete() { return true; }
        void Delete(T entity) { }
    }
}
