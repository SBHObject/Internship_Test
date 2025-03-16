using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSlotData : MonoBehaviour
{
    [SerializeField]
    private Image monsterImage;
    private int number;

    public MonsterDataSO Data { get; private set; }
    public Sprite SpriteData { get; private set; }

    public void SetInfoData(Sprite sprite, MonsterDataSO baseData, int num)
    {
        SpriteData = sprite;
        monsterImage.sprite = sprite;
        Data = baseData;
        number = num;
    }

    public void ClickIcon()
    {
        UIManager.Instance.infoUI.SelectIcon(number);
    }
}
