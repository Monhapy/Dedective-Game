using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public List<SO_Dialogue> dialogueStage;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public List<Stage> dialogues;
    private Dictionary<CharacterType, List<SO_Dialogue>> _dialogueDictionary;

    private void Awake()
    {
        Instance = this;
        _dialogueDictionary = new Dictionary<CharacterType, List<SO_Dialogue>>();
        foreach (var dialogue in dialogues)
        {
            foreach (var soDialogue in dialogue.dialogueStage)
            {
                if (!_dialogueDictionary.ContainsKey(soDialogue.characterType))
                {
                    _dialogueDictionary.Add(soDialogue.characterType, new List<SO_Dialogue>());
                }
            }
        }
    }

    public void QuestionSelected(CharacterType characterType, int answerIndex, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.QuestionSelected(new List<string>(dialogue[stageIndex].answers), answerIndex,
                stageIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }

    public void StartDialogue(CharacterType characterType, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.QuestionAsked(new List<string>(dialogue[stageIndex].questions), characterType,
                stageIndex);
            Debug.Log(stageIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }

    public void ProvideAnswer(CharacterType characterType, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.AnswerGiven(new List<string>(dialogue[stageIndex].answers), characterType,
                stageIndex);
            Debug.Log(stageIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }

    public void QuestionExit(CharacterType characterType, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            DialogueEvents.Instance.QuestionExit(new List<string>(dialogue[stageIndex].answers),
                new List<string>(dialogue[stageIndex].questions), stageIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }
}