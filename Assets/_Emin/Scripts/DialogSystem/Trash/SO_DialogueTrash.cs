using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class SO_DialogueTrash : ScriptableObject
{
    [TextArea] public string[] questions;
    [TextArea] public string[] answers;
    [FormerlySerializedAs("characterType")] public CharacterTypeTrash characterTypeTrash;
}