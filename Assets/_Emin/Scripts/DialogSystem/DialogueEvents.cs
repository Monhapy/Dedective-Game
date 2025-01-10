using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    public static DialogueEvents Instance;

    private void Awake()
    {
        Instance = this;
    }
   
    public event Action<List<string>, int, int > OnQuestionSelected;
    public event Action<List<string>, CharacterType, int> OnQuestionAsked;
    public event Action<List<string>, CharacterType, int> OnAnswerGiven;

    public event Action<List<string>, List<string>, int> OnQuestionExit; 

    public void QuestionSelected(List<string> answer, int answerIndex, int stageIndex)
    {
        OnQuestionSelected?.Invoke(answer,answerIndex, stageIndex);
    }
    public void QuestionAsked(List<string> questions, CharacterType characterType, int stageIndex)
    {
        OnQuestionAsked?.Invoke(questions, characterType,stageIndex);
    }
    
    public void AnswerGiven(List<string> answers, CharacterType characterType, int stageIndex)
    {
        OnAnswerGiven?.Invoke(answers, characterType, stageIndex);
    }

    public void QuestionExit(List<string> answers, List<string> questions, int stageIndex)
    {
        OnQuestionExit?.Invoke(answers, questions,stageIndex);
    }

   
}
