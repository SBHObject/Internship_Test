using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnInteracte(PlayerCharacter player);
    public void ShowInteracteEffect();
    public void HideInteracteEffect();
}
