using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class DCategoria
    {
        private IMongoCollection<Categoria> _categoriaCollection;
        private IMongoDatabase _database;

        public DCategoria(string databaseName)
        {
            var database = ConexionMongoDB.ObtenerBaseDatos(databaseName);
            _categoriaCollection = database.GetCollection<Categoria>("categorias");

            // Crear un índice único para asegurar que IdCategoria sea autoincrementable
            var indexKeysDefinition = Builders<Categoria>.IndexKeys.Ascending(c => c.IdCategoria);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Categoria>(indexKeysDefinition, indexOptions);
            _categoriaCollection.Indexes.CreateOne(indexModel);
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await _categoriaCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task InsertCategoriaAsync(Categoria categoria)
        {
            // Obtener el valor máximo actual de IdCategoria y generar el siguiente
            var maxIdCategoria = await _categoriaCollection.Find(new BsonDocument())
                .SortByDescending(c => c.IdCategoria).Limit(1)
                .Project(c => c.IdCategoria).FirstOrDefaultAsync();

            categoria.IdCategoria = (maxIdCategoria == null) ? 1 : maxIdCategoria + 1;

            await _categoriaCollection.InsertOneAsync(categoria);
        }

        public async Task DeleteCategoriaAsync(Categoria categoria)
        {
            var result = await _categoriaCollection.DeleteOneAsync(c => c.Id == categoria.Id);
            if (result.DeletedCount == 0)
                throw new Exception("No se encontró la categoría para eliminar.");
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            var result = await _categoriaCollection.ReplaceOneAsync(c => c.Id == categoria.Id, categoria);
            if (result.MatchedCount == 0)
                throw new Exception("No se encontró la categoría para actualizar.");
        }
    }
}
