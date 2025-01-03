using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteSystem : MonoBehaviour
{
    //Konusma bittigini kontrol etmek icin eklenen bool  
    public bool isTalked;
    public NoteSO noteSo;

    [Header("----Current Note & Note Texts")] [SerializeField]
    private TextMeshProUGUI noteText;

    [SerializeField] private int currentNote;
    [SerializeField] private TextMeshProUGUI currentNoteText;

    [Header("----Note Objects")] [SerializeField]
    private GameObject notePanel;

    [SerializeField] private float duration = 2f;
    [SerializeField] private GameObject notePopup;
    [SerializeField] private GameObject noteAnimation;
    [SerializeField] private Image noteSprite;
    [SerializeField] private List<Sprite> noteSprites = new List<Sprite>();
    private Animator _noteAnimator;
    private float _noteAnimLength;
    [Header("----Keys")] [SerializeField] private KeyCode noteActive = KeyCode.Y;
    [SerializeField] private KeyCode switchNoteLeft = KeyCode.Q;
    [SerializeField] private KeyCode switchNoteRight = KeyCode.E;

    private GameObject notePanelInstance;
    private List<GameObject> _noteObjects = new List<GameObject>();
    private bool isNoteActive;
    private PlayerMovement playerMovement;

    [Obsolete("Obsolete")]
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        _noteAnimator = noteAnimation.GetComponent<Animator>();
    }

    private void Update()
    {
        SetNote();
        SwitchNote();
        NoteActive();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SetNote()
    {
        if (isTalked && _noteObjects.Count < noteSo.notes.Length)
        {
            MapSystem.Instance.GetTalkingNPC(currentNote);
            currentNote = _noteObjects.Count;
            noteText.text = noteSo.notes[_noteObjects.Count];
            currentNoteText.text = (currentNote + 1).ToString();
            NewNote();
            isTalked = false;
        }
    }

    private void NoteActive()
    {
        if (Input.GetKeyDown(noteActive) && _noteObjects.Count > 0)
        {
            isNoteActive = !isNoteActive;
            _noteObjects[currentNote].SetActive(isNoteActive);
            noteAnimation.SetActive(isNoteActive);
            noteSprite.gameObject.SetActive(isNoteActive);
            NoteActiveFreeze();
        }
    }

    private void NoteActiveFreeze()
    {
        playerMovement.enabled = !isNoteActive;
        // Time.timeScale = isNoteActive ? 0 : 1;
    }

    private void NewNote()
    {
        noteSprite.gameObject.SetActive(false);
        notePanelInstance = Instantiate(notePanel, notePanel.transform.parent);
        _noteObjects.Add(notePanelInstance);
        foreach (var noteObject in _noteObjects)
        {
            noteObject.SetActive(false);
        }
        isNoteActive = false;
        NoteActiveFreeze();
        StartCoroutine(NotePopupController());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SwitchNote()
    {
        Debug.Log(isNoteActive);
        Debug.Log(_noteAnimLength);
        if (Input.GetKeyDown(switchNoteLeft) && _noteObjects.Count > 1 && currentNote > 0
            && isNoteActive && !_noteAnimator.GetCurrentAnimatorStateInfo(0).IsName("NoteAnimation"))
        {
            currentNote--;
            StartCoroutine(SwitchNoteArray());
        }

        if (Input.GetKeyDown(switchNoteRight) && currentNote < _noteObjects.Count - 1 && 
            isNoteActive && !_noteAnimator.GetCurrentAnimatorStateInfo(0).IsName("NoteAnimation"))
        {
            currentNote++;
            StartCoroutine(SwitchNoteArray());
        }

        Debug.Log("Current Note: " + currentNote);
        Debug.Log("Note Object Count: " + _noteObjects.Count);
    }

    private IEnumerator SwitchNoteArray()
    {
        noteAnimation.SetActive(true);
        noteSprite.gameObject.SetActive(false);
        _noteAnimLength = _noteAnimator.runtimeAnimatorController.animationClips[0].length;
        _noteAnimator.SetTrigger("isSwitch");
        foreach (var noteObject in _noteObjects)
        {
            noteObject.SetActive(false);
        }
        yield return new WaitForSecondsRealtime(_noteAnimLength);
        _noteObjects[currentNote].SetActive(true);
        noteSprite.sprite = noteSprites[currentNote];
        noteAnimation.SetActive(false);
        noteSprite.gameObject.SetActive(true);
    }


    private IEnumerator NotePopupController()
    {
        notePopup.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        notePopup.gameObject.SetActive(false);
    }
}