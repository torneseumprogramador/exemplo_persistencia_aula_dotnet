using console_app.Models;
using System.Text.Json;

namespace console_app.Servicos;

public class JsonPersistencia
{
    public static void Salvar(string nomeArquivo, List<Cliente> clientes)
    {
        string stringJson = JsonSerializer.Serialize(clientes);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }

    public static List<Cliente> Lista(string nomeArquivo)
    {
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        if(!File.Exists(caminhoArquivo)) return new List<Cliente>();
        string stringJson = File.ReadAllText(caminhoArquivo);
        List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>(stringJson);
        return clientes;
    }
}