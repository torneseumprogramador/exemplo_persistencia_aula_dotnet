using console_app.Models;
using ContratoPersistencia;
using System.Collections.Generic;
using System.IO;

namespace console_app.Servicos
{
    public class Persistencia
    {
        public Persistencia(APersistencia _persistencia)
        {
            this.persistencia = _persistencia;
        }

        private APersistencia persistencia;

        public void Salvar(Cliente cliente)
        {
            persistencia.Incluir(cliente);
        }

        public List<IEntity> Lista()
        {
            return persistencia.Buscar(typeof(Cliente));
        }
    }
}
