using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace reservas_autocarro
{

    public class Pessoa
    {
        public string Nome { get; set; }
        public string NIF { get; set; }

        public Pessoa(string nome, string nif)
        {
            Nome = nome;
            NIF = nif;
        }

        public virtual void MostrarDados()
        {
            Console.WriteLine($"Nome: {Nome}, NIF: {NIF}");
        }
    }
}


