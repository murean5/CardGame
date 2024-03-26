using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CardLayout : MonoBehaviour
{
    [SerializeField] public int layoutId;
    [SerializeField] public Vector2 offset;
    [SerializeField] private bool faceUp;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var sameLayoutCardViews = CardGame.Instance.GetCardsInLayout(layoutId);

        foreach (var cardView in sameLayoutCardViews)
        {
            cardView.transform.localPosition =
                _rectTransform.position + new Vector3(offset.x * cardView.CardInstance.CardPosition,
                    offset.y * cardView.CardInstance.CardPosition,
                    -cardView.CardInstance.CardPosition) - Vector3.left * offset.x;
            cardView.transform.SetSiblingIndex(cardView.CardInstance.CardPosition);

            cardView.Rotate(faceUp);
        }
    }
}