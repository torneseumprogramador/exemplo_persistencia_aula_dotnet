using ContratoPersistencia.Atributos;
namespace console_app.Models
{
    public class Pessoa
    {
        [Identidade(NomeNoBancoDeDados = "xxx")]
        public string Documento { get; set; }
        public string Endereco { get; set; }

        private string enderecoCompleto;
        protected string enderecoCompletoProtegido;
        internal string enderecoCompletoInternoParaOCsProj;

        public string NomeComFrase(string frasePassada)
        {
            return $"{frasePassada} - {this.Documento}";
        }

        public virtual string NomeComFraseVirtual(string frasePassada)
        {
            return $"{frasePassada} - {this.Documento}";
        }
    }
}