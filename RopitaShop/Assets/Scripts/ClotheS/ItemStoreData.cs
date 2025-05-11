using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStoreData", menuName = "BGS/ItemStoreData", order = 0)]
public class ItemStoreData : ScriptableObject
{
    public List<TypesItems> typesItems;
}

[System.Serializable]
public class ItemData
{
    public string itemID;
    public Sprite itemSprite;
    public int itemPrice;
    public bool isPurchased;
}

[System.Serializable]
public class TypesItems
{
    public ClotheType clotheType;
    public List<ItemData> items;
}