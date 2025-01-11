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
   
    public event Action<List<string>, int, List<string>> OnQuestionSelected;
    public event Action<List<string>, CharacterType> OnQuestionAsked;
    public event Action<List<string>, CharacterType> OnAnswerGiven;

    public event Action<List<string>, List<string>> OnQuestionExit; 

    public void QuestionSelected(List<string> answer, int answerIndex, List<string> question)
    {
        OnQuestionSelected?.Invoke(answer,answerIndex, question);
    }
    public void QuestionAsked(List<string> questions, CharacterType characterType)
    {
        OnQuestionAsked?.Invoke(questions, characterType);
    }
    
    public void AnswerGiven(List<string> answers, CharacterType characterType)
    {
        OnAnswerGiven?.Invoke(answers, characterType);
    }

    public void QuestionExit(List<string> answers, List<string> questions)
    {
        OnQuestionExit?.Invoke(answers, questions);
    }

   
}
