using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public class Reserva
    {
        public Passageiro Passageiro { get; set; }
        public int NumeroAssento { get; set; }
        public Viagem Viagem { get; set; }

        public Reserva(Passageiro passageiro, int numeroAssento, Viagem viagem)
        {
            Passageiro = passageiro;
            NumeroAssento = numeroAssento;
            Viagem = viagem;
        }

        public override string ToString()
        {
            return $"Assento {NumeroAssento}: {Passageiro.Nome} ({Passageiro.NIF})";
        }
    }
}
