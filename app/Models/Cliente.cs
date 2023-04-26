using System;
using ContratoPersistencia.Atributos;

namespace console_app.Models
{
    public class Cliente
    {
        public Cliente()
        {
            int timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            this.Id = timestamp;
        }

        [Identidade(NomeNoBancoDeDados = "Id")]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }
    }
}