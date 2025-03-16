using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInfo : MonoBehaviour
{
    public void CreateWeaponIcon(int id)
    {
        WeaponIconUI ui = ResourceManager.Instance.LoadResource<WeaponIconUI>("WeaponIconUI", EMajorType.Prefab, ESubType.UI);
        Sprite icon = ResourceManager.Instance.LoadResource<Sprite>(id.ToString(), EMajorType.Data, ESubType.Sprite);

        WeaponIconUI created = Instantiate(ui, transform);
        created.SetData(icon, id);
    }
}
