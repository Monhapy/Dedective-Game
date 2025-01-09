using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    public List<SO_Dialogue> dialogues;
    private Dictionary<CharacterType, SO_Dialogue> _dialogueDictionary;
    
    private void Awake()
    {
        Instance = this;
        _dialogueDictionary = new Dictionary<CharacterType, SO_Dialogue>();
        foreach (var dialogue in dialogues)
        {
            _dialogueDictionary.Add(dialogue.characterType, dialogue);
        }
    }
    
    public void QuestionSelected(CharacterType characterType, int answerIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.QuestionSelected(new List<string>(dialogue.answers), answerIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
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
