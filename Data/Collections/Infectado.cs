using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Infectado
    {
        public Infectado(int idInfectado, DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.IdInfectado = idInfectado;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public int IdInfectado { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
        

    }
}