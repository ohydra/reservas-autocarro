using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/////COMENTARIO

namespace reservas_autocarro
{
    class Program
    {
        static void Main(string[] args)
        static void Main()
        {

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
                        Console.WriteLine("\n[Opção: Listar viagens]");
                        // Chamar método ListarViagens();
                        break;

                    case "2":
                        Console.WriteLine("\n[Opção: Ver mapa de lugares]");
                        // Chamar método VerMapa();
                        break;

                    case "3":
                        Console.WriteLine("\n[Opção: Criar reserva]");
                        // Chamar método CriarReserva();
                        break;

                    case "4":
                        Console.WriteLine("\n[Opção: Cancelar reserva]");
                        // Chamar método CancelarReserva();
                        break;

                    case "5":
                        Console.WriteLine("\n[Opção: Procurar reservas por passageiro]");
                        // Chamar método ProcurarReservas();
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
    }
