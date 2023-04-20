using System;
using System.IO;
using System.Text.Json;

namespace console_app.Models
{
    public partial class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }
}