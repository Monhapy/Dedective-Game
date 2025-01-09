using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        CharacterTypeController npc = other.GetComponent<CharacterTypeController>();
        if (npc != null)
        {
            CharacterType characterType = npc.GetCharacterType();
            dialogueManager.StartDialogue(characterType);
            dialogueManager.ProvideAnswer(characterType);
            Debug.Log("NPC found");
        }
        else
        {
            Debug.LogWarning("NPC not found");
        }
    }
}