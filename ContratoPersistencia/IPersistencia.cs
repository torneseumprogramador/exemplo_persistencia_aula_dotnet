namespace ContratoPersistencia;
public interface IPersistencia<T>
{
    void Incluir(T entidade);
    void Atualizar(T entidade);
    List<T> Buscar();
    void Apagar(T entidade);
}
