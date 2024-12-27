using UnityEngine;

public class GamerInteraction : MonoBehaviour
{
	private IInteractable _interactableObject;
	private void OnTriggerEnter(Collider other)
	{
		_interactableObject = other.GetComponent<IInteractable>();
		_interactableObject?.StartInteraction();
	}
    
	private void OnTriggerStay(Collider other)
	{
		if(_interactableObject != null && Input.GetKey(KeyCode.P))
		{
			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		_interactableObject?.ExitInteraction();
		_interactableObject = null;
	}
}
