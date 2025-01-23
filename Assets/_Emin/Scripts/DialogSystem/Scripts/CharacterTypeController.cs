using UnityEngine;

public class CharacterTypeController : MonoBehaviour
{
    [SerializeField] private CharacterTypeHandler characterType;

    public CharacterTypeHandler GetCharacterType()
    {
        return characterType;
    }
}
