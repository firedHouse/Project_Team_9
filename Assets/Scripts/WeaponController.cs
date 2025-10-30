using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Weapon _weapon;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            WeaponUpgrade();
        }
    }

    public void Init()
    {
        _weapon = GetComponent<Weapon>();
    }

    public void WeaponUpgrade()
    {
        if(_weapon.WeaponLevel >= 10)
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
        _weapon.WeaponDamage += 1f;
    }
    private void WeaponRangeUpgrade()
    {
        Debug.Log("무기 범위 증가");
        _weapon.WeaponRange += 0.5f;
    }
    private void WeaponCooldownUpgrade()
    {
        Debug.Log("무기 데미지 증가");
        _weapon.WeaponCooldown -= 0.5f;
    }
}
