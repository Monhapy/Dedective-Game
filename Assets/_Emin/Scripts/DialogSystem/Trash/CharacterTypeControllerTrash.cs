using UnityEngine;
using UnityEngine.Serialization;

public class CharacterTypeControllerTrash : MonoBehaviour
{
    [FormerlySerializedAs("characterType")] [SerializeField] private CharacterTypeTrash characterTypeTrash;
    
    public CharacterTypeTrash GetCharacterType()
    {
        return characterTypeTrash;
    }
}