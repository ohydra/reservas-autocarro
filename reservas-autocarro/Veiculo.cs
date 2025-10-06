using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public class Veiculo
    {
        public string Matricula { get; set; }
        public int NumeroLugares { get; set; }

        public Veiculo(string matricula, int numeroLugares)
        {
            Matricula = matricula;
            NumeroLugares = numeroLugares;
        }

        public virtual void MostrarInfo()
        {
            Console.WriteLine($"Veículo com matrícula {Matricula} e {NumeroLugares} lugares.");
        }
    }
}
