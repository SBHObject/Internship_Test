using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    private readonly int shovel = 10000;
    private readonly int rake = 10001;
    private readonly int sickle = 10002;
    private readonly int handgun = 10003;
    private readonly int rifle = 10004;
    private readonly int shotgun = 10005;

    //무기 소지 갯수
    private Dictionary<int, int> weaponCount = new Dictionary<int, int>();
    private Dictionary<int, EquipedWeapon> playerWeapons = new Dictionary<int, EquipedWeapon>();

    [SerializeField]
    private Transform weaponPosition;

    private int currentWeapon;

    public event Action<int, int> OnAddWeapon;

    public void Awake()
    {
        LoadWeapons();

        for(int i = 0; i < 6; i++)
        {
            weaponCount.Add(10000 + i, 0);
        }

        AddWeapon(shovel);
        EquipWeapon(shovel);
    }

    public void LoadWeapons()
    {
        List<EquipedWeapon> weapons = ResourceManager.Instance.LoadAllResources<EquipedWeapon>(EMajorType.Prefab, ESubType.Weapon);

        for (int i = 0; i < weapons.Count; i++)
        {
            var created = Instantiate(weapons[i], weaponPosition);
            playerWeapons.Add(10000 + i, created);
            created.gameObject.SetActive(false);
        }
    }

    public void AddWeapon(int weaponId)
    {
        if(weaponCount.ContainsKey(weaponId))
        {
            weaponCount[weaponId] += 1;
            OnAddWeapon?.Invoke(weaponId, weaponCount[weaponId]);
        }
    }

    public void EquipWeapon(int weaponId)
    {
        if (weaponCount[weaponId] <= 0) return;

        currentWeapon = weaponId;
        playerWeapons[weaponId].gameObject.SetActive(true);
    }

}
