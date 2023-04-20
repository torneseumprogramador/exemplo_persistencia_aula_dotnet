using System;

namespace console_app.Models
{
    public class Cliente : Pessoa
    {
        public Cliente()
        {
            this.id = Guid.NewGuid().ToString();
        }

        public Cliente(string _id)
        {
            this.id = _id;
        }

        public Cliente(int _id)
        {
            this.id = _id.ToString();
        }

        private string codigo;
        private string elemento;


        private string id;
        public string Id
        {
            get
            {
                return this.id;
            }
        }

        private string nome;

        public string Nome
        {
            get
            {
                return this.nome.ToUpper();
            }
            set
            {
                this.nome = value.ToLower();
            }
        }


        private string cnpj;
        public string CNPJ
        {
            get { return this.cnpj; }
        }

        private void validarCNPJ(string value)
        {
            this.enderecoCompletoProtegido = "aaa";
            this.enderecoCompletoInternoParaOCsProj = "sss";
            if (this.cnpj == "invalido")
                throw new Exception("CNPJ Inválido");
        }

        public string Telefone { get; set; }

        public string Telefone2 { get => "teste"; }
        public int Codigo { get; set; } = 0;
        public double ValorPorMinuto { get => 0; }

        public void Salvar()
        {
            // salvar a informação em disco
            Console.WriteLine($"Estou salvando o objeto {this.nome}");
        }

        public static void Salvar(Cliente obj)
        {
            Console.WriteLine($"Estou salvando o objeto {obj.nome}");
        }

        public new string NomeComFrase(string frasePassada)
        {
            return $"chamado da classe interna - {base.NomeComFrase(frasePassada)}";
        }

        public override string NomeComFraseVirtual(string frasePassada)
        {
            return $"chamado da classe interna - {base.NomeComFrase(frasePassada)}";
        }
    }
}