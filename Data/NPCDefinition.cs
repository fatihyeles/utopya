using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    Male,
    Female,
    [InspectorName("Any [For romance only]")]
    Any
}

[Serializable]
public class PortraitsCollection
{
    public Texture2D normal;
    public Texture2D surprised;
    public Texture2D confused;
    public Texture2D angry;
}

[CreateAssetMenu(menuName ="Data/NPC Character")]
public class NPCDefinition : ScriptableObject
{
    public string Name = "Nameless";
    public Gender gender = Gender.Male;

    public PortraitsCollection portraits;

    public GameObject characterPrefab;

    [Header("Interaction")]
    public bool canBeRomanced;
    public Gender romancableGender;

    public List<Item> itemLikes;
    public List<Item> itemDislikes;

    [Header("Dialogues")]
    public List<DialogueContainer> genaralDialogues;

    [Header("Schedule WIP")]
    public string schedule;
}
