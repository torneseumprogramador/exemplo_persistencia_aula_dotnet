using console_app.Models;
using System.Collections.Generic;
using System.IO;

namespace console_app.Servicos
{
    public class CsvPersistencia
    {
        public static void Salvar(string nomeArquivo, List<Cliente> clientes)
        {
            string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                sw.WriteLine("Id,Nome,Telefone");

                foreach (Cliente c in clientes)
                {
                    sw.WriteLine($"{c.Id},{c.Nome},{c.Telefone}");
                }
            }
        }

        public static List<Cliente> Lista(string nomeArquivo)
        {
            string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
            if (!File.Exists(caminhoArquivo)) return new List<Cliente>();

            List<Cliente> clientes = new List<Cliente>();
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                sr.ReadLine(); // Ignora a primeira linha (cabeçalho)

                while (!sr.EndOfStream)
                {
                    string[] campos = sr.ReadLine().Split(",");
                    Cliente c = new Cliente(campos[0])
                    {
                        Nome = campos[1],
                        Telefone = campos[2]
                    };
                    clientes.Add(c);
                }
            }
            return clientes;
        }
    }
}
