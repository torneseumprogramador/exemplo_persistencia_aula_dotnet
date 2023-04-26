using ContratoPersistencia;
using ContratoPersistencia.Atributos;
using Npgsql;

namespace PostgresPersistencia;

public class PersistenciaPostgres<T> : IPersistencia<T>
{
    public PersistenciaPostgres(string _cnn)
    {
        connString = _cnn;
    }

    private string connString;

    public void Incluir(T entidade)
    {
        if(entidade == null) return;

        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        var tipoEntidade = typeof(T).GetType();
        var nomeTabela = tipoEntidade.Name.ToLower() + "s";
        var campos = string.Join(",", tipoEntidade.GetProperties().Select(pi => pi.Name.ToLower()));
        var valores = string.Join(",", tipoEntidade.GetProperties().Select(pi => "@" + pi.Name));

        var sql = $"INSERT INTO {nomeTabela} ({campos}) VALUES ({valores})";

        using var cmd = new NpgsqlCommand(sql, conn);
        foreach (var pi in tipoEntidade.GetProperties())
        {
            var valor = pi.GetValue(entidade);
            if(valor == null) continue;
            cmd.Parameters.AddWithValue(pi.Name.ToLower(), valor);
        }

        cmd.ExecuteNonQuery();
    }

    public void Atualizar(T entidade)
    {
        if(entidade == null) return;

        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        var tipoEntidade = entidade.GetType();
        var nomeTabela = tipoEntidade.Name.ToLower() + "s";
        var valores = string.Join(",", tipoEntidade.GetProperties().Select(pi => $"{pi.Name.ToLower()}=@{pi.Name}"));
        var sql = $"UPDATE {nomeTabela} SET {valores} WHERE id = @id";

        using var cmd = new NpgsqlCommand(sql, conn);
        foreach (var pi in tipoEntidade.GetProperties())
        {
            var valor = pi.GetValue(entidade);
            if(valor == null) continue;
            cmd.Parameters.AddWithValue(pi.Name.ToLower(), valor);
        }

        var valorId = buscaValorIdObj(entidade);

        if(valorId == null) 
            throw new Exception("O valor da identidade não pode ser null");

        cmd.Parameters.AddWithValue("id", valorId);

        cmd.ExecuteNonQuery();
    }


    public List<T> Buscar()
    {
        List<T> entidades = new List<T>();
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand($"SELECT * FROM {typeof(T).Name.ToLower()}s;", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var entidade = Activator.CreateInstance(typeof(T));
            if(entidade == null) return new List<T>();

            foreach(var pi in entidade.GetType().GetProperties())
            {
                var piSet = entidade.GetType().GetProperty(pi.Name);
                if(piSet == null) continue;
                piSet.SetValue(entidade, reader[pi.Name]);
            }

            entidades.Add((T)entidade);
        }
        return entidades;
    }

    public void Apagar(T entidade)
    {
        if(entidade == null) return;

        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM clientes WHERE id = @id;", conn);

        var valorId = buscaValorIdObj(entidade);

        if(valorId == null) 
            throw new Exception("O valor da identidade não pode ser null");
            
        cmd.Parameters.AddWithValue("id", valorId);
        cmd.ExecuteNonQuery();
    }

    private object? buscaValorIdObj(T entidade)
    {
        if(entidade == null) return null;
        
        var propriedadeId = entidade.GetType().GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(IdentidadeAttribute)));
        if(propriedadeId == null) 
            throw new Exception("Tipo passado não tem atributo identidade");

        var valorId = propriedadeId.GetValue(entidade);

        if(valorId == null) 
            throw new Exception("O valor da identidade não pode ser null");

        return valorId;
    }
}
