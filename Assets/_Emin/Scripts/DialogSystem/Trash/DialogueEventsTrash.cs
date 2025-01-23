using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventsTrash : MonoBehaviour
{
    public static DialogueEventsTrash Instance;

    private void Awake()
    {
        Instance = this;
    }
   
    public event Action<List<string>, int, List<string>, string> OnQuestionSelected;
    public event Action<List<string>, CharacterTypeTrash> OnQuestionAsked;
    public event Action<List<string>, CharacterTypeTrash> OnAnswerGiven;

    public event Action<List<string>, List<string>> OnQuestionExit; 

    public void QuestionSelected(List<string> answer, int answerIndex, List<string> question, string previousAnswer)
    {
        OnQuestionSelected?.Invoke(answer,answerIndex, question, previousAnswer);
    }
    public void QuestionAsked(List<string> questions, CharacterTypeTrash characterTypeTrash)
    {
        OnQuestionAsked?.Invoke(questions, characterTypeTrash);
    }
    
    public void AnswerGiven(List<string> answers, CharacterTypeTrash characterTypeTrash)
    {
        OnAnswerGiven?.Invoke(answers, characterTypeTrash);
    }

    public void QuestionExit(List<string> answers, List<string> questions)
    {
        OnQuestionExit?.Invoke(answers, questions);
    }

   
}
