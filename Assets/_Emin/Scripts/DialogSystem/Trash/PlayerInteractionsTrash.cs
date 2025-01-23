using UnityEngine;

public class PlayerInteractionsTrash : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    private CharacterTypeTrash _characterTypeTrash;
    private CharacterTypeControllerTrash _npc;

    
    private void OnTriggerStay(Collider other)
    {
        _npc = other.GetComponent<CharacterTypeControllerTrash>();
        if (_npc != null && Input.GetKeyDown(KeyCode.E))
        {
            dialogueCanvas.gameObject.SetActive(true);
            _characterTypeTrash = _npc.GetCharacterType();
            DialogueManagerTrash.Instance.StartDialogue(_characterTypeTrash, DialogueUITrash.CurrentStage);
            Debug.Log("NPC found");
            Debug.Log(_npc.GetCharacterType());
        }
        else if(_npc == null && Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.LogWarning("NPC not found");
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (_npc!=null)
    //     {
    //         dialogueCanvas.gameObject.SetActive(false);
    //         DialogueManager.Instance.QuestionExit(_characterType, DialogueUI.CurrentStage);
    //     }
    //     
    // }

    public void OnClickQuestion(int answerIndex)
    {
        // DialogueManager.Instance.ProvideAnswer(_characterType,DialogueUI.CurrentStage);
        DialogueManagerTrash.Instance.QuestionSelected(_characterTypeTrash, answerIndex,DialogueUITrash.CurrentStage,DialogueUITrash._previousAnswer);
    }
}