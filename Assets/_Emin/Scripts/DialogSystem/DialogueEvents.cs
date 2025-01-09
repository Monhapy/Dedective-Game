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
    
    public event Action<List<string>, CharacterType> OnQuestionAsked;
    public event Action<List<string>, CharacterType> OnAnswerGiven; 
    
    public void QuestionAsked(List<string> questions, CharacterType characterType)
    {
        OnQuestionAsked?.Invoke(questions, characterType);
    }
    
    public void AnswerGiven(List<string> answers, CharacterType characterType)
    {
        OnAnswerGiven?.Invoke(answers, characterType);
    }
}
