using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amount;

    [SerializeField]
    private Image iconImage;

    private int weaponId;

    private PlayerCharacter Player => GamePlayManager.Instance.playerChar;

    private void Start()
    {
        Player.WeaponSystem.OnAddWeapon += UpdateAmount;
    }

    public void SetData(Sprite sprite, int id)
    {
        weaponId = id;
        iconImage.sprite = sprite;

        amount.text = "1";
    }

    public void UpdateAmount(int id, int nowAmount)
    {
        if(id == weaponId)
        {
            amount.text = nowAmount.ToString();
        }
    }
}
