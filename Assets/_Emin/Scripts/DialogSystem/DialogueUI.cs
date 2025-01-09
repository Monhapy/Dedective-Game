using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text[] questionText;
    [SerializeField] private Text[] answerText;
    private int _currentAnswerIndex;
    
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        
        DialogueEvents.Instance.OnQuestionSelected += QuestionSelected;
        DialogueEvents.Instance.OnQuestionAsked += ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven += ShowAnswers;
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        DialogueEvents.Instance.OnQuestionSelected -= QuestionSelected;
        DialogueEvents.Instance.OnQuestionAsked -= ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven -= ShowAnswers;
    }
    private void QuestionSelected(List<string> answer,int answerIndex)
    {
        _currentAnswerIndex = answerIndex;
        foreach (var t in questionText)
        {
            t.transform.parent.gameObject.SetActive(false);
        }
        answerText[_currentAnswerIndex].text = answer[_currentAnswerIndex];
    }

    private void ShowQuestions (List<string> questions, CharacterType characterType)
    {
        for (int i = 0; i < questionText.Length; i++)
        {
            questionText[i].text = questions[i];
        }
    }

    private void ShowAnswers(List<string> answers, CharacterType characterType)
    {
        foreach (var t in answerText)
        {
            t.transform.parent.gameObject.SetActive(false);
        }
        answerText[_currentAnswerIndex].transform.parent.gameObject.SetActive(true);
    }
   
}
