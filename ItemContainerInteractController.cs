using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;
    InventoryController InventoryController;
    [SerializeField] ItemContainerPanel ItemContainerPanel;
    Transform openedChest;
    [SerializeField] float maxDistance = 0.8f;
    private void Awake()
    {
        InventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        if(openedChest !=null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            if (distance > maxDistance)
            {
                openedChest.GetComponent<LootContainerInteract>().Close(GetComponent<Character>());
            }
        }
    }
    public void Open(ItemContainer itemContainer, Transform _openedChest)
    {
        targetItemContainer = itemContainer;
        ItemContainerPanel.inventory = targetItemContainer;
        InventoryController.Open();
        ItemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }

    public void Close()
    {
        InventoryController.Close();
        ItemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }

    
}
