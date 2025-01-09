using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterTypeController npc = other.GetComponent<CharacterTypeController>();
        if (npc != null)
        {
            CharacterType characterType = npc.GetCharacterType();
            DialogueManager.Instance.StartDialogue(characterType);
            DialogueManager.Instance.ProvideAnswer(characterType);
            Debug.Log("NPC found");
        }
        else
        {
            Debug.LogWarning("NPC not found");
        }
    }
}