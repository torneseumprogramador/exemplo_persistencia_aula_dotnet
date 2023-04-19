using System;
using System.Collections.Generic;
using console_app.Models;
using Npgsql;

namespace console_app.Servicos
{
    public class PostgresPersistencia
    {
        private static readonly string connString = "Server=localhost;User Id=postgres;Password=senha;Database=nome_do_banco;";

        public static void Incluir(Cliente cliente)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO clientes (nome, telefone) VALUES (@nome, @telefone);", conn);
            cmd.Parameters.AddWithValue("nome", cliente.Nome);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            cmd.ExecuteNonQuery();
        }

        public static void Atualizar(Cliente cliente)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using var cmd = new NpgsqlCommand("UPDATE clientes SET nome = @nome, telefone = @telefone WHERE id = @id;", conn);
            cmd.Parameters.AddWithValue("nome", cliente.Nome);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("id", cliente.Id);
            cmd.ExecuteNonQuery();
        }

        public static List<Cliente> Buscar()
        {
            List<Cliente> clientes = new List<Cliente>();
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM clientes;", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new Cliente(reader.GetInt32(0))
                {
                    Nome = reader.GetString(1),
                    Telefone = reader.GetString(2)
                });
            }
            return clientes;
        }

        public static void Apagar(int id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM clientes WHERE id = @id;", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
