using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private List<Text> questionText;
    [SerializeField] private Text answerText;
    private int _currentStage;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        DialogueEventHandler.Instance.OnQuestionStart += QuestionStart;
        DialogueEventHandler.Instance.OnQuestionSelect -= QuestionSelect;
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
        answerText.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < questionText.Count; i++)
        {
            questionText[i].text = questions[i];
        }

        
    }
    private void QuestionSelect(List<string> answer)
    {
        Debug.LogWarning("Selected");
        string previousAnswer = answer[_currentStage];
        answerText.text = previousAnswer;
        answerText.transform.parent.gameObject.SetActive(true);
        _currentStage++;
    }
   
}
