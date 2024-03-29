﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string Name; //Eşyanın ismi
    public int id;
    public bool stackable; //istilenebilir eşya
    public Sprite icon; //Eşyanýn ikonu
    public ToolAction onAction; //Eşyanın kullanımı
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public bool iconHighlight;
    public GameObject itemPrefab;
    public bool isWeapon;
    public int damage = 10;
    public int price = 100;
    public bool canBeSold = true;
}
