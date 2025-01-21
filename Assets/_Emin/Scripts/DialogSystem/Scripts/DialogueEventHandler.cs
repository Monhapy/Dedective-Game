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
    public event Action<List<string>> OnQuestionSelect;
    
    public void QuestionStart(List<string> question)
    {
        OnQuestionStart?.Invoke(question);
    }

    public void QuestionSelect(List<string> answer)
    {
        OnQuestionSelect?.Invoke(answer);
    }
}
