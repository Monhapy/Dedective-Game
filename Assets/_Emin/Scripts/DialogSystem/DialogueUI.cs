using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text[] questionText;
    [SerializeField] private Text[] answerButtons;
    
    
    private void OnEnable()
    {
        DialogueEvents.Instance.OnQuestionAsked += ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven += ShowAnswers;
    }
    
    private void OnDisable()
    {
        DialogueEvents.Instance.OnQuestionAsked -= ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven -= ShowAnswers;
    }
    
    
    public void ShowQuestions (List<string> questions, CharacterType characterType)
    {
        for (int i = 0; i < questionText.Length; i++)
        {
            questionText[i].text = questions[i];
        }
    }

    public void ShowAnswers(List<string> answers, CharacterType characterType)
    {
        foreach (var answer in answers)
        {
            Debug.Log("Answers: " + answer);
        }
    }
   
}
