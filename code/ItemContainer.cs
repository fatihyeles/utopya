using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

//using static UnityEditor.Progress;

[Serializable]
public class ItemSlot //Eşyaların gözükeceği slotlar
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;  
        count = slot.count;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
    public void Clear()
    {
        item = null;  
        count = 0;
    }
}

    [CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;//eşyaların listesini

    public void Add(Item item, int count = 1)//eşya aldı zaman +1 sayıması 
    {
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);  //istilenebilen eşyalar
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);  //istilenemeyen eşyalar
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
        }

    public void Remove(Item itemToRemove, int count = 1)
    {
        if (itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if (itemSlot == null) { return; }

            itemSlot.count -= count;
            if (itemSlot.count <=0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if (itemSlot == null) { return; }

                itemSlot.Clear();
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot ıtemSlot = slots.Find(x => x.item == checkingItem.item);

        if (ıtemSlot == null) { return false; }

        if (checkingItem.item.stackable) { return ıtemSlot.count > checkingItem.count; }

        return true;
    }
}

