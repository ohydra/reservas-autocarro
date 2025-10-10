using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    using System;
    using System.Collections.Generic;

    public class Viagem : ViagemBase
    {
        public Autocarro Autocarro { get; set; }
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Viagem(string origem, string destino, DateTime dataHora, Autocarro autocarro)
            : base(origem, destino, dataHora)
        {
            Autocarro = autocarro;
        }

        public bool AssentoDisponivel(int numeroAssento)
        {
            return !Reservas.Exists(r => r.NumeroAssento == numeroAssento);
        }

        public void AdicionarReserva(Reserva reserva)
        {
            if (AssentoDisponivel(reserva.NumeroAssento))
            {
                Reservas.Add(reserva);
                Console.WriteLine($"Reserva confirmada: assento {reserva.NumeroAssento}.");
            }
            else
            {
                Console.WriteLine($"O assento {reserva.NumeroAssento} já está reservado!");
            }
        }

        public void CancelarReserva(int numeroAssento)
        {
            Reserva reserva = Reservas.Find(r => r.NumeroAssento == numeroAssento);
            if (reserva != null)
            {
                Reservas.Remove(reserva);
                Console.WriteLine($"Reserva do assento {numeroAssento} cancelada.");
            }
            else
            {
                Console.WriteLine($"Nenhuma reserva encontrada para o assento {numeroAssento}.");
            }
        }

        public override void MostrarResumo()
        {
            Console.WriteLine($"Viagem: {Origem} >>> {Destino} em {DataHora} ({Autocarro})");
        }

        public void ListarReservas()
        {
            Console.WriteLine($"Reservas da viagem {Origem} → {Destino} ({DataHora})");
            foreach (var r in Reservas)
                Console.WriteLine(r);
        }

        public override string ToString()
        {
            return $"{Origem} >>> {Destino} em {DataHora} | {Autocarro}";
        }
    }
}
