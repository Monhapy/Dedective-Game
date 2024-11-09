using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog" ,menuName = "Dialog")]
public class SO_Dialogs : ScriptableObject
{
    [TextArea] public string[] questions;
    [TextArea] public string answer;
    
}
