using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private List<Text> questionText;
    [SerializeField] private Text answerText;
    public static int _currentStage;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        DialogueEventHandler.Instance.OnQuestionStart += QuestionStart;
        DialogueEventHandler.Instance.OnQuestionSelect += QuestionSelect;
    }


    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        DialogueEventHandler.Instance.OnQuestionStart -= QuestionStart;
        DialogueEventHandler.Instance.OnQuestionSelect -= QuestionSelect;
    }

    private void QuestionStart(List<string> questions)
    {
        _currentStage = 0;
        // answerText.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < questionText.Count; i++)
        {
            questionText[i].text = questions[i];
        }
    }

    private void QuestionSelect(List<string> answer, List<string> questions, int answerIndex)
    {
        foreach (var question in questionText)
        {
            question.transform.parent.gameObject.SetActive(false);
        }
        string previousAnswer = answer[_currentStage];
        answerText.text = previousAnswer;
        answerText.DOFade(0, 0).OnComplete(() =>
        {
            answerText.DOFade(1, 3).OnComplete(() =>
            {
                foreach (var question in questionText)
                {
                    question.transform.parent.gameObject.SetActive(true);
                }
            });
        });
        answerText.transform.parent.gameObject.SetActive(true);
        _currentStage++;
        for (int i = 0; i < questionText.Count; i++)
        {
            questionText[i].text = questions[i];
        }
        Debug.LogWarning("Current Stage: " + _currentStage);
    }
}