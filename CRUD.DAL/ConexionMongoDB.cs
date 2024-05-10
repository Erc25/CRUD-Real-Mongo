using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public static class ConexionMongoDB
    {
        public static IMongoDatabase ObtenerBaseDatos(string nombreBaseDatos)
        {
            // Define tu cadena de conexión a MongoDB
            string connectionString = "mongodb+srv://Eric:emqjjyjmg250898@crud.rqdugs5.mongodb.net/?retryWrites=true&w=majority&appName=CRUD"; // Ejemplo de cadena de conexión, ajusta según tus necesidades

            // Crea el cliente de MongoDB
            var client = new MongoClient(connectionString);

            // Obtiene la base de datos
            var database = client.GetDatabase(nombreBaseDatos);

            return database;
        }
    }
}
