using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class DArticulo
    {
        private IMongoCollection<Articulo> _articuloCollection;

        public DArticulo(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _articuloCollection = database.GetCollection<Articulo>("articulos");

            //Crear un índice único para asegurar que IdCategoria sea autoincrementable
            var indexKeysDefinition = Builders<Articulo>.IndexKeys.Ascending(c => c.IdArticulo);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Articulo>(indexKeysDefinition, indexOptions);
            _articuloCollection.Indexes.CreateOne(indexModel);
        }

        public async Task<List<Articulo>> GetArticulosAsync()
        {
            return await _articuloCollection.Find(_ => true).ToListAsync();
        }

        public async Task InsertArticuloAsync(Articulo articulo)
        {
            //Obtener el valor máximo actual de IdCategoria y generar el siguiente
            var maxIdArticulo = await _articuloCollection.Find(new BsonDocument())
                .SortByDescending(c => c.IdArticulo).Limit(1)
                .Project(c => c.IdArticulo).FirstOrDefaultAsync();

            articulo.IdArticulo = (maxIdArticulo == null) ? 1 : maxIdArticulo + 1;
            await _articuloCollection.InsertOneAsync(articulo);
        }

        public async Task DeleteArticuloAsync(Articulo articulo)
        {
            var result = await _articuloCollection.DeleteOneAsync(c => c.Id == articulo.Id);
            if (result.DeletedCount == 0)
                throw new Exception("No se encontró el articulo para eliminar.");
        }
    }
}
