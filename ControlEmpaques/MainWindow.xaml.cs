using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ControlEmpaques.Modales;
using ControlEmpaques.Modelos;

namespace ControlEmpaques
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ProyectoAc { get; set; }
        private string OrdenAc { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DatosPresupuesto.RowHeight = 22;
        }

        private void BtnProyecto_Click(object sender, RoutedEventArgs e)
        {
            var sProject = new SetProject {Owner = this};
            if (sProject.ShowDialog() == true)
            {
                LoadInventarioPresupuesto(sProject.GetProyecto);
            }
        }

        private void LoadInventarioPresupuesto(string sProjectGetProyecto)
        {

            var innoWin = new InnoWinEntities();
            var checkPresupuesto = (innoWin.PedidoInventario.Where(pre => pre.Presupuesto == sProjectGetProyecto)).Count();

            if (checkPresupuesto > 0)
            {
                ProyectoAc = sProjectGetProyecto;
                var inventario = (from ped in innoWin.PedidoInventario
                    join box in innoWin.Boxlist on ped.id equals box.idInventario into bx
                    from boxEnd in bx.DefaultIfEmpty()
                    join emp in innoWin.Empaque on boxEnd.idEmpaque equals emp.id into ep
                    from empEnd in ep.DefaultIfEmpty()
                    where ped.Presupuesto == sProjectGetProyecto
                    orderby ped.Empacado descending
                    orderby boxEnd.idEmpaque ascending
                    select new
                    {
                        Id = ped.id,
                        Presupuesto = ped.Presupuesto,
                        Pedido = ped.Pedido,
                        Descripcion = ped.Descripcion,
                        Dimensiones = ped.Dimensiones,
                        OrdenFabricacion = ped.OrdenFabricacion,
                        Autonumerico = ped.Autonumerico,
                        Empacado = ped.Empacado,
                        IdEmpaque = (boxEnd == null ? 0 : boxEnd.idEmpaque),
                        Consecutivo = (empEnd == null ? 0 : empEnd.Consecutivo)
                    }).ToList();
                DatosPresupuesto.ItemsSource = inventario.OrderBy(obj => !obj.Empacado).ToList();
                TxtPresupuesto.Content = sProjectGetProyecto;

                BtnNuevoEmp.IsEnabled = true;
                BtnNuevoEmp.Background = Brushes.LightSkyBlue;
                BtnNuevoEmp.FontWeight = FontWeights.Bold;
            }
            else
            {
                MessageBox.Show("No fue posible encontrar el presupuesto:   " + sProjectGetProyecto, "Innovoka",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnNuevoEmp_Click(object sender, RoutedEventArgs e)
        {
            var innowin = new InnoWinEntities();

        }
    }
}
