using CRUD.BIZ;
using CRUD.DAL;
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

namespace EjemploCRUD.UI
{
    /// <summary>
    /// Lógica de interacción para Articulo.xaml
    /// </summary>
    public partial class Articulo : Window
    {
        private NCategoria _categoriaBiz;
        private NArticulo _articuloBiz;
        private string nombreCategoriaSeleccionada;
        public Articulo()
        {
            InitializeComponent();
            _articuloBiz = new NArticulo("mongodb+srv://Eric:emqjjyjmg250898@crud.rqdugs5.mongodb.net/?retryWrites=true&w=majority&appName=CRUD", "CRUD");
            _categoriaBiz = new NCategoria("CRUD");
            LoadCategorias();
            LoadArticulos();
        }

        private async void LoadCategorias()
        {
            try
            {
                var categorias = await _categoriaBiz.GetCategoriasAsync();

                // Asignar la lista de categorías al ComboBox
                categoriaComboBox.ItemsSource = categorias;

                // Establecer la propiedad para mostrar el nombre de la categoría
                categoriaComboBox.DisplayMemberPath = "Nombre";

                // Establecer la propiedad para obtener el Id de la categoría seleccionada
                categoriaComboBox.SelectedValuePath = "IdCategoria";

                // Manejar el evento de selección del ComboBox
                categoriaComboBox.SelectionChanged += categoriaComboBox_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las categorías: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadArticulos()
        {
            var articulos = await _articuloBiz.GetArticulosAsync();
            ArticuloDataGrid.ItemsSource = articulos;
        }

        private void ArticuloDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private async void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener los valores de los campos de texto y el ComboBox
                string nombre = nombreTextBox.Text;
                int precio = int.Parse(precioTextBox.Text);
                int idCategoria = (int)categoriaComboBox.SelectedValue; // Suponiendo que el ValueMember es el IdCategoria
                // Crear una instancia de Articulo con los datos ingresados
                CRUD.DAL.Articulo nuevoArticulo = new CRUD.DAL.Articulo()
                {
                    Nombre = nombre,
                    Precio = precio,
                    IdCategoria = idCategoria,
                    Categoria = nombreCategoriaSeleccionada,
                };

                // Llamar al método de la capa de negocio para insertar el artículo
                await _articuloBiz.InsertArticuloAsync(nuevoArticulo);

                // Recargar los artículos en el DataGrid después de la inserción
                LoadArticulos();

                MessageBox.Show("Artículo insertado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar el artículo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (ArticuloDataGrid.SelectedItem != null && ArticuloDataGrid.SelectedItem is CRUD.DAL.Articulo selectedArticulo)
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar esta categoría?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _articuloBiz.DeleteArticuloAsync(selectedArticulo);
                        MessageBox.Show("Categoría eliminada correctamente.");
                        LoadArticulos(); // Actualizar la lista de categorías después de la eliminación
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar categoría: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una categoría para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void categoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el objeto de la categoría seleccionada
            Categoria categoriaSeleccionada = categoriaComboBox.SelectedItem as Categoria;

            // Guardar el nombre de la categoría seleccionada en la variable nombreCategoriaSeleccionada
            if (categoriaSeleccionada != null)
            {
                nombreCategoriaSeleccionada = categoriaSeleccionada.Nombre;
            }
        }
    }
}
