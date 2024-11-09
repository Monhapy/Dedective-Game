using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable _interactableObject;
    private PlayerDialogSystem _playerDialogSystem;
    
    private void Awake()
    {
        _playerDialogSystem = GetComponent<PlayerDialogSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _interactableObject = other.GetComponent<IInteractable>();
        _interactableObject?.StartInteraction();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(_interactableObject != null && Input.GetKey(KeyCode.P))
        {
            _playerDialogSystem.PlayerDialogStarted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _interactableObject?.ExitInteraction();
        _interactableObject = null;
    }
}