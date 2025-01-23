using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    public static DialogueEventHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<List<string>> OnQuestionStart;
    public event Action<List<string>,List<string>,int> OnQuestionSelect;
    
    public void QuestionStart(List<string> question)
    {
        OnQuestionStart?.Invoke(question);
    }

    public void QuestionSelect(List<string> answer,List<string> question, int answerIndex)
    {
        OnQuestionSelect?.Invoke(answer,question,answerIndex);
    }
}
