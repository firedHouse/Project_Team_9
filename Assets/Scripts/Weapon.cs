using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Job { Warrior, Archer, Rogue, End};
    [SerializeField] private Job _job;
    [SerializeField] private int _weaponLevel = 1;
    [SerializeField] private float _weaponDamage = 3f;
    [SerializeField] private float _weaponRange = 1f;
    [SerializeField] private float _weaponCooldown = 5f;

    public int WeaponLevel
    {
        get => _weaponLevel;
    }

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

    private void Init()
    {

    }

    // 수동 공격
    public void WeaponAttack(float damage)
    {
        Debug.Log($"무기 공격 : damage - {_weaponDamage} range - {_weaponRange}");
    }

    // 무기 스킬 해금

}
