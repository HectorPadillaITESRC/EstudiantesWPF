using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EstudiantesWPF.ViewModels
{
    public class Navigation : INotifyPropertyChanged
    {
        public UserControl? View { get; set; }

        public Navigation()
        {
            
        }

        public void Navigate(string name)
        {
            var view = (UserControl)Application.Current.MainWindow.FindResource(name);
            View = view;
            Actualizar(nameof(View)); 
        }

        private void Actualizar(string? nombre = null)
        {
            PropertyChanged?.Invoke(null, new(nombre));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
