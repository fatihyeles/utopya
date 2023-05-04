using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;
    public Action<int> onChange;
    [SerializeField] IconHighlight iconHighlight;
    public ItemSlot GetItemSlot
    {
        get
        {
            return GameManeger.instance.inventoryContainer.slots[selectedTool];
        }
    }
    public Item GetItem
    {
        get
        {
            return GameManeger.instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    private void Start()
    {
        onChange += UpdateHighlightIcon;
        UpdateHighlightIcon(selectedTool);
    }


    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }


    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
    public void UpdateHighlightIcon(int id = 0)
    {
        Item item = GetItem;
        if (item == null)
        {
            iconHighlight.Show = false;
            return;
        }
        iconHighlight.Show = item.iconHighlight;
        if (item.iconHighlight)
        {
            iconHighlight.Set(item.icon);
        }
    }

}
