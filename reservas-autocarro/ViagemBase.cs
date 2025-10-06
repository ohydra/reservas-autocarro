using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public abstract class ViagemBase
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataHora { get; set; }

        public ViagemBase(string origem, string destino, DateTime dataHora)
        {
            Origem = origem;
            Destino = destino;
            DataHora = dataHora;
        }

        public abstract void MostrarResumo();
    }
}
