using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/New Dialogue")]
public class SO_Dialogue : ScriptableObject
{
    [TextArea] public List<string> questions;
    [TextArea] public List<string> answers;
    public CharacterTypeHandler characterTypeHandler;
}
