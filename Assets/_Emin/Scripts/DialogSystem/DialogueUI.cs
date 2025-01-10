using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text[] questionText;
    [SerializeField] private Text answerText;
    private int _currentAnswerIndex;
    public static int _currentStage;

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

    private void QuestionSelected(List<string> answer, int answerIndex, int stageIndex)
    {
        if (stageIndex < 0) throw new ArgumentOutOfRangeException(nameof(stageIndex));
        _currentAnswerIndex = answerIndex;
        _currentStage++;
        stageIndex = _currentStage;
        Debug.Log("Stage Index: " + stageIndex);
        foreach (var t in questionText)
        {
            t.transform.parent.gameObject.SetActive(false);
        }

        answerText.DOText(" ", 0f).OnComplete(() =>
            answerText.DOText(answer[_currentAnswerIndex], 2).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    foreach (var t in questionText)
                    {
                        t.transform.parent.gameObject.SetActive(true);
                    }
                }));
    }

    private void ShowQuestions(List<string> questions, CharacterType characterType, int stageIndex)
    {
        for (int i = 0; i < questionText.Length; i++)
        {
            questionText[i].text = questions[i];
        }

        foreach (var t in questions)
        {
            Debug.Log(t);
        }
        
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(true);
        }

        Debug.Log("ShowQuestions");
    }

    private void ShowAnswers(List<string> answers, CharacterType characterType, int stageIndex)
    {
        answerText.transform.parent.gameObject.SetActive(true);
    }

    private void QuestionExit(List<string> answers, List<string> questions, int stageIndex)
    {
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(false);
        }

        answerText.transform.parent.gameObject.SetActive(false);
    }
}