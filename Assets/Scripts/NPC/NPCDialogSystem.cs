using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class NPCDialogSystem : MonoBehaviour
{
    public static List<SO_Dialogs> DialogStageList;
    public static int CurrentQuestion;
    
    [SerializeField] private SO_Dialogs[] dialogStages;
    
    [SerializeField] private Camera npcCam;
    [SerializeField] private Transform playerRefTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private TextMeshProUGUI interactionInformation;

    private void OnEnable()
    {
        DialogEventManager.AddHandler(DialogEvent.OnExitDialog,ExitDialog);
    }

    private void OnDisable()
    {
        DialogEventManager.RemoveHandler(DialogEvent.OnExitDialog,ExitDialog);
    }

    private void Awake()
    {
        DialogStageList = new List<SO_Dialogs>();
    }

    private void Start()
    {
        DialogStageList.Add(dialogStages[0]);
        DialogStageList.Add(dialogStages[1]);
        DialogStageList.Add(dialogStages[2]);
    }
    
    public void SetDialogReferences()
    {
        DialogEventManager.AddHandler(DialogEvent.OnSetScene,SetSceneReferences);
        interactionInformation.enabled = true;
    }

    public void UnSetDialogReferences()
    {
        DialogEventManager.RemoveHandler(DialogEvent.OnSetScene,SetSceneReferences);
        interactionInformation.enabled = false;
    }

    private void SetSceneReferences()
    {
        npcCam.enabled = true;
        targetTransform.position = playerRefTransform.position;
        interactionInformation.enabled = false;
        
        DialogEventManager.Broadcast(DialogEvent.OnStartQuestionStage);
    }

    private void ExitDialog()
    {
        npcCam.enabled = false;
    }


}
