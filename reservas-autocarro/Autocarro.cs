using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public class Autocarro : Veiculo
    {
        public int Numero { get; set; }
        public string Motorista { get; set; }

        public Autocarro(int numero, string matricula, int numeroLugares = 40, string motorista = "Desconhecido")
            : base(matricula, numeroLugares)
        {
            Numero = numero;
            Motorista = motorista;
        }

        public override void MostrarInfo()
        {
            Console.WriteLine($"Autocarro Nº {Numero} | Matrícula: {Matricula} | Lugares: {NumeroLugares} | Motorista: {Motorista}");
        }

        public override string ToString()
        {
            return $"Autocarro Nº {Numero} ({Matricula}) - {NumeroLugares} lugares";
        }
    }
}
