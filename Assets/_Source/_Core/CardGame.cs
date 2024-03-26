using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CardGame : MonoBehaviour
{
    public static CardGame Instance { get; private set; }

    public Dictionary<CardInstance, CardView> cardViews = new();
    public List<CardAsset> possibleCards;
    public int handCapacity;
    
    [SerializeField] private int cardDuplicatesInDeck;

    [SerializeField] private GameObject cardViewPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        for (var i = 0; i < cardDuplicatesInDeck; i++)
        {
            foreach (var possibleCard in possibleCards)
            {
                CreateCard(possibleCard, 0);
            }
        }

        StartTurn();
    }

    private void CreateCardView(CardInstance cardInstance)
    {
        var cardView = Instantiate(cardViewPrefab).GetComponent<CardView>();
        cardView.Init(cardInstance);

        cardViews.Add(cardInstance, cardView);
    }

    private void CreateCard(CardAsset cardAsset, int layoutId)
    {
        var cardInstance = new CardInstance(cardAsset);
        var cardsInLayout = GetCardsInLayout(layoutId);

        CreateCardView(cardInstance);
        cardInstance.MoveToLayout(layoutId, cardsInLayout.Count + 1);
    }

    public List<CardView> GetCardsInLayout(int layoutId)
    {
        return (from cardInstance in cardViews.Keys
            where
                cardInstance.LayoutId == layoutId
            select
                cardViews[cardInstance]).ToList();
    }

    public void RecalculateLayout(int layoutId)
    {
        var cards = GetCardsInLayout(layoutId);
        for (var index = 0; index < cards.Count; index++)
        {
            var cardView = cards[index];
            cardView.CardInstance.CardPosition = index + 1;
        }
    }

    private void StartTurn()
    {
        const int deckLayout = 0;
        const int handLayout = 1;
        
        ShuffleLayout(deckLayout);
        for (var i = 0; i < handCapacity; i++)
        {
            var currentDeck = GetCardsInLayout(deckLayout);
            if (currentDeck.Count == 0)
            {
                return;
            }
            
            currentDeck[0].CardInstance.MoveToLayout(handLayout, GetCardsInLayout(handLayout).Count + 1);
            RecalculateLayout(deckLayout);
        }
    }

    private void ShuffleLayout(int layoutId)
    {
        var cardsInLayout = GetCardsInLayout(layoutId);

        foreach (var card in cardsInLayout)
        {
            var randomCard = cardsInLayout[Random.Range(0, cardsInLayout.Count)];
            (card.CardInstance.CardPosition, randomCard.CardInstance.CardPosition) = (
                randomCard.CardInstance.CardPosition,
                card.CardInstance.CardPosition);
        }
    }
}