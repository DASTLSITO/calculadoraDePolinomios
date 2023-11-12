using MathNet.Numerics;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System.Drawing;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace P422310540TM
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == "" || textBox2.Text == ""))
            {
                Polinomio polinomio1 = new Polinomio();
                Polinomio polinomio2 = new Polinomio();

                string polinomio1String = textBox1.Text.Trim();
                string polinomio2String = textBox2.Text.Trim();

                polinomio1String = polinomio1String.Replace(" ", "");
                polinomio2String = polinomio2String.Replace(" ", "");

                AgregarADictionary(ref polinomio1, ref polinomio2, polinomio1String, polinomio2String);

                Polinomio resultado = SumarPolinomios(polinomio1, polinomio2);

                label1.Text = "Resultado: " + PolinomioAString(resultado);
            }
        }

        static void AgregarADictionary(ref Polinomio polinomio1, ref Polinomio polinomio2, string polinomioString1, string polinomioString2)
        {
            string[] terminos1 = polinomioString1.Split(new[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
            string[] terminos2 = polinomioString2.Split(new[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (polinomioString1[0] != '-')
                polinomioString1 = "+" + polinomioString1;

            if (polinomioString2[0] != '-')
                polinomioString2 = "+" + polinomioString2;

            Regex regex = new Regex(@"(?<coeficiente>[0-9]?\d*)"); ;

            // Crear un array para almacenar los operadores
            char[] operadores1 = new char[polinomioString1.Length];
            char[] operadores2 = new char[polinomioString2.Length];

            int posicion = 0;

            // Copiar los operadores al array
            for (int i = 0; i < operadores1.Length; i++)
            {

                if (polinomioString1[i] == '+' || polinomioString1[i] == '-')
                {
                    operadores1[posicion] = polinomioString1[i];
                    posicion++;
                }
            }

            posicion = 0;

            for (int i = 0; i < operadores2.Length; i++)
            {

                if (polinomioString2[i] == '+' || polinomioString2[i] == '-')
                {
                    operadores2[posicion] = polinomioString2[i];
                    posicion++;
                }
            }

            int terminoOperacion = 0;
            foreach (var termino in terminos1)
            {

                // Verificar las coincidencias 
                Match match = regex.Match(termino.Trim());

                if (match.Success)
                {
                    // Obtener coeficiente y variables
                    int coeficiente = string.IsNullOrEmpty(match.Groups["coeficiente"].Value) ? 1 : int.Parse(match.Groups["coeficiente"].Value);
                    int cantidad = coeficiente == 1 ? 0 : coeficiente.ToString().Length;

                    string variables = "";

                    for (int i = cantidad; i < termino.Length; i++)
                    {
                        variables += termino[i];
                    }

                    // Determinar el signo del término
                    int signo = operadores1[terminoOperacion] == '-' ? -1 : 1;

                    // Construir la clave del diccionario
                    string clave = variables;

                    int indice = polinomio1.Variables.Contains(clave);

                    // Agregar al diccionario
                    if (indice != -1)
                    {
                        polinomio1.Coeficiente.ModificarPorIndice(polinomio1.Coeficiente[indice] + (signo * coeficiente), indice);
                    }
                    else
                    {
                        polinomio1.AgregarPolinomio(clave, signo * coeficiente);
                    }
                    terminoOperacion++;
                }
            }

            terminoOperacion = 0;
            foreach (var termino in terminos2)
            {

                // Verificar las coincidencias 
                Match match = regex.Match(termino.Trim());

                if (match.Success)
                {
                    // Obtener coeficiente y variables
                    int coeficiente = string.IsNullOrEmpty(match.Groups["coeficiente"].Value) ? 1 : int.Parse(match.Groups["coeficiente"].Value);
                    int cantidad = coeficiente == 1 ? 0 : coeficiente.ToString().Length;

                    string variables = "";

                    for (int i = cantidad; i < termino.Length; i++)
                    {
                        variables += termino[i];
                    }

                    // Determinar el signo del término
                    int signo = operadores2[terminoOperacion] == '-' ? -1 : 1;

                    // Construir la clave del diccionario
                    string clave = variables;

                    int indice = polinomio2.Variables.Contains(clave);

                    // Agregar al diccionario
                    if (indice != -1)
                    {
                        polinomio2.Coeficiente.ModificarPorIndice(polinomio2.Coeficiente[indice] + (signo * coeficiente), indice);
                    }
                    else
                    {
                        polinomio2.AgregarPolinomio(clave, signo * coeficiente);
                    }
                    terminoOperacion++;
                }
            }
        }

        static Polinomio SumarPolinomios(Polinomio polinomio1, Polinomio polinomio2)
        {
            Polinomio resultado = new();

            ListaCircular<string> variables2 = polinomio2.Variables;
            ListaCircular<int> coeficientes2 = polinomio2.Coeficiente;
            Nodo<string> variablesNodo2 = variables2.primero;
            Nodo<int> coeficientesNodo2 = coeficientes2.primero;

            ListaCircular<string> variables1 = polinomio1.Variables;
            ListaCircular<int> coeficientes1 = polinomio1.Coeficiente;
            Nodo<string> variablesNodo1 = variables1.primero;
            Nodo<int> coeficientesNodo1 = coeficientes1.primero;
            int indice = 0;

            do
            {
                indice = resultado.Variables.Contains(variablesNodo1.Dato);
                if (indice != -1)
                {
                    resultado.Coeficiente.ModificarPorIndice(resultado.Coeficiente[indice] + coeficientesNodo1.Dato, indice);
                }
                else
                {
                    resultado.AgregarPolinomio(variablesNodo1.Dato, coeficientesNodo1.Dato);
                }

                coeficientesNodo1 = coeficientesNodo1.Siguiente;
                variablesNodo1 = variablesNodo1.Siguiente;
            } while (variablesNodo1 != variables1.primero);

            do
            {
                indice = resultado.Variables.Contains(variablesNodo2.Dato);
                if (indice != -1)
                {
                    resultado.Coeficiente.ModificarPorIndice(resultado.Coeficiente[indice] + coeficientesNodo2.Dato, indice);
                }
                else
                {
                    resultado.AgregarPolinomio(variablesNodo2.Dato, coeficientesNodo2.Dato);
                }

                coeficientesNodo2 = coeficientesNodo2.Siguiente;
                variablesNodo2 = variablesNodo2.Siguiente;
            } while (variablesNodo2 != variables2.primero);

            return resultado;
        }

        static string PolinomioAString(Polinomio polinomio)
        {
            string resultado = "";

            ListaCircular<string> variables = polinomio.Variables;
            ListaCircular<int> coeficientes = polinomio.Coeficiente;
            Nodo<string> variablesNodo = variables.primero;
            Nodo<int> coeficientesNodo = coeficientes.primero;

            do
            {
                if (coeficientesNodo.Dato != 0)
                {
                    if (resultado != "")
                    {
                        if (coeficientesNodo.Dato < 0)
                            resultado += " ";
                        else
                            resultado += " + ";
                    }

                    resultado += coeficientesNodo.Dato.ToString();

                    if (variablesNodo.Dato != "")
                    {
                        resultado += variablesNodo.Dato;
                    }
                }

                variablesNodo = variablesNodo.Siguiente;
                coeficientesNodo = coeficientesNodo.Siguiente;
            } while (variablesNodo != variables.primero);

            return resultado;
        }
    }
}