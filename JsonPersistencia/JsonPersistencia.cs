using System.Reflection;
using System.Text.Json;
using ContratoPersistencia;
using ContratoPersistencia.Atributos;

namespace JsonPersistencia;

public class PersistenciaJson<T> : IPersistencia<T>
{
    public PersistenciaJson()
    {
        this.nomeArquivo = typeof(T).Name.ToLower() + "s.json";
    }

    private string nomeArquivo;

    public void Incluir(T entidade)
    {
        var entidades = this.Buscar();
        entidades.Add(entidade);

        string stringJson = JsonSerializer.Serialize(entidades);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }

    public void Atualizar(T entidade)
    {
        if(entidade == null) return;
        
        var entidades = this.Buscar();

        var objLocalizado = descobrirObjeto(entidade, entidades);

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

    private T? descobrirObjeto(T entidade, List<T> entidades)
    {
        if(entidade == null) return default(T);
        
        var propriedadeId = entidade.GetType().GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(IdentidadeAttribute)));
        if(propriedadeId == null) return default(T);
        var valorPropriedadeId = propriedadeId.GetValue(entidade);
        if(valorPropriedadeId == null) return default(T);

        var objLocalizado = entidades.FirstOrDefault(e =>
        {
            if(e != null)
            {
                var propriedadeIdLocalizado = e.GetType().GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(IdentidadeAttribute)));
                if(propriedadeIdLocalizado != null)
                {
                    var valorPropriedadeIdLocalizado = propriedadeIdLocalizado.GetValue(e);
                    if(valorPropriedadeIdLocalizado != null)
                        return valorPropriedadeIdLocalizado.Equals(valorPropriedadeId);
                }
            }

            return false;
        });

        return objLocalizado;
    }

    public List<T> Buscar()
    {
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        if (!File.Exists(caminhoArquivo)) return new List<T>();
        string stringJson = File.ReadAllText(caminhoArquivo);

        Type tipoLista = typeof(List<>).MakeGenericType(typeof(T));
        object? lista = JsonSerializer.Deserialize(stringJson, tipoLista);

        if(lista == null) return new List<T>();

        List<T> entidades = (List<T>)lista;
        return entidades;
    }

    public void Apagar(T entidade)
    {
        var entidades = this.Buscar();
        var objLocalizado = descobrirObjeto(entidade, entidades);
        if(objLocalizado == null) return;
        
        entidades.Remove(objLocalizado);

        string stringJson = JsonSerializer.Serialize(entidades);
        string caminhoArquivo = Directory.GetCurrentDirectory() + "/" + nomeArquivo;
        File.WriteAllText(caminhoArquivo, stringJson);
    }
}