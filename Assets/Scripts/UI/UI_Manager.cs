using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Image fadeInOutImage;
    [SerializeField] private float fadeDuration;
    
    [SerializeField] private GameObject questionsCanvas;
        
    [SerializeField] private TextMeshProUGUI[] questions;
    [SerializeField] private TextMeshProUGUI answer;
    
    [SerializeField] private float answerDisplayDuration;
        
    private bool isDisplayingFinalAnswer = false;
        private void OnEnable()
        {
            DialogEventManager.AddHandler(DialogEvent.OnInitializedDialog,FadeScreen);
            DialogEventManager.AddHandler(DialogEvent.OnExitDialog,FadeScreen);
            
            DialogEventManager.AddHandler(DialogEvent.OnStartQuestionStage,GetDialogStage);
            DialogEventManager.AddHandler(DialogEvent.OnStartQuestionStage,UnGetAnswerUI);
            DialogEventManager.AddHandler(DialogEvent.OnStartQuestionStage,GetQuestionsUI);
            
            DialogEventManager.AddHandler(DialogEvent.OnStartAnswerStage,DisplayAnswerWithDelay);
            DialogEventManager.AddHandler(DialogEvent.OnStartAnswerStage,UnGetQuestionsUI);
            
            DialogEventManager.AddHandler(DialogEvent.OnExitDialog,UnGetQuestionsUI);
        }
    
        private void OnDisable()
        {
            DialogEventManager.RemoveHandler(DialogEvent.OnInitializedDialog,FadeScreen);
            DialogEventManager.RemoveHandler(DialogEvent.OnExitDialog,FadeScreen);
            
            DialogEventManager.RemoveHandler(DialogEvent.OnStartQuestionStage,GetQuestionsUI);
            DialogEventManager.RemoveHandler(DialogEvent.OnStartQuestionStage,UnGetAnswerUI);
            DialogEventManager.RemoveHandler(DialogEvent.OnStartQuestionStage,GetDialogStage);
            
            DialogEventManager.RemoveHandler(DialogEvent.OnStartAnswerStage,DisplayAnswerWithDelay);
            DialogEventManager.RemoveHandler(DialogEvent.OnStartAnswerStage,UnGetQuestionsUI);
            
            DialogEventManager.RemoveHandler(DialogEvent.OnExitDialog,UnGetQuestionsUI);
            
            // as--s
            DOTween.Kill(fadeInOutImage);
        }
        private void FadeScreen()
        {
            if (fadeInOutImage == null) return; // fadeInOutImage null ise metodu bitir.
    
            fadeInOutImage.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                if (fadeInOutImage == null) return; // OnComplete tetiklendiğinde fadeInOutImage null olabilir.
        
                DialogEventManager.Broadcast(DialogEvent.OnSetScene);
                fadeInOutImage.DOFade(0f, fadeDuration);
            });
        }
    
        #region GetSet and UnGetSet Methods
    
        private void GetQuestionsUI()
        {
            questionsCanvas.SetActive(true);
        }
    
        private void UnGetQuestionsUI()
        {
            questionsCanvas.SetActive(false);
        }
    
        private void GetAnswerUI()
        {
            answer.enabled = true;
        }
        
        private void UnGetAnswerUI()
        {
            answer.enabled = false;
        }
    
        #endregion
        
    
        private void SetDialogStage(int newIndex)
        {
            questions[0].text = NPCDialogSystem.DialogStageList[newIndex].questions[0];
            questions[1].text = NPCDialogSystem.DialogStageList[newIndex].questions[1];
            questions[2].text = NPCDialogSystem.DialogStageList[newIndex].questions[2];
            
            answer.text = NPCDialogSystem.DialogStageList[newIndex].answer;
        }
    
        private void GetDialogStage()
        {
            SetDialogStage(NPCDialogSystem.CurrentQuestion);
        }
    
        public void QuestionButton()  // Button tarafından kontrol edilecek method
        {
            // Eğer coroutine zaten çalışıyorsa hiçbir işlem yapma.
            if (isDisplayingFinalAnswer) return;

            if (NPCDialogSystem.CurrentQuestion < NPCDialogSystem.DialogStageList.Count - 1)
            {
                DialogEventManager.Broadcast(DialogEvent.OnStartAnswerStage);
                NPCDialogSystem.CurrentQuestion++;
            }
            else
            {
                isDisplayingFinalAnswer = true; // Coroutine'in başladığını işaret et.
                StartCoroutine(DisplayFinalAnswerAndExit());
            }
            
        }
        
        private void DisplayAnswerWithDelay()
        {
            StartCoroutine(DisplayAnswerCoroutine());
        }
    
        private IEnumerator DisplayAnswerCoroutine()
        {
            GetAnswerUI();  
            yield return new WaitForSeconds(answerDisplayDuration);  
            DialogEventManager.Broadcast(DialogEvent.OnStartQuestionStage);  
        }
        
        
        private IEnumerator DisplayFinalAnswerAndExit()
        {
            GetAnswerUI();
            UnGetQuestionsUI();
            yield return new WaitForSeconds(answerDisplayDuration);
            UnGetAnswerUI();

            fadeInOutImage.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                DialogEventManager.Broadcast(DialogEvent.OnExitDialog);
                fadeInOutImage.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    isDisplayingFinalAnswer = false; // Coroutine tamamlandığında bayrağı sıfırla.
                });
            });
        }
}
