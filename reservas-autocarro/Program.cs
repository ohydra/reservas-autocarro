using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    class Program
    {
        static void Main()
        {
            SistemaReservas sistema = new SistemaReservas();

            // Criar autocarros (agora com herança de Veiculo)
            Autocarro bus1 = new Autocarro(101, "AA-12-BB", 40, "João Mendes");
            Autocarro bus2 = new Autocarro(202, "CC-34-DD", 40, "Carla Silva");

            sistema.AdicionarAutocarro(bus1);
            sistema.AdicionarAutocarro(bus2);

            // Mostrar detalhes do autocarro (demonstra polimorfismo)
            Veiculo v = bus1;
            v.MostrarInfo(); // chama o método do Autocarro (override)

            // Criar e listar viagens
            sistema.CriarViagem("Lisboa", "Porto", new DateTime(2025, 10, 7, 9, 0, 0), bus1);
            sistema.CriarViagem("Porto", "Braga", new DateTime(2025, 10, 7, 14, 30, 0), bus2);

            sistema.ListarViagens();

            // Fazer reserva
            Viagem viagem = sistema.Viagens[0];
            Passageiro p1 = new Passageiro("Ana Silva", "123456789");
            Reserva r1 = new Reserva(p1, 12, viagem);
            viagem.AdicionarReserva(r1);

            viagem.ListarReservas();
        }
    }
}
