using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public List<SO_Dialogue> Dialogues;
}

public class DialogueManager : MonoBehaviour
{
    private DialogueEventHandler _dialogueEventHandler;
    public List<Stage> dialogueStage;
    private Dictionary<CharacterTypeHandler, List<Stage>> _dialogueDictionary;

    private void Awake()
    {
        _dialogueDictionary = new Dictionary<CharacterTypeHandler, List<Stage>>();
        for (int i = 0; i < dialogueStage.Count; i++)
        {
            Stage stage = dialogueStage[i];
            for (int j = 0; j < stage.Dialogues.Count; j++)
            {
                SO_Dialogue soDialogue = stage.Dialogues[j];
                if (!_dialogueDictionary.ContainsKey(soDialogue.characterTypeHandler))
                {
                    _dialogueDictionary.Add(soDialogue.characterTypeHandler, new List<Stage>());
                }


                _dialogueDictionary[soDialogue.characterTypeHandler].Add(stage);
            }
        }

        // foreach (var entry in _dialogueDictionary)
        // {
        //     Debug.Log("CharacterTypeHandler: " + entry.Key + ", Stages Count: " + entry.Value.Count);
        // }
    }


    public void QuestionStart(CharacterTypeHandler characterTypeHandler, int dialogueIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeHandler, out var dialogueStages))
        {
            int stageIndex = (int)characterTypeHandler;
            var dialogue = dialogueStages[stageIndex].Dialogues[dialogueIndex];
            DialogueEventHandler.Instance.QuestionStart(dialogue.questions);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeHandler);
        }
    }

    public void QuestionSelect(CharacterTypeHandler characterTypeHandler, int answerIndex)
    {
        if (_dialogueDictionary.TryGetValue(characterTypeHandler, out var dialogueStages))
        {
            int stageIndex = (int)characterTypeHandler;
            var dialogue = dialogueStages[stageIndex].Dialogues[DialogueUI._currentStage];
            DialogueEventHandler.Instance.QuestionSelect(dialogue.answers, dialogue.questions, answerIndex);
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterTypeHandler);
        }
    }
}