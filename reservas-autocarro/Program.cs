using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/////COMENTARIO

namespace reservas_autocarro
{
    class Program
    {
        static void Main()
        {
            SistemaReservas sistema = new SistemaReservas();

            // Criar autocarros e viagens de exemplo
            Autocarro bus1 = new Autocarro(101, "AA-12-BB", 40, "João Mendes");
            Autocarro bus2 = new Autocarro(202, "CC-34-DD", 40, "Carla Silva");
            Autocarro bus3 = new Autocarro(303, "DD-30-EE", 40, "Gonçalo Rocha");
            sistema.AdicionarAutocarro(bus1);
            sistema.AdicionarAutocarro(bus2);
            sistema.AdicionarAutocarro(bus3);

            sistema.CriarViagem("Lisboa", "Porto", new DateTime(2025, 10, 7, 9, 0, 0), bus1);
            sistema.CriarViagem("Porto", "Braga", new DateTime(2025, 10, 7, 14, 30, 0), bus2);
            sistema.CriarViagem("Porto", "Portimão", new DateTime(2025, 9, 3, 05, 30, 0), bus3);

            string opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Reservas de Autocarro ===");
                Console.WriteLine("1) Listar viagens");
                Console.WriteLine("2) Ver mapa de lugares");
                Console.WriteLine("3) Criar reserva");
                Console.WriteLine("4) Cancelar reserva");
                Console.WriteLine("5) Procurar reservas por passageiro");
                Console.WriteLine("0) Sair");
                Console.WriteLine("----------------------------------------");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarViagens(sistema);
                        break;

                    case "2":
                        VerMapa(sistema);
                        break;

                    case "3":
                        CriarReserva(sistema);
                        break;

                    case "4":
                        CancelarReserva(sistema);
                        break;

                    case "5":
                        ProcurarReservas(sistema);
                        break;

                    case "0":
                        Console.WriteLine("\nA sair do sistema... até breve!");
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPrima qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != "0");
        }

        // ---------------- MÉTODOS DO MENU ----------------

        static void ListarViagens(SistemaReservas sistema)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Viagens ===");
            sistema.ListarViagens();
        }

        static void VerMapa(SistemaReservas sistema)
        {
            Console.Clear();
            Console.WriteLine("=== Ver Mapa de Lugares ===");
            var viagem = EscolherViagem(sistema);
            if (viagem == null) return;

            for (int i = 1; i <= viagem.Autocarro.NumeroLugares; i++)
            {
                bool ocupado = viagem.Reservas.Any(r => r.NumeroAssento == i);
                Console.Write(ocupado ? "[X] " : "[ ] ");
                if (i % 10 == 0) Console.WriteLine(); // Nova linha a cada 10 lugares
            }
        }

        static void CancelarReserva(SistemaReservas sistema)
        {
            Console.Clear();
            Console.WriteLine("=== Cancelar Reserva ===");
            var viagem = EscolherViagem(sistema);
            if (viagem == null) return;

            Console.Write("Número do assento a cancelar: ");
            if (int.TryParse(Console.ReadLine(), out int assento))
            {
                viagem.CancelarReserva(assento);
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }
        }

        static void ProcurarReservas(SistemaReservas sistema)
        {
            Console.Clear();
            Console.WriteLine("=== Procurar Reservas por Passageiro ===");
            Console.Write("Digite o nome ou NIF: ");
            string pesquisa = Console.ReadLine().ToLower();

            bool encontrou = false;

            foreach (var viagem in sistema.Viagens)
            {
                foreach (var reserva in viagem.Reservas)
                {
                    if (reserva.Passageiro.Nome.ToLower().Contains(pesquisa) ||
                        reserva.Passageiro.NIF.ToLower().Contains(pesquisa))
                    {
                        Console.WriteLine($"{reserva.Passageiro.Nome} - Assento {reserva.NumeroAssento} na viagem {viagem.Origem} → {viagem.Destino}");
                        encontrou = true;
                    }
                }
            }

            if (!encontrou)
                Console.WriteLine("Nenhuma reserva encontrada.");
        }

        static Viagem EscolherViagem(SistemaReservas sistema)
        {
            Console.WriteLine("\nViagens disponíveis:");
            for (int i = 0; i < sistema.Viagens.Count; i++)
                Console.WriteLine($"{i + 1}) {sistema.Viagens[i]}");

            Console.Write("Escolha o número da viagem: ");
            if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= sistema.Viagens.Count)
                return sistema.Viagens[escolha - 1];

            Console.WriteLine("Escolha inválida!");
            return null;
        }
    }
}
