using System;

namespace OfxImport
{
    public class OfxDocument
    {
        public enum ECreditoDebito { Credito, Debito };
        public ECreditoDebito CreditoDebito { get; internal set; }

        public void SetCreditoDebito(string value)
        {
            switch (value)
            {
                case "CREDIT":
                    CreditoDebito = ECreditoDebito.Credito;
                    break;
                case "DEBIT":
                    CreditoDebito = ECreditoDebito.Debito;
                    break;
                default:
                    CreditoDebito = ECreditoDebito.Credito;
                    break;
            }
        }

        public DateTime DataOperacao { get; internal set; }
        public void SetDataOperacao(string value)
        {
            if (value is null)
                DataOperacao = DateTime.Today;

            DateTime dataOperacao;
            DateTime.TryParse($"{value.Substring(6, 2)}/{value.Substring(4, 2)}/{value.Substring(0, 4)}", out dataOperacao);
            DataOperacao = dataOperacao;
        }

        public double ValorOperacao { get; internal set; }
        public void SetValorOperacao(string value)
        {
            double valorOperacao;
            double.TryParse(value.Replace(",", "").Replace(".", ","), out valorOperacao);
            ValorOperacao = valorOperacao;
        }

        public string Documento { get; set; }
        public string Observacoes { get; set; }
    }
}
