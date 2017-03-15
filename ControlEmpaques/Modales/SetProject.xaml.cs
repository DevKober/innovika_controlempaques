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
using System.Windows.Shapes;

namespace ControlEmpaques.Modales
{    
    /// <summary>
    /// Lógica de interacción para SetProject.xaml
    /// </summary>
    public partial class SetProject : Window
    {
        private string[] Datos { get; set; }
        public string GetProyecto => Datos[0];
        public string GetOrden => Datos[1];
        public string GetPieza => Datos[2];

        public SetProject()
        {
            InitializeComponent();
            TxtProyecto.Focus();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = TxtProyecto.Text.Length > 8;
        }

        private void TxtProyecto_KeyUp(object sender, KeyEventArgs e)
        {
            var codeOrigin = TxtProyecto.Text;
            Datos = codeOrigin.Split('.');

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                DialogResult = TxtProyecto.Text.Length > 8;
            }
        }

    }
}
