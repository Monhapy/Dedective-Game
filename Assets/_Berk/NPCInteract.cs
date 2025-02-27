using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	public DialogManag dialogueManager;
	public bool isPlayerNear = false;

	void Update()
	{
		if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
		{
			dialogueManager.StartDialogue();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerNear = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerNear = false;
		}
	}
}
