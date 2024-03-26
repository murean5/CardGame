using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour
{
    public CardInstance CardInstance { get; private set; }

    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private SpriteRenderer shirtRenderer;
    
    public void Init(CardInstance cardInstance)
    {
        CardInstance = cardInstance;
        faceRenderer.sprite = cardInstance.CardAsset.sprite;
    }

    public void Rotate(bool faceUp)
    {
        faceRenderer.enabled = faceUp;
        shirtRenderer.enabled = !faceUp;
    }

    private void PlayCard()
    {
        switch (CardInstance.LayoutId)
        {
            case 1:
                CardInstance.MoveToLayout(3, CardGame.Instance.GetCardsInLayout(3).Count + 1);
                CardGame.Instance.RecalculateLayout(1);
                break;
            case 3:
                CardInstance.MoveToLayout(2, CardGame.Instance.GetCardsInLayout(2).Count + 1);
                break;
        }

        CardGame.Instance.RecalculateLayout(3);
    }

    private void OnMouseDown()
    {
        PlayCard();
    }
}