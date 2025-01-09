using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<SO_Dialogue> dialogues;

    private Dictionary<CharacterType, SO_Dialogue> _dialogueDictionary;
    
    private void Awake()
    {
        _dialogueDictionary = new Dictionary<CharacterType, SO_Dialogue>();
        foreach (var dialogue in dialogues)
        {
            _dialogueDictionary.Add(dialogue.characterType, dialogue);
        }
    }
    
    public void StartDialogue(CharacterType characterType)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.QuestionAsked(new List<string>(dialogue.questions), characterType);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }
    
    public void ProvideAnswer(CharacterType characterType)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.AnswerGiven(new List<string>(dialogue.answers), characterType);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }
}
