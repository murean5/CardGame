using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstance
{
    public CardAsset CardAsset { get; private set; }
    public int LayoutId;
    public int CardPosition;
    
    public CardInstance(CardAsset cardAsset)
    {
        CardAsset = cardAsset;
    }

    public void MoveToLayout(int layoutId, int cardPosition)
    {
        LayoutId = layoutId;
        CardPosition = cardPosition;
    }
}
