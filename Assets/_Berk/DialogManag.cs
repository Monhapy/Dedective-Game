using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogManag : MonoBehaviour
{
    public GameObject dialogueUI;
    public Button[] questionButtons;
    public TextMeshProUGUI answerText;
    public PlayerMovement playerMovement;
    public NoteSystem noteSystem;
    
    public string[][] questions = new string[][]
    {
        new string[] { "Nasılsın?", "Ne yapıyorsun?", "Burada ne var?" },
        new string[] { "Burası güvenli mi?", "Bana yardım eder misin?", "Burada kimler yaşıyor?" },
        new string[] { "Görevin nedir?", "Burada ne yapıyorsun?", "Güçlü müsün?" }
    };
    public string[][] answers = new string[][]
    {
        new string[] { "İyiyim, teşekkürler!", "Bekçilik yapıyorum.", "Burası eski bir köy." },
        new string[] { "Evet, oldukça güvenli.", "Tabii ki, neye ihtiyacın var?", "Burada sadece gezginler var." },
        new string[] { "Ben bir muhafızım.", "Görevim burayı korumak.", "Evet, güçlü olduğumu düşünüyorum." }
    };

    private int currentSet = 0;
    private bool interactionFinished = false;

    void Start()
    {
        dialogueUI.SetActive(false);
        LockCursor(); // Oyun başlarken cursor kaybolsun
    }

    public void StartDialogue()
    {
        if (interactionFinished) return;

        dialogueUI.SetActive(true);
        ShowQuestions();
        UnlockCursor(); // Diyalog başlayınca mouse gelsin
    }

    void ShowQuestions()
    {
        if (currentSet >= questions.Length)
        {
            EndDialogue();
            return;
        }

        answerText.text = "";

        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].gameObject.SetActive(true);
            questionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentSet][i];
            int index = i;
            questionButtons[i].onClick.RemoveAllListeners();
            questionButtons[i].onClick.AddListener(() => ShowAnswer(index));
        }
    }

    void ShowAnswer(int questionIndex)
    {
        answerText.text = answers[currentSet][questionIndex];

        foreach (var btn in questionButtons)
        {
            btn.gameObject.SetActive(false);
        }

        StartCoroutine(NextQuestionSet());
    }

    IEnumerator NextQuestionSet()
    {
        yield return new WaitForSeconds(2);

        currentSet++;

        if (currentSet < questions.Length)
        {
            ShowQuestions();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        interactionFinished = true;
        LockCursor(); // Diyalog bitince cursor kaybolsun
        noteSystem.isTalked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // İmleci serbest bırak
        Cursor.visible = true; // İmleci görünür yap
        playerMovement.movementSpeed = 0;
        playerMovement.mouseSensitivity = 0;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // İmleci ekrana kilitle
        Cursor.visible = false; // İmleci gizle
        playerMovement.movementSpeed = 5;
        playerMovement.mouseSensitivity = 3;
    }
    
}
