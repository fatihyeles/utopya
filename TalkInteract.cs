using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
   // [SerializeField] DialogueContainer dialogue;
    NPCCharacter npcCharacter;
    NPCDefinition npcDefinition;
    private void Awake()
    {
        npcCharacter = GetComponent<NPCCharacter>();
        npcDefinition = npcCharacter.character;
    }
    public override void Interact(Character character)
        {
        DialogueContainer dialogueContainer = npcDefinition.genaralDialogues[Random.Range(0, npcDefinition.genaralDialogues.Count)];
        npcCharacter.IncreaseRelationship(0.1f);
        GameManeger.instance.dialogueSystem.Initialize(dialogueContainer);

        }
}

