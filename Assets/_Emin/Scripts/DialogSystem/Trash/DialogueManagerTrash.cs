using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageTrash
{
    public List<SO_DialogueTrash> dialogueStage;
}

public class DialogueManagerTrash : MonoBehaviour
{
    public static DialogueManagerTrash Instance;
    public List<StageTrash> dialogues;
    private Dictionary<CharacterTypeTrash, List<StageTrash>> _dialogueDictionary;

    private void Awake()
    {
        Instance = this;
        _dialogueDictionary = new Dictionary<CharacterTypeTrash, List<StageTrash>>();
        foreach (var dialogue in dialogues)
        {
            foreach (var soDialogue in dialogue.dialogueStage)
            {
                if (!_dialogueDictionary.ContainsKey(soDialogue.characterTypeTrash))
                {
                    _dialogueDictionary.Add(soDialogue.characterTypeTrash, new List<StageTrash>());
                }

                _dialogueDictionary[soDialogue.characterTypeTrash].Add(dialogue);
            }
        }
    }

    public void QuestionSelected(CharacterTypeTrash characterTypeTrash, int answerIndex, int stageIndex, string previousAnswer)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeTrash, out var dialogue))
        {
            DialogueEventsTrash.Instance.QuestionSelected(
                new List<string>(dialogue[stageIndex].dialogueStage[stageIndex].answers),
                answerIndex,
                new List<string>(dialogue[stageIndex].dialogueStage[stageIndex].questions),previousAnswer);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeTrash);
        }
    }

    public void StartDialogue(CharacterTypeTrash characterTypeTrash, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeTrash, out var dialogue))
        {
            DialogueEventsTrash.Instance.QuestionAsked(
                new List<string>(dialogue[stageIndex].dialogueStage[stageIndex].questions),
                characterTypeTrash);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeTrash);
        }
    }

    public void ProvideAnswer(CharacterTypeTrash characterTypeTrash, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeTrash, out var dialogue))
        {
            DialogueEventsTrash.Instance.AnswerGiven(
                new List<string>(dialogue[stageIndex].dialogueStage[stageIndex - 1].answers),
                characterTypeTrash);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeTrash);
        }
    }

    public void QuestionExit(CharacterTypeTrash characterTypeTrash, int stageIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeTrash, out var dialogue))
        {
            if (stageIndex < dialogue.Count)
            {
                DialogueEventsTrash.Instance.QuestionExit(
                    new List<string>(dialogue[stageIndex].dialogueStage[stageIndex - 1].answers),
                    new List<string>(dialogue[stageIndex].dialogueStage[stageIndex].questions));
            }
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeTrash);
        }
    }
}