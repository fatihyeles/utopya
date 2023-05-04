using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    Store store;
    Currency money;

    ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    [SerializeField] ItemPanel inventoryItemPanel;

    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }
    public void BeginTrading(Store store)
    {
        this.store = store;
        itemStorePanel.SetInventory(store.storeContent);
        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);

    }

    internal void BuyItem(int id)
    {
        Item itemToBuy = store.storeContent.slots[id].item;
        int totalPrice = (int)(itemToBuy.price * store.sellToPlayerMultip);
        if (money.Check(totalPrice) == true)
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemToBuy);
            inventoryItemPanel.Show();
        }
    }

    public void StopTrading()
    {
        store = null;
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    public void SellItem()
    {
        if (GameManeger.instance.dragAndDropController.CheckForSale() == true)
        {
            ItemSlot itemToSell = GameManeger.instance.dragAndDropController.itemSlot;
            int moneyGain = itemToSell.item.stackable == true ? 
                (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip) :
                (int)(itemToSell.item.price * store.buyFromPlayerMultip);
            money.Add(moneyGain);
            itemToSell.Clear();
            GameManeger.instance.dragAndDropController.updateIcon();
        }
    }
}
