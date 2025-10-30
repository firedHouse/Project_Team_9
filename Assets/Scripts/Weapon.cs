using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Job { Warrior, Archer, Rogue, End};
    [SerializeField] private Job _job;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private float _weaponDamage = 3f;
    [SerializeField] private float _weaponRange = 1f;
    [SerializeField] private float _weaponCooldown = 5f;

    public float WeaponDamage
    {
        get => _weaponDamage;
        set => _weaponDamage = value;
    }
    public float WeaponRange
    {
        get => _weaponRange;
        set => _weaponRange = value;
    }
    public float WeaponCooldown
    {
        get => _weaponCooldown;
        set => _weaponCooldown = value;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        playerStats = GetComponent<PlayerStats>();
        playerStats.OnLevelChanged += WeaponUpgrade;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            WeaponAttack();
        }
    }

    // 수동 공격
    public void WeaponAttack()
    {
        Debug.Log($"무기 공격 : damage - {_weaponDamage} range - {_weaponRange}");
    }

    public void WeaponUpgrade(int curLvl, int maxLvl)
    {
        if (curLvl >= maxLvl)
        {
            return;
        }
        WeaponDamageUpgrade();
        WeaponRangeUpgrade();
        WeaponCooldownUpgrade();
    }

    private void WeaponDamageUpgrade()
    {
        Debug.Log("무기 데미지 증가");
        _weaponDamage += 1f;
    }
    private void WeaponRangeUpgrade()
    {
        Debug.Log("무기 범위 증가");
        _weaponRange += 0.5f;
    }
    private void WeaponCooldownUpgrade()
    {
        Debug.Log("무기 쿨타임 감소");
        _weaponCooldown -= 0.5f;
    }

}
