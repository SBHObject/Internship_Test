using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthHUD : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    private PlayerCharacter Player => GamePlayManager.Instance.playerChar;

    private void Start()
    {
        Player.Status.OnHpChange += BarUpdate;
    }

    private void BarUpdate()
    {
        healthBar.fillAmount = Player.Status.CurrentHealth / Player.Status.MaxHealth;
    }

}
