using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaCartas
{
    private Card[] elementos;
    private int indice;

    public PilaCartas(int capacidad = 100)
    {
        elementos = new Card[capacidad];
        indice = 0;
    }

    public void Inicializar()
    {
        indice = 0;
    }

    public void Apilar(Card c)
    {
        if (indice < elementos.Length)
        {
            elementos[indice] = c;
            indice++;
        }
        else
        {
            Debug.LogWarning("La pila está llena.");
        }
    }

    public void Desapilar()
    {
        if (!Vacia())
            indice--;
    }

    public Card Tope()
    {
        if (!Vacia())
            return elementos[indice - 1];
        return null;
    }

    public bool Vacia()
    {
        return indice == 0;
    }

    public int Count()
    {
        return indice;
    }   

    public void Clear()
    {
        indice = 0;
    }
}

