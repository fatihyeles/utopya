using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/Player Data")]

public class PlayerData : ScriptableObject
{
    public string characterName;
    public Gender playerCharacterGender;
    public  int saveSlotId;
}
