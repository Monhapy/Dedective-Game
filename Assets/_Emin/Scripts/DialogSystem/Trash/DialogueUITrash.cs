using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DialogueUITrash : MonoBehaviour
{
    [SerializeField] private Text[] questionText;
    [SerializeField] private Text answerText;
    private int _currentAnswerIndex;
    public static int CurrentStage;
    public static string _previousAnswer;
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

        DialogueEventsTrash.Instance.OnQuestionExit += QuestionExit;
        DialogueEventsTrash.Instance.OnQuestionSelected += QuestionSelected;
        DialogueEventsTrash.Instance.OnQuestionAsked += ShowQuestions;
        // DialogueEvents.Instance.OnAnswerGiven += ShowAnswers;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        DialogueEventsTrash.Instance.OnQuestionExit -= QuestionExit;
        DialogueEventsTrash.Instance.OnQuestionSelected -= QuestionSelected;
        DialogueEventsTrash.Instance.OnQuestionAsked -= ShowQuestions;
        // DialogueEvents.Instance.OnAnswerGiven -= ShowAnswers;
    }

    private void QuestionSelected(List<string> answer, int answerIndex, List<string> questions, string previousAnswer)
    {
        _currentAnswerIndex = answerIndex;
        _previousAnswer = previousAnswer;
        _previousAnswer = answer[_currentAnswerIndex]; // Önceki cevabı sakla

        if (!string.IsNullOrEmpty(previousAnswer))
        {
            answerText.transform.parent.gameObject.SetActive(true);
            answerText.text = previousAnswer;
        }
        else
        {
            answerText.transform.parent.gameObject.SetActive(false);
        }
        // Bir sonraki soruya geç
        CurrentStage++;
        if (CurrentStage >= questions.Count)
        {
            CurrentStage = 0; // Eğer son soruya gelindiyse başa dön
        }
        for (int i = 0; i < questionText.Length && i <= questions.Count; i++)
        {
            questionText[i].text = questions[i];
        }
        
    }

    private void ShowQuestions(List<string> questions, CharacterTypeTrash characterTypeTrash)
    {
        CurrentStage = 0;
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

    // private void ShowAnswers(List<string> answers, CharacterType characterType)
    // {
    //     
    // }

    private void QuestionExit(List<string> answers, List<string> questions)
    {
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(false);
        }

        answerText.transform.parent.gameObject.SetActive(false);
    }
}