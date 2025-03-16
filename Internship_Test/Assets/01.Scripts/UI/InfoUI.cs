using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField]
    private Transform slotPerant;
    private List<InfoSlotData> slots = new List<InfoSlotData>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image monsterImage;

    public void CreateMonsterInfoSlot(string key)
    {
        var uiResource = ResourceManager.Instance.LoadResource<InfoSlotData>("InfoSlot", EMajorType.Prefab, ESubType.UI);
        var monster = ResourceManager.Instance.LoadResource<Enemy>(key, EMajorType.Prefab, ESubType.Enemy);
        var sprite = ResourceManager.Instance.LoadResource<Sprite>(key, EMajorType.Data,ESubType.Sprite);

        InfoSlotData created = Instantiate(uiResource, slotPerant);
        created.SetInfoData(sprite, monster.Data, slots.Count);

        slots.Add(created);
    }

    public void EnableUI()
    {
        Time.timeScale = 0;
        SelectIcon(0);
        gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void SelectIcon(int index)
    {
        monsterImage.sprite = slots[index].SpriteData;
        nameText.text = slots[index].Data.Name;
        descriptionText.text = slots[index].Data.Description;
    }
}
