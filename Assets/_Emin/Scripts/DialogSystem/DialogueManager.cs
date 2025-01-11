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

                _dialogueDictionary[soDialogue.characterType].Add(soDialogue);
            }
        }
    }

    public void QuestionSelected(CharacterType characterType, int answerIndex, int stageIndex)
    {
        int previousStageIndex = stageIndex - 1;
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            if (previousStageIndex>=0 && stageIndex<dialogue.Count)
            {
                DialogueEvents.Instance.QuestionSelected(new List<string>(dialogue[previousStageIndex].answers),
                    answerIndex,
                    new List<string>(dialogue[stageIndex].questions));
                Debug.Log("Previous Stage: " + previousStageIndex);
                Debug.Log("Stage Index " + stageIndex);  
            }
                
            
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }

    public void StartDialogue(CharacterType characterType, int stageIndex)
    {
        stageIndex = (int)characterType;
        if (_dialogueDictionary.TryGetValue(characterType, out var dialogue))
        {
            if (stageIndex<dialogue.Count)
            {
                DialogueEvents.Instance.QuestionAsked(new List<string>(dialogue[stageIndex].questions),
                    characterType);
            }
           
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
            if (stageIndex < dialogue.Count)
            {
                DialogueEvents.Instance.AnswerGiven(new List<string>(dialogue[stageIndex].answers), characterType);
            }
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
            if (stageIndex<dialogue.Count)
            {
                DialogueEvents.Instance.QuestionExit(new List<string>(dialogue[stageIndex].answers),
                    new List<string>(dialogue[stageIndex].questions));
            }
            
        }
        else
        {
            Debug.LogError("Dialogue not found for " + characterType);
        }
    }
}