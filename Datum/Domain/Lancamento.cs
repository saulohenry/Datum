using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{   [Table("lancamento")]
    public class Lancamento
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public TipoLancamento Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public enum TipoLancamento
        {
            Debito = 1,
            Credito = 2
        }

        public void ConsolidadoDia(DateTime DataConsolidado)
        {
            return;
        }
    }
}

