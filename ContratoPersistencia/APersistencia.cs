namespace ContratoPersistencia;
public abstract class APersistencia
{
    public abstract void Incluir(IEntity entidade);
    public abstract void Atualizar(IEntity entidade);
    public abstract List<IEntity> Buscar(Type tipoEntidade);
    public abstract void Apagar(IEntity entidade);

    public string QuemEVoce()
    {
        return "Eu sou uma abistração e tenho mais poderes do que a interface";
    }
}
