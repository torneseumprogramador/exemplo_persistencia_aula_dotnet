using ContratoPersistencia;
using Npgsql;

namespace PostgresPersistencia;

public class PersistenciaPostgres : APersistencia
{
    public PersistenciaPostgres(string _cnn)
    {
        connString = _cnn;
    }

    private string connString;

    public override void Incluir(IEntity entidade)
    {
        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        var tipoEntidade = entidade.GetType();
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

    public override void Atualizar(IEntity entidade)
    {
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
        cmd.Parameters.AddWithValue("id", entidade.Id);

        cmd.ExecuteNonQuery();
    }


    public override List<IEntity> Buscar(Type tipoEntidade)
    {
        List<IEntity> entidades = new List<IEntity>();
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand($"SELECT * FROM {tipoEntidade.Name.ToLower()}s;", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var entidade = Activator.CreateInstance(tipoEntidade);
            if(entidade == null) return new List<IEntity>();

            foreach(var pi in entidade.GetType().GetProperties())
            {
                var piSet = entidade.GetType().GetProperty(pi.Name);
                if(piSet == null) continue;
                piSet.SetValue(entidade, reader[pi.Name]);
            }

            entidades.Add((IEntity)entidade);
        }
        return entidades;
    }

    public override void Apagar(IEntity entidade)
    {
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM clientes WHERE id = @id;", conn);
        cmd.Parameters.AddWithValue("id", entidade.Id);
        cmd.ExecuteNonQuery();
    }
}
