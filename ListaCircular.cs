using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P422310540TM
{
    public class ListaCircular<T>
    {
        public Nodo<T> primero, ultimo;
        public int cantidad = 0;

        public ListaCircular()
        {
            primero = null;
            ultimo = null;
        }

        public void Agregar(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T> { Dato = valor, Siguiente = null };

            if (primero == null)
            {
                primero = ultimo = nuevoNodo;
                nuevoNodo.Siguiente = primero;
            }
            else
            {
                nuevoNodo.Siguiente = primero;
                ultimo.Siguiente = nuevoNodo;
                ultimo = nuevoNodo;
            }
            cantidad++;
        }

        public bool PopByValue(T value)
        {
            Nodo<T> puntero = primero;
            Nodo<T> antesDelPuntero = null;

            T resultado = default;

            do
            {
                if (puntero.Dato.Equals(value))
                {
                    if (antesDelPuntero == null)
                    {
                        primero = primero.Siguiente;
                        ultimo.Siguiente = primero;
                    }
                    else
                    {
                        antesDelPuntero.Siguiente = puntero.Siguiente;
                    }
                    return true;
                }
                antesDelPuntero = puntero;
                puntero = puntero.Siguiente;
            } while (puntero != primero || primero == null);

            return false;
        }

        public T Pop()
        {
            Nodo<T> puntero = primero;
            Nodo<T> antesDelPuntero = null;
            T resultado = default;

            if (EstaVacio())
            {
                return default;
            }

            do
            {
                if (antesDelPuntero == ultimo && puntero == primero)
                {
                    resultado = ultimo.Dato;
                    PopByValue(puntero.Dato);

                    ultimo = primero = null;

                }
                else if (puntero == ultimo)
                {
                    resultado = ultimo.Dato;
                    antesDelPuntero.Siguiente = primero;
                }
                
                antesDelPuntero = puntero;
                puntero = puntero.Siguiente;
            } while (puntero != primero || primero == null || ultimo == null);

            ultimo = antesDelPuntero;

            return resultado;
        }

        public Nodo<T> Peek()
        {
            return ultimo;
        }

        public T ObtenerElementoEnIndice(int indice)
        {
            if (EstaVacio() )
            {
                throw new IndexOutOfRangeException("Índice fuera de rango.");
            }

            Nodo<T> nodoActual = primero;

            for (int i = 0; i < indice; i++)
            {
                nodoActual = nodoActual.Siguiente;
            }

            return nodoActual.Dato;
        }

        public void ModificarPorIndice(T dato, int indice)
        {
            if (EstaVacio())
            {
                throw new IndexOutOfRangeException("Índice fuera de rango.");
            }

            Nodo<T> nodoActual = primero;

            for (int i = 0; i < indice; i++)
            {
                nodoActual = nodoActual.Siguiente;
            }

            nodoActual.Dato = dato;
        }

        public bool EstaVacio()
        {
            return primero == null;
        }

        public int Contains(T value)
        {
            if (EstaVacio()) return -1;

            int indice = 0;
            Nodo<T> pointer = primero;
            do
            {
                if (pointer.Dato.Equals(value))
                {
                    return indice;
                }
                else
                {
                    pointer = pointer.Siguiente;
                    indice++;
                }
            } while (pointer != primero);
            return -1;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Contador())
                {
                    throw new IndexOutOfRangeException("Index is out of range - Get");
                }

                Nodo<T> pointer = primero;
                for (int i = 0; i < index; i++)
                {
                    pointer = pointer.Siguiente;
                }

                return pointer.Dato;
            }

            set
            {
                if (index < 0 || index >= Contador())
                {
                    throw new IndexOutOfRangeException("Index is out of range - Set");
                }

                Nodo<T> pointer = primero;
                for (int i = 0; i < index; i++)
                {
                    pointer = pointer.Siguiente;
                }

                pointer.Dato = value;
            }
        }

        public int Contador()
        {
            if (!EstaVacio())
            {
                Nodo<T> tempPointer = primero;
                int contador = 0;

                do
                {
                    contador++;
                    tempPointer = tempPointer.Siguiente;
                } while (tempPointer != primero);

                return contador;
            }
            else
                return 0;
        }
    }
}
