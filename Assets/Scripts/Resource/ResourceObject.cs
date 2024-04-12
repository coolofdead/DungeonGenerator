using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceName", menuName = "ScriptableObjects/ResourceObject", order = 2)]
public class ResourceObject : ScriptableObject
{
    public ItemType type;
    public ItemRarity rarity;
    public new string name;
    public string desc;
    public Sprite sprite;
    public GameObject prefab;
    public int price;
}

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary,
}
