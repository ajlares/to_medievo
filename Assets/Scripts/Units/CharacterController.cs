using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharactersBase currentSelectedCharacter;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharactersBase clickedCharacter = hit.collider.GetComponent<CharactersBase>();
                if (clickedCharacter != null)
                {
                    SelectCharacter(clickedCharacter);
                }
            }
        }
    }

    private void SelectCharacter(CharactersBase newCharacter)
    {
        if (currentSelectedCharacter != null && currentSelectedCharacter != newCharacter)
        {
            currentSelectedCharacter.GetComponent<UnitStateManager>().ChangeState(new UnitIdleState());
        }

        currentSelectedCharacter = newCharacter;

        if (currentSelectedCharacter != null)
        {
            currentSelectedCharacter.GetComponent<UnitStateManager>().ChangeState(new UnitSelectedState());
        }
    }
}
