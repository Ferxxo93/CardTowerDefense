using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;

public class CardsManager : MonoBehaviour
{
    public PilaCartas deck = new PilaCartas();
    public PilaCartas discardPile = new PilaCartas();
    public Transform[] cardSlots;
    public bool[] availableCardsSlots;
    private Card[] cartasEnMano = new Card[4];
    public GameObject Knight;
    public GameObject Archer;
    public GameObject FallenTree;
    [SerializeField] private List<Transform> spawns;
    [SerializeField] private GameObject meleeAlliePrefab;
    [SerializeField] private GameObject rangeAlliePrefab;
    public TMP_Text deckSizeText;
    public TMP_Text discardPileText;
    [SerializeField] private GameObject Allie;
    private AllieType type;
    public ABB_Card arbolCartas = new ABB_Card();
    public ManaManager manaManager;
    private GraphManager graphManager;

    void Start()
    {
        arbolCartas.InicializarArbol();
        Card card = arbolCartas.BuscarPorMana(3);
        graphManager = GraphManager.Instance;
        if (card != null)
        {
            Debug.Log("Carta encontrada con mana 3: " + card.name);
        }
        else
        {
            Debug.Log("No se encontró carta con mana 3");
        }
    }

    public void PushToDeck(Card card)
    {
        deck.Apilar(card);
    }

    public Card PopFromDeck()
    {
        if (!deck.Vacia())
        {
            Card c = deck.Tope();
            deck.Desapilar();
            return c;
        }
        return null;
    }

    public void PushToDiscard(Card card)
    {
        discardPile.Apilar(card);
    }

    public Card PopFromDiscard()
    {
        if (!discardPile.Vacia())
        {
            Card c = discardPile.Tope();
            discardPile.Desapilar();
            return c;
        }
        return null;
    }



    public void DrawCard()
    {
        if (deck.Count() > 0)
        {
            Card topCard = PopFromDeck();

            for (int i = 0; i < availableCardsSlots.Length; i++)
            {
                if (availableCardsSlots[i])
                {
                    topCard.gameObject.SetActive(true);
                    topCard.handIndex = i;
                    topCard.transform.position = cardSlots[i].position;
                    topCard.transform.SetParent(cardSlots[i]);
                    topCard.hasBeenPlayed = false;

                    availableCardsSlots[i] = false;
                    return;
                }
            }

            PushToDeck(topCard);
        }
    }

    public void DiscardCard(Card card)
    {
        card.gameObject.SetActive(false);
        PushToDiscard(card);
        availableCardsSlots[card.handIndex] = true;
    }

    public void Undo()
    {
        Card lastDiscard = PopFromDiscard();
        if (lastDiscard != null)
        {
            PushToDeck(lastDiscard);
            lastDiscard.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (deckSizeText != null)
            deckSizeText.text = deck.Count().ToString();

        if (discardPileText != null)
            discardPileText.text = discardPile.Count().ToString();
    }

    public void InitializeDeck(List<Card> cards)
    {
        arbolCartas.InicializarArbol();

        foreach (Card card in cards)
        {
            PushToDeck(card);
            arbolCartas.AgregarElem(card);
        }

        deck.Clear();
        foreach (Card card in cards)
        {
            PushToDeck(card);
            card.gameObject.SetActive(false);
        }
    }


    public void TryPlayCard(Card card)
    {
        if (manaManager.SpendMana(card.manaCost))
        {
            if (card.cardType == CardType.Tree)
            {
                graphManager.BloquearNodoAleatorioTemporal(10f);  
            }
            SpawnObjectForCard(card);
            DiscardCard(card);
        }
        else
        {
            Debug.Log("No hay suficiente maná para esta carta.");
        }
    }

    public void SpawnObjectForCard(Card card)
    {


        foreach (Transform spawnPoint in spawns)
        {

            switch (card.cardType)
            {
                case CardType.Knight:

                    Instantiate(meleeAlliePrefab, spawnPoint.position, Quaternion.identity);

                    break;
                case CardType.Archer:
                    Instantiate(rangeAlliePrefab, spawnPoint.position, Quaternion.identity);

                    break;
                case CardType.Tree:
                    Instantiate(FallenTree, spawnPoint.position, Quaternion.identity);
                    break;
            }


        }
    }

    public void OrdenarCartasDisponiblesPorMana(bool menorAMayor)
    {
        List<Card> cartasEnMano = new List<Card>();
        foreach (Transform slot in cardSlots)
        {
            foreach (Transform child in slot)
            {
                Card c = child.GetComponent<Card>();
                if (c != null && c.gameObject.activeSelf)
                {
                    cartasEnMano.Add(c);
                }
            }
        }
        if (menorAMayor)
            cartasEnMano.Sort((a, b) => a.manaCost.CompareTo(b.manaCost));
        else
            cartasEnMano.Sort((a, b) => b.manaCost.CompareTo(a.manaCost));

        for (int i = 0; i < cartasEnMano.Count && i < cardSlots.Length; i++)
        {
            Card carta = cartasEnMano[i];
            Transform slot = cardSlots[i];

            carta.transform.SetParent(slot);
            carta.transform.position = slot.position;

            carta.handIndex = i;
        }
    }
}