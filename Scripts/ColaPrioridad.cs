using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaPrioridad
{
    private int[] elementos;
    private int[] prioridades;
    private int indice;

    public void InicializarCola()
    {
        elementos = new int[100];
        prioridades = new int[100];
        indice = 0;
    }

    public void AcolarPrioridad(int valor, int prioridad)
    {
        int i;
        for (i = indice; i > 0 && prioridades[i - 1] > prioridad; i--)
        {
            elementos[i] = elementos[i - 1];
            prioridades[i] = prioridades[i - 1];
        }

        elementos[i] = valor;
        prioridades[i] = prioridad;
        indice++;
    }

    public void Desacolar()
    {
        if (!ColaVacia())
            indice--;
    }

    public int Primero()
    {
        return elementos[indice - 1];
    }

    public bool ColaVacia()
    {
        return indice == 0;
    }
}
