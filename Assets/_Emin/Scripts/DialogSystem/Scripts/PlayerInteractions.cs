using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    public Canvas dialogueCanvas;
    private CharacterTypeController _npc;
    private CharacterTypeHandler _getCharacterType;
    private void Awake()
    {
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        _npc = other.GetComponent<CharacterTypeController>();
        if (_npc!=null && Input.GetKeyDown(KeyCode.E))
        {
            _getCharacterType = _npc.GetCharacterType();
            Debug.LogWarning(_getCharacterType);
            dialogueCanvas.gameObject.SetActive(true);
            _dialogueManager.QuestionStart(_getCharacterType,0);
            
        }
        else if (_npc == null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("NPC not found");
        }
    }
    
    public void OnClickQuestion(int answerIndex)
    {
        _dialogueManager.QuestionSelect(_getCharacterType,answerIndex);
    }
   
}
