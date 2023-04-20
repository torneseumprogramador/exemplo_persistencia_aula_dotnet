using System;
using System.IO;
using System.Text.Json;

namespace console_app.Models
{
    public partial class Carro
    {
        public void Salvar()
        {
            string stringJson = JsonSerializer.Serialize(this);
            string caminhoArquivo = Directory.GetCurrentDirectory() + "/clientes.json";
            File.WriteAllText(caminhoArquivo, stringJson);
        }
    }
}