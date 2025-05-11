using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ClotheStoreUI : MenuWindow
{
    [SerializeField] private ItemStoreData _itemStoreData;
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<ItemClotheStore> _hairItems = new List<ItemClotheStore>();
    [SerializeField] private List<ItemClotheStore> _torsoItems = new List<ItemClotheStore>();
    [SerializeField] private PlayerMov _playerMov;
    private ClotheType _currentClotheType = ClotheType.Torso;
    
    protected override void Initialize()
    {
        base.Initialize();
        _closeButton.onClick.AddListener(() => CloseWindow());
        InitializeItems();
    }

    private void CloseWindow()
    {
        HideWindow();
        _playerMov.EnableMovement(true);
    }
    
    public override void ShowWindow()
    {
        base.ShowWindow();
        InitializeItems();
    }
    

    private void InitializeItems()
    {
        InitializeItemsOfType(ClotheType.Hair, _hairItems);
        InitializeItemsOfType(ClotheType.Torso, _torsoItems);
    }

    private void InitializeItemsOfType(ClotheType clotheType, List<ItemClotheStore> items)
    {
        var typesItems = _itemStoreData.typesItems
            .Where(typesItem => typesItem.clotheType == clotheType)
            .SelectMany(typesItem => typesItem.items)
            .ToList();

        if (items.Count != typesItems.Count)
        {
            Debug.Log("The number of ItemClotheStore objects and ItemData objects do not match for " + clotheType);
            return;
        }

        var pairedItems = items.Zip(typesItems, (item, itemData) => new {Item = item, ItemData = itemData});

        foreach (var pair in pairedItems)
        {
            int index = items.IndexOf(pair.Item);
            pair.Item.Initialize(pair.ItemData.itemID, pair.ItemData.itemSprite, pair.ItemData.itemPrice, clotheType, index);
        }

    }
}
public enum ClotheType
{
    Hair,
    Torso,
}

