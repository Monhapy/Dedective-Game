using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text[] questionText;
    [SerializeField] private Text answerText;
    private int _currentAnswerIndex;
    public static int CurrentStage;

    private void Awake()
    {
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(false);
        }
        
        answerText.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;

        DialogueEvents.Instance.OnQuestionExit += QuestionExit;
        DialogueEvents.Instance.OnQuestionSelected += QuestionSelected;
        DialogueEvents.Instance.OnQuestionAsked += ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven += ShowAnswers;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        DialogueEvents.Instance.OnQuestionExit -= QuestionExit;
        DialogueEvents.Instance.OnQuestionSelected -= QuestionSelected;
        DialogueEvents.Instance.OnQuestionAsked -= ShowQuestions;
        DialogueEvents.Instance.OnAnswerGiven -= ShowAnswers;
    }

    private void QuestionSelected(List<string> answer, int answerIndex, List<string> questions)
    {
        _currentAnswerIndex = answerIndex;
        foreach (var t in questionText)
        {
            t.transform.parent.gameObject.SetActive(false);
        }
        
        answerText.DOText(" ", 0f).OnComplete(() =>
            answerText.DOText(answer[_currentAnswerIndex], 2).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    for (int i = 0; i < questionText.Length && i < questions.Count; i++)
                    {
                        questionText[i].text = questions[i];
                    }

                    if (CurrentStage<=questions.Count)
                    {
                        foreach (var t in questionText)
                        {
                      
                            t.transform.parent.gameObject.SetActive(true);
                       
                        }   
                    }
                    
                }));
    }

    private void ShowQuestions(List<string> questions, CharacterType characterType)
    {
        CurrentStage = 1;
        for (int i = 0; i < questionText.Length && i <= questions.Count; i++)
        {
            questionText[i].text = questions[i];
        }
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(true);
        }
        
        Debug.Log("ShowQuestions");
    }

    private void ShowAnswers(List<string> answers, CharacterType characterType)
    {
        answerText.transform.parent.gameObject.SetActive(true);
    }

    private void QuestionExit(List<string> answers, List<string> questions)
    {
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(false);
        }

        answerText.transform.parent.gameObject.SetActive(false);
    }
}