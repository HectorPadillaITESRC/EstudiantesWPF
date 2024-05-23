using EstudiantesWPF.ViewModels;
using System;
using System.Collections.Generic;
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

namespace EstudiantesWPF.Views
{
    /// <summary>
    /// Lógica de interacción para MostrarAlumnosView.xaml
    /// </summary>
    public partial class MostrarAlumnosView : UserControl
    {
        public MostrarAlumnosView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((EstudiantesViewModel)this.DataContext)
                .FiltrarCommand
                .Execute(((TextBox)sender).Text);
        }
    }
}
