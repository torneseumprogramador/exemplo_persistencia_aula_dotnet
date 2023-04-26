using console_app.Models;
using ContratoPersistencia;
using System.Collections.Generic;
using System.IO;

namespace console_app.Servicos
{
    public class Persistencia<T>
    {
        public Persistencia(IPersistencia<T> _persistencia)
        {
            this.persistencia = _persistencia;
        }

        private IPersistencia<T> persistencia;

        public void Salvar(T entidade)
        {
            persistencia.Incluir(entidade);
        }

        public List<T> Lista()
        {
            return persistencia.Buscar();
        }
    }
}
