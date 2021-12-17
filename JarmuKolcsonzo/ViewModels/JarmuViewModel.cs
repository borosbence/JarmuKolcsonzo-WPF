using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace JarmuKolcsonzo.ViewModels
{
    public class JarmuViewModel : ObservableObject
    {
        private GenericRepository<Jarmu, JKContext> jarmuRepo;
        public ObservableCollection<Jarmu> Jarmuvek { get; set; }

        public JarmuViewModel(JKContext context)
        {
            jarmuRepo = new JarmuRepository<JKContext>(context);
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuRepo.GetAll());
        }
    }
}