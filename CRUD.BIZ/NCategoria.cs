using CRUD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BIZ
{
    public class NCategoria
    {
        private DCategoria _categoriaDAL;

        public NCategoria(string databaseName)
        {
            _categoriaDAL = new DCategoria(databaseName);
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await _categoriaDAL.GetCategoriasAsync();
        }

        public async Task InsertCategoriaAsync(Categoria categoria)
        {
            await _categoriaDAL.InsertCategoriaAsync(categoria);
        }

        public async Task DeleteCategoriaAsync(Categoria categoria)
        {
            await _categoriaDAL.DeleteCategoriaAsync(categoria);
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            await _categoriaDAL.UpdateCategoriaAsync(categoria);
        }
    }
}
