using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class SO_Dialogue : ScriptableObject
{
    [TextArea] public string[] questions;
    [TextArea] public string[] answers;
    public CharacterType characterType;
}