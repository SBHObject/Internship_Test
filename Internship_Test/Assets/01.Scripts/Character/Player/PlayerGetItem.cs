using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    private PlayerCharacter character;

    private IInteractable currentInteracte;

    private void Awake()
    {
        character = GetComponent<PlayerCharacter>();

        character.InputController.OnInteracteInput += Interacte;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out DropItem item))
        {
            item.ItemEffect(character);
        }

        if (collision.transform.TryGetComponent(out IInteractable interactable))
        {
            currentInteracte = interactable;
            interactable.ShowInteracteEffect();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IInteractable interactable))
        {
            interactable.HideInteracteEffect();
            currentInteracte = null;
        }
    }

    private void Interacte()
    {
        if (currentInteracte != null)
        {
            currentInteracte.OnInteracte(character);
        }
    }
}
