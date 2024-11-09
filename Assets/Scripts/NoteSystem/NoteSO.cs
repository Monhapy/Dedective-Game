using UnityEngine;

[CreateAssetMenu(fileName = "NoteSO", menuName = "Create NoteSO")]
public class NoteSO : ScriptableObject
{
    [TextArea] public string[] notes;
}
    