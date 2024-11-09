using UnityEngine;

public class NPCInteraction : MonoBehaviour,IInteractable
{
    private NPCDialogSystem _npcDialogSystem;
    
    private void Awake()
    {
        _npcDialogSystem = GetComponentInParent<NPCDialogSystem>();
    }
    public void StartInteraction()
    {
        _npcDialogSystem.SetDialogReferences();
    }

    public void ExitInteraction()
    {
        _npcDialogSystem.UnSetDialogReferences();
    }
}
