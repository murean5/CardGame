using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Card Asset", fileName = "CardAsset")]
public class CardAsset : ScriptableObject
{
    public new string name;
    public Color color;
    public Sprite sprite;
}
