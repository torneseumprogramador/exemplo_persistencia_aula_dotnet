namespace ContratoPersistencia;
public interface IPersistencia
{
    void Incluir(IEntity entidade);
    void Atualizar(IEntity entidade);
    List<IEntity> Buscar(Type tipoEntidade);
    void Apagar(IEntity entidade);
}
