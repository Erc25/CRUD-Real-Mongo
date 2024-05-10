using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRUD.BIZ;
using CRUD.DAL;

namespace EjemploCRUD.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NCategoria _categoriaBiz;
        private Categoria _categoriaSeleccionada;
        public MainWindow()
        {
            InitializeComponent();
            _categoriaBiz = new NCategoria("CRUD");
            LoadCategorias();
        }

        private async void LoadCategorias()
        {
            var categorias = await _categoriaBiz.GetCategoriasAsync();
            categoriasDataGrid.ItemsSource = categorias;
        }

        private async void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Categoria nuevaCategoria = new Categoria
                {
                    Nombre = nombreTextBox.Text,
                    Descripcion = descripcionTextBox.Text,
                    Clave = Convert.ToInt32(claveTextBox.Text)
                };

                await _categoriaBiz.InsertCategoriaAsync(nuevaCategoria);
                MessageBox.Show("Categoría insertada correctamente.");
                LoadCategorias(); // Actualizar la lista de categorías después de la inserción
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar categoría: {ex.Message}");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (categoriasDataGrid.SelectedItem != null && categoriasDataGrid.SelectedItem is Categoria selectedCategoria)
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar esta categoría?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _categoriaBiz.DeleteCategoriaAsync(selectedCategoria);
                        MessageBox.Show("Categoría eliminada correctamente.");
                        LoadCategorias(); // Actualizar la lista de categorías después de la eliminación
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

        private void categoriasDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (categoriasDataGrid.SelectedItem != null && categoriasDataGrid.SelectedItem is Categoria selectedCategoria)
            {
                _categoriaSeleccionada = selectedCategoria;
                // Cargar datos de la categoría seleccionada en los TextBox
                nombreTextBox.Text = selectedCategoria.Nombre;
                descripcionTextBox.Text = selectedCategoria.Descripcion;
                claveTextBox.Text = selectedCategoria.Clave.ToString();
            }
        }

        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (_categoriaSeleccionada != null)
            {
                try
                {
                    // Actualizar los datos de la categoría seleccionada
                    _categoriaSeleccionada.Nombre = nombreTextBox.Text;
                    _categoriaSeleccionada.Descripcion = descripcionTextBox.Text;
                    _categoriaSeleccionada.Clave = Convert.ToInt32(claveTextBox.Text);

                    await _categoriaBiz.UpdateCategoriaAsync(_categoriaSeleccionada);
                    MessageBox.Show("Categoría actualizada correctamente.");
                    LoadCategorias(); // Actualizar la lista de categorías después de la actualización
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar categoría: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una categoría para editar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnArticulo_Click(object sender, RoutedEventArgs e)
        {
            Articulo frm = new Articulo();
            frm.Show();
            this.Hide();
        }
    }
}