using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ABBTDA_Card
{
    Card Raiz();
    ABBTDA_Card HijoIzq();
    ABBTDA_Card HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(Card c);
    void EliminarElem(int manaCost);
    Card BuscarPorMana(int manaCost);
    Card Menor(ABBTDA_Card a);
    Card Mayor(ABBTDA_Card a);
}

public class NodoABB_Card
{
    public Card info;
    public ABBTDA_Card hijoIzq;
    public ABBTDA_Card hijoDer;
}

public class ABB_Card : ABBTDA_Card
{
    NodoABB_Card raiz;

    public void InicializarArbol()
    {
        raiz = null;
    }

    public bool ArbolVacio()
    {
        return raiz == null;
    }

    public Card Raiz()
    {
        return raiz.info;
    }

    public ABBTDA_Card HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public ABBTDA_Card HijoDer()
    {
        return raiz.hijoDer;
    }

    public void AgregarElem(Card c)
    {
        if (ArbolVacio())
        {
            raiz = new NodoABB_Card();
            raiz.info = c;
            raiz.hijoIzq = new ABB_Card();
            raiz.hijoIzq.InicializarArbol();
            raiz.hijoDer = new ABB_Card();
            raiz.hijoDer.InicializarArbol();
        }
        else if (c.manaCost < raiz.info.manaCost)
        {
            raiz.hijoIzq.AgregarElem(c);
        }
        else if (c.manaCost > raiz.info.manaCost)
        {
            raiz.hijoDer.AgregarElem(c);
        }
    }

    public void EliminarElem(int manaCost)
    {
        if (!ArbolVacio())
        {
            if (manaCost == raiz.info.manaCost && raiz.hijoIzq.ArbolVacio() && raiz.hijoDer.ArbolVacio())
            {
                raiz = null;
            }
            else if (manaCost == raiz.info.manaCost && !raiz.hijoIzq.ArbolVacio())
            {
                raiz.info = Mayor(raiz.hijoIzq);
                raiz.hijoIzq.EliminarElem(raiz.info.manaCost);
            }
            else if (manaCost == raiz.info.manaCost)
            {
                raiz.info = Menor(raiz.hijoDer);
                raiz.hijoDer.EliminarElem(raiz.info.manaCost);
            }
            else if (manaCost < raiz.info.manaCost)
            {
                raiz.hijoIzq.EliminarElem(manaCost);
            }
            else
            {
                raiz.hijoDer.EliminarElem(manaCost);
            }
        }
    }

    public Card BuscarPorMana(int manaCost)
    {
        if (ArbolVacio()) return null;

        if (manaCost == raiz.info.manaCost)
            return raiz.info;
        else if (manaCost < raiz.info.manaCost)
            return raiz.hijoIzq.BuscarPorMana(manaCost);
        else
            return raiz.hijoDer.BuscarPorMana(manaCost);
    }

    public Card Menor(ABBTDA_Card a)
    {
        if (a.HijoIzq().ArbolVacio())
            return a.Raiz();
        else
            return Menor(a.HijoIzq());
    }

    public Card Mayor(ABBTDA_Card a)
    {
        if (a.HijoDer().ArbolVacio())
            return a.Raiz();
        else
            return Mayor(a.HijoDer());
    }
}
