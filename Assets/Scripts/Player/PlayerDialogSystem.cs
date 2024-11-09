using UnityEngine;

public class PlayerDialogSystem : MonoBehaviour
{
    public void PlayerDialogStarted()
    {
        DialogEventManager.Broadcast(DialogEvent.OnInitializedDialog);
    }
}
