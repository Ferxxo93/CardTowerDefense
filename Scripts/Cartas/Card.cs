using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum CardType
    {
        Knight,
        Archer,
        Tree
    }
public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;
    private CardsManager gm;
    public CardType cardType;
    public int manaCost = 3;

    private void Start()
    {
        gm = FindObjectOfType<CardsManager>();
    }

    private void OnMouseDown()
    {
        if (!hasBeenPlayed)
        {
            if (gm != null)
            {
                gm.TryPlayCard(this);
            }
        }
    }

    void MoveToDiscardPile()
    {
        gm.DiscardCard(this); 
        gameObject.SetActive(false);
    }
}