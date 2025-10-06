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

            // Criar autocarros e viagens
            Autocarro bus1 = new Autocarro(101, "AA-12-BB", 40, "João Mendes");
            Autocarro bus2 = new Autocarro(202, "CC-34-DD", 40, "Carla Silva");
            sistema.AdicionarAutocarro(bus1);
            sistema.AdicionarAutocarro(bus2);

            sistema.CriarViagem("Lisboa", "Porto", new DateTime(2025, 10, 7, 9, 0, 0), bus1);
            sistema.CriarViagem("Porto", "Braga", new DateTime(2025, 10, 7, 14, 30, 0), bus2);

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

            Console.WriteLine($"\nLugares totais: 40");
            Console.WriteLine($"Lugares disponíveis: {viagem.Autocarro.NumeroLugares}\n");
            Console.WriteLine("Legenda: [X] = Ocupado   [nº] = Livre\n");

            // Mostra o mapa dos lugares com números
            for (int i = 1; i <= 40; i++)
            {
                bool ocupado = viagem.Reservas.Any(r => r.NumeroAssento == i);

                if (ocupado)
                    Console.Write("[X]".PadRight(5));   // Lugar ocupado
                else
                    Console.Write($"[{i}]".PadRight(5)); // Mostra o número do lugar livre

                if (i % 4 == 2) Console.Write("   "); // Espaço entre os dois lados do autocarro
                if (i % 4 == 0) Console.WriteLine(); // Nova linha a cada 4 lugares
            }

            Console.WriteLine();
        }

        static bool ValidarNIF(string nif)
        {
            // Verifica se tem exatamente 9 dígitos
            if (!Regex.IsMatch(nif, @"^\d{9}$"))
                return false;

            // Converter para array de inteiros
            int[] digits = nif.Select(c => int.Parse(c.ToString())).ToArray();

            // Cálculo do dígito de controlo (módulo 11)
            int soma = 0;
            for (int i = 0; i < 8; i++)
            {
                soma += digits[i] * (9 - i);
            }

            int resto = soma % 11;
            int checkDigit = resto < 2 ? 0 : 11 - resto;

            return digits[8] == checkDigit;
        }

        static void CriarReserva(SistemaReservas sistema)
        {
            Console.Clear();
            Console.WriteLine("=== Criar Reserva ===");
            var viagem = EscolherViagem(sistema);
            if (viagem == null) return;

            string nome;
            do
            {
                Console.Write("Nome do passageiro: ");
                nome = Console.ReadLine().Trim();

                // Validação - Nome deve ter pelo menos 2 letras e apenas letras/espaços
                if (string.IsNullOrWhiteSpace(nome) || nome.Length < 2 || !Regex.IsMatch(nome, @"^[A-Za-zÀ-ÿ\s]+$"))
                    Console.WriteLine(" Nome inválido! Use apenas letras e espaços (mínimo 2 caracteres).");
                else
                    break;

            } while (true);


            // === Validação do NIF ===
            string nif;
            do
            {
                Console.Write("NIF (9 dígitos): ");
                nif = Console.ReadLine();

                if (!ValidarNIF(nif))
                    Console.WriteLine(" NIF inválido! Tente novamente.");
                else
                    break;

            } while (true);

            // === Verificar se já existe reserva com o mesmo NIF nessa viagem ===
            bool jaReservou = viagem.Reservas.Any(r => r.Passageiro.NIF == nif);
            if (jaReservou)
            {
                Console.WriteLine($"\n O passageiro com NIF {nif} já possui uma reserva nesta viagem ({viagem.DataHora:dd/MM/yyyy}).");
                return;
            }

            Console.WriteLine("\n=== Mapa de Lugares ===");
            Console.WriteLine("Legenda: [X] = Ocupado   [nº] = Livre\n");

            for (int i = 1; i <= 40; i++)
            {
                bool ocupado = viagem.Reservas.Any(r => r.NumeroAssento == i);

                if (ocupado)
                    Console.Write("[X]".PadRight(5));  // lugar ocupado
                else
                    Console.Write($"[{i}]".PadRight(5)); // mostra número livre

                if (i % 4 == 2) Console.Write("   "); // Espaço entre os dois lados do autocarro
                if (i % 4 == 0) Console.WriteLine(); // Nova linha a cada 4 lugares
            }

            Console.WriteLine();

            // === Escolher número do assento ===
            int assento;
            do
            {
                Console.Write("\nEscolha o número do assento livre: ");
                if (!int.TryParse(Console.ReadLine(), out assento) ||
                    assento < 1 ||
                    assento > 40)
                {
                    Console.WriteLine("❌ Número inválido! Tente novamente.");
                    continue;
                }

                bool ocupado = viagem.Reservas.Any(r => r.NumeroAssento == assento);
                if (ocupado)
                {
                    Console.WriteLine("⚠️ Esse assento já está ocupado. Escolha outro.");
                }
                else
                    break;

            } while (true);

            // === Criar e adicionar a reserva ===
            Passageiro passageiro = new Passageiro(nome, nif);
            Reserva reserva = new Reserva(passageiro, assento, viagem);
            viagem.AdicionarReserva(reserva);

            // === Diminui os lugares disponiveis ===
            viagem.Autocarro.NumeroLugares--;

            Console.WriteLine($"\n Reserva criada com sucesso para {nome} (Assento {assento}).");
            Console.WriteLine($"Lugares restantes: {viagem.Autocarro.NumeroLugares}");

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
                viagem.Autocarro.NumeroLugares++;
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
