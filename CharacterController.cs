using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterController : MonoBehaviour
{

    CharacterController2d characterController2d; //Karakteri
                                              Character character;
    Rigidbody2D rgbd2d; //Bedeni tanımlama
    ToolbarController toolbarController; // Envanteri tanımlama
    Animator animator;
    [SerializeField] float offsetDistance = 1f; 
    [SerializeField] float sizeOfInteractableArea = 1.2f; //Karakterin etkileþime geçebileceği uzaklık
    [SerializeField] MarkerManager markerManager; 
    [SerializeField] TileMapReadController tileMapReadController; // Toprakları ayırmak için
    [SerializeField] float maxDistance = 1.5f; // Maks uzaklık.
    [SerializeField] ToolAction onTilePickUp;
    [SerializeField] IconHighlight iconHighlight;
    AttackController attackController;

    [SerializeField] int weaponEnergyCost = 5;
    Vector3Int selectedTilePosition;
    bool selectable;
    


    private void Awake()
    {
        character = GetComponent<Character>();
        characterController2d = GetComponent<CharacterController2d>();     //Karakteri kontrol ayarlarına bağlamak.
      //  
        rgbd2d = GetComponent<Rigidbody2D>();              // Bedeni kodlarına bağlamak.
        toolbarController = GetComponent<ToolbarController>(); //Envanteri kontrolü.
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            WeaponAction();
        }
        SelectTile(); //Toprak seçimi
        CanSelectCheck(); // Kontrol yapılabilir
        Marker(); 
        if (Input.GetMouseButtonDown(0))
        {
            if(UseToolWorld() == true) 
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void WeaponAction()
    {
        Item item = toolbarController.GetItem; 
        if (item == null) { return; }
        if (item.isWeapon == false) { return; }
        EnergyCost(weaponEnergyCost);

        Vector2 position = rgbd2d.position + characterController2d.lastMotionVector * offsetDistance;

        attackController.Attack(item.damage, characterController2d.lastMotionVector);
    }

    private void EnergyCost( int energyCost)
    {
        character.GetTired(energyCost);
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true); //Fare konumuna göre toprak seçimi.
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;   //Seçilebilir topraðýn fare konumuna ve karaktere
        markerManager.Show(selectable);
        //uzaklýða göre belirlenmesi
        iconHighlight.CanSelect = selectable;
    }
    private void Marker()
    {
      
        markerManager.markedCellPosition = selectedTilePosition; //Ýmlecin seçilen karede gözükmesi
        iconHighlight.cellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + characterController2d.lastMotionVector * offsetDistance; 

        Item item = toolbarController.GetItem; //Eþyalarý envanterde bulundurmak.
        if (item == null) { return false; }
        if (item.onAction == null) { return false; }

        EnergyCost(item.onAction.energyCost);
        // animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManeger.instance.inventoryContainer);
            }
        }

        return complete;
    }
    private void UseToolGrid()
    {
        if (selectable == true)
        {
            Item item = toolbarController.GetItem;
            if (item == null) {
                PickUpTile();
                return; }
            if (item.onTileMapAction == null) { return; }
            EnergyCost(item.onTileMapAction.energyCost);
            //animator.SetTrigger("act");// SONRA TOPRAK SÜRME ANİMASYONU EKLEYEBİLİRİZ.
            bool complete = item.onTileMapAction.OnApplyToTileMap(
                selectedTilePosition, 
                tileMapReadController, 
                item);

            if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManeger.instance.inventoryContainer);
                }
            }
            
        }
    }

    private void PickUpTile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
