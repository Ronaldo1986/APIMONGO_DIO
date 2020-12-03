using System.Collections.Immutable;
using System;
using System.Reflection.Metadata.Ecma335;
using Api.Data.Collections;
using Api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.IdInfectado, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto)
        {
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.IdInfectado == dto.IdInfectado), Builders<Infectado>.Update.Set("sexo", dto.Sexo));
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.IdInfectado == dto.IdInfectado), Builders<Infectado>.Update.Set("dataNascimento", dto.DataNascimento));

            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{idInfectado}")]
        public ActionResult Delete(int idInfectado)
        {
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.IdInfectado == idInfectado));

            return Ok("Excluido com sucesso");
        }
    }
}