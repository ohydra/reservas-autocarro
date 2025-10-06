using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public class SistemaReservas
    {
        public List<Autocarro> Autocarros { get; set; } = new List<Autocarro>();
        public List<Viagem> Viagens { get; set; } = new List<Viagem>();

        public void AdicionarAutocarro(Autocarro autocarro)
        {
            Autocarros.Add(autocarro);
        }

        public void CriarViagem(string origem, string destino, DateTime dataHora, Autocarro autocarro)
        {
            Viagens.Add(new Viagem(origem, destino, dataHora, autocarro));
        }

        public void ListarViagens()
        {
            Console.WriteLine("=== Lista de Viagens ===");
            foreach (var v in Viagens)
                v.MostrarResumo();
        }

    }
}
