using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace P422310540TM
{
    class Polinomio
    {
        public ListaCircular<string> Variables;
        public ListaCircular<int> Coeficiente;

        public Polinomio()
        {
            Variables = new ListaCircular<string>();
            Coeficiente = new ListaCircular<int>();
        }

        public void AgregarPolinomio(string variables, int coeficiente)
        {
            Variables.Agregar(variables);
            Coeficiente.Agregar(coeficiente);
        }

        public (string variables, int coeficiente) EliminarPolinomio()
        {
            string variable = Variables.Pop();
            int coeficiente = Coeficiente.Pop();

            return (variable, coeficiente);
        }
    }
}
