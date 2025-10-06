using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservas_autocarro
{
    public class Passageiro : Pessoa
    {
        public Passageiro(string nome, string nif) : base(nome, nif)
        {
        }

        public override void MostrarDados()
        {
            Console.WriteLine($"Passageiro: {Nome} (NIF: {NIF})");
        }

        public override string ToString()
        {
            return $"{Nome} (NIF: {NIF})";
        }
    }
}
