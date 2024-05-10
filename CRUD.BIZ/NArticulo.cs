using CRUD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BIZ
{
    public  class NArticulo
    {
        private DArticulo _articuloDAL;

        public NArticulo(string connectionString, string databaseName)
        {
            _articuloDAL = new DArticulo(connectionString, databaseName);
        }

        public async Task<List<Articulo>> GetArticulosAsync()
        {
            return await _articuloDAL.GetArticulosAsync();
        }

        public async Task InsertArticuloAsync(Articulo articulo)
        {
            await _articuloDAL.InsertArticuloAsync(articulo);
        }

        public async Task DeleteArticuloAsync(Articulo articulo)
        {
            await _articuloDAL.DeleteArticuloAsync(articulo);
        }
    }
}
