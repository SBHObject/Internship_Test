using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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

    private Transform target;
    private Vector2 targetDir;
    [SerializeField]
    private Transform weaponPivot;
    private float timer = 0;
    private readonly float searchTime = 0.2f;

    private float attackTimer = 0;

    private PlayerCharacter player;

    public void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        LoadWeapons();

        for(int i = 0; i < 6; i++)
        {
            weaponCount.Add(10000 + i, 0);
        }

        AddWeapon(shovel);
        AddWeapon(handgun);

        EquipWeapon(shovel);
    }

    private void Start()
    {
        player.InputController.OnNumber1Input += Input1;
        player.InputController.OnNumber2Input += Input2;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        if (searchTime < timer)
        {
            TargetSearch();
            timer = 0;
        }

        if(target != null)
        {
            RotateWeapon(targetDir);
            AttackCall();
        }
    }

    public void LoadWeapons()
    {
        List<EquipedWeapon> weapons = ResourceManager.Instance.LoadAllResources<EquipedWeapon>(EMajorType.Prefab, ESubType.Weapon);

        for (int i = 0; i < weapons.Count; i++)
        {
            var created = Instantiate(weapons[i], weaponPosition);
            playerWeapons.Add(created.GetItemData().ItemID, created);
            created.gameObject.SetActive(false);

            UIManager.Instance.weaponsInfoUI.CreateWeaponIcon(created.GetItemData().ItemID);
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
        if (currentWeapon == weaponId) return;
        if (weaponCount[weaponId] <= 0) return;

        attackTimer = 0;

        if (currentWeapon != 0)
        {
            playerWeapons[currentWeapon].gameObject.SetActive(false);
        }

        currentWeapon = weaponId;
        playerWeapons[currentWeapon].gameObject.SetActive(true);
    }

    public void Input1()
    {
        ChangeWeapon(1);
    }

    public void Input2()
    {
        ChangeWeapon(4);
    }

    public void ChangeWeapon(int index)
    {
        int id = GetIdByIndex(index);
        if(weaponCount[id] > 0)
        {
            EquipWeapon(id);
        }
    }

    private void TargetSearch()
    {
        var targetCol = Physics2D.OverlapCircleAll(transform.position, playerWeapons[currentWeapon].GetItemData().AtkRange, LayerMask.GetMask("Enemy"));
        float min = float.MaxValue;

        Transform targetMin = null;

        for(int i = 0; i < targetCol.Length; i++)
        {
            if (Vector2.SqrMagnitude(targetCol[i].transform.position - transform.position) < min)
            {
                targetMin = targetCol[i].transform;
            }
        }

        if(targetMin != null)
        {
            target = targetMin.transform;
            targetDir = (targetMin.position - transform.position).normalized;
        }
        else
        {
            target = null;
        }
    }

    private void RotateWeapon(Vector2 dir)
    {
        float rotz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        playerWeapons[currentWeapon].FlipSprite(Mathf.Abs(rotz) > 90f);
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotz);
    }

    private void AttackCall()
    {
        if(attackTimer >= playerWeapons[currentWeapon].GetItemData().AtkSpeed)
        {
            playerWeapons[currentWeapon].DoAttack(weaponPivot.rotation);
            attackTimer = 0;
        }
    }

    private int GetIdByIndex(int index)
    {
        int target;
        switch (index)
        {
            case 1:
                target = shovel;
                break;
            case 2:
                target = rake;
                break;
            case 3:
                target = sickle;
                break;
            case 4:
                target = handgun;
                break;
            case 5:
                target = rifle;
                break;
            case 6:
                target = shotgun;
                break;
            default:
                target = shovel;
                break;
        }

        return target;
    }
}
