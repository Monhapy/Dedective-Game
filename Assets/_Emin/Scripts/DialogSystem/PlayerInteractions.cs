using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    private CharacterType _characterType;
    private CharacterTypeController _npc;

    
    private void OnTriggerStay(Collider other)
    {
        _npc = other.GetComponent<CharacterTypeController>();
        if (_npc != null && Input.GetKeyDown(KeyCode.E))
        {
            dialogueCanvas.gameObject.SetActive(true);
            _characterType = _npc.GetCharacterType();
            DialogueManager.Instance.StartDialogue(_characterType, DialogueUI._currentStage);
            Debug.Log("NPC found");
            Debug.Log(_npc.GetCharacterType());
        }
        else if(_npc == null && Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.LogWarning("NPC not found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_npc!=null)
        {
            dialogueCanvas.gameObject.SetActive(false);
            DialogueManager.Instance.QuestionExit(_characterType, DialogueUI._currentStage);
        }
        
    }

    public void OnClickQuestion(int answerIndex)
    {
        DialogueManager.Instance.ProvideAnswer(_characterType,DialogueUI._currentStage);
        DialogueManager.Instance.QuestionSelected(_characterType, answerIndex,DialogueUI._currentStage);
    }
}