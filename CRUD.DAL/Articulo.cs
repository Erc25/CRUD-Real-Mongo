using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Articulo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("idArticulo")]
        public int IdArticulo { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("precio")]
        public int Precio { get; set; }

        [BsonElement("idCategoria")]
        public int IdCategoria { get; set; }

        public string Categoria { get; set; }
    }
}
