namespace ContratoPersistencia.Atributos;

public class IdentidadeAttribute : Attribute
{
    public string NomeNoBancoDeDados { get; set; } = default!;
}