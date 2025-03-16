using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelHUD : MonoBehaviour
{
    [SerializeField]
    private Image expBar;

    [SerializeField]
    private TextMeshProUGUI levelTmp;

    private PlayerCharacter Player => GamePlayManager.Instance.playerChar;

    private void Start()
    {
        Player.Status.OnExpChange += UpdateExp;

        UpdateExp();
    }

    private void UpdateExp()
    {
        expBar.fillAmount = Player.Status.CurrentExp / Player.Status.NeedExp;
        levelTmp.text = Player.Status.Level.ToString();
    }
}
