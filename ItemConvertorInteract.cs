using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConvertorData
{
    public ItemSlot itemSlot;
    public int timer;
    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
    }
}

[RequireComponent(typeof(TimeAgent))]
public class ItemConvertorInteract : Interactable, IPersistant
{
    [SerializeField] Item convertableItem;
    [SerializeField] Item producedItem;
    [SerializeField] int producedItemCount = 1;




    [SerializeField] int timeToprocess = 5;
    ItemConvertorData data;

    Animator animator;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += ItemConvertProcess; 
        if (data == null)
        {
            data = new ItemConvertorData();
        }
        animator = GetComponent<Animator>();
        Animate();

    }

    private void ItemConvertProcess(DayTimeController dayTimeController)
    {
        if (data.itemSlot == null) { return; }
        if (data.timer > 0)
        {
            data.timer -= 1;
            if (data.timer <= 0)
            {
                CompleteItemConversion();

            }
        }
    }

    public override void Interact(Character character)
    {
        if (data.itemSlot.item == null)
        {
            if (GameManeger.instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing(GameManeger.instance.dragAndDropController.itemSlot);
                return;
            }
            ToolbarController toolbarController = character.GetComponent<ToolbarController>(); 
            if (toolbarController == null) { return; }
            ItemSlot itemSlot = toolbarController.GetItemSlot;
            if ( itemSlot.item == convertableItem)
            {
                StartItemProcessing(itemSlot); return;
            }
        }
        if (data.itemSlot.item != null && data.timer <= 0)
        {
            GameManeger.instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }
    }

    private void StartItemProcessing(ItemSlot toProcess)
    {
        
        data.itemSlot.Copy(GameManeger.instance.dragAndDropController.itemSlot);
        data.itemSlot.count = 1;
       if (toProcess.item.stackable)
        {
            toProcess.count -= 1;
            if (toProcess.count < 0)
            {
                toProcess.Clear();
            }
        }
        else
        {
            toProcess.Clear();

        }
        data.timer = timeToprocess;
        Animate();
    }

    private void Animate()
    {
        animator.SetBool("Working", data.timer > 0f);
    }

   

    private void CompleteItemConversion()
    {

        Animate();
        data.itemSlot.Clear();
        data.itemSlot.Set(producedItem, producedItemCount);

    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    }
}
