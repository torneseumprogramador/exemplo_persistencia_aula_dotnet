using System.Text.Json;
using ContratoPersistencia;

namespace JsonPersistencia;

public class PersistenciaJson : IPersistencia
{
    public PersistenciaJson(string _nomeArquivo)
    {
        this.nomeArquivo = _nomeArquivo;
    }

    private string nomeArquivo;

    public void Incluir(IEntity entidade)
    {
        var entidades = this.Buscar(entidade.GetType());
        entidades.Add(entidade);

        string stringJson = JsonSerializer.Serialize(entidades);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }

    public void Atualizar(IEntity entidade)
    {
        var entidades = this.Buscar(entidade.GetType());

        var objLocalizado = entidades.Find(e => e.Id == entidade.Id);
        if(objLocalizado == null)
        {
            new Exception($"Entidade({entidade.GetType().Name}) não localizada");
            return;
        }

        foreach(var piLocalizado in objLocalizado.GetType().GetProperties())
        {
            var piEntidadePassada = entidade.GetType().GetProperty(piLocalizado.Name);
            if(piEntidadePassada != null)
            {
                piLocalizado.SetValue(objLocalizado, piEntidadePassada.GetValue(entidade));
            }
        }

        string stringJson = JsonSerializer.Serialize(entidades);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }

    public List<IEntity> Buscar(Type tipoEntidade)
    {
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        if (!File.Exists(caminhoArquivo)) return new List<IEntity>();
        string stringJson = File.ReadAllText(caminhoArquivo);

        Type tipoLista = typeof(List<>).MakeGenericType(tipoEntidade);
        object? lista = JsonSerializer.Deserialize(stringJson, tipoLista);

        if(lista == null) return new List<IEntity>();

        List<IEntity> entidades = (List<IEntity>)lista;
        return entidades;
    }

    public void Apagar(IEntity entidade)
    {
       var entidades = this.Buscar(entidade.GetType());
        entidades.RemoveAll(e => e.Id == entidade.Id);

        string stringJson = JsonSerializer.Serialize(entidades);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }
}