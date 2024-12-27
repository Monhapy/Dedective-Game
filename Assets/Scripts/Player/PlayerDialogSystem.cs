using UnityEngine;

public class PlayerDialogSystem : MonoBehaviour
{
    public void PlayerDialogStarted()
    {
        Cursor.lockState = CursorLockMode.None;
        DialogEventManager.Broadcast(DialogEvent.OnInitializedDialog);
    }
}
