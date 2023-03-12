using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Entities.Entities.Validations
{
    public class Notifiers
    {
        public Notifiers()
        {
            Notificacoes = new List<Notifiers>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifiers> Notificacoes { get; set; }

        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifiers 
                { 
                    NomePropriedade = nomePropriedade,
                    Mensagem = "Campo obrigatório"
                });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor > 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifiers
                {
                    NomePropriedade = nomePropriedade,
                    Mensagem = "Campo obrigatório"
                });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor > 0.1M || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifiers
                {
                    NomePropriedade = nomePropriedade,
                    Mensagem = "Campo obrigatório"
                });

                return false;
            }

            return true;
        }
    }
}
