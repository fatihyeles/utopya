using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public ItemContainer inventoryContainer;                     //GameManager'e baðlanan scriptler
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
    public DialogueSystem dialogueSystem;
    public PlaceableObjectsReferenceManager placeableObjects;
    public ItemList itemDB;
    public OnScreenMesageSystem mesageSystem;
    public ScreenTint screenTint;
}
