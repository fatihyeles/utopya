using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string Name; //Eşyanın ismi
    public bool stackable; //istilenebilir eşya
    public Sprite icon; //Eşyanýn ikonu
    public ToolAction onAction; //Eşyanın kullanımı
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
}
