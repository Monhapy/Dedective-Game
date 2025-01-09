using UnityEngine;

public class CharacterTypeController : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;
    
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
}