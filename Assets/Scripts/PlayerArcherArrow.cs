using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherArrow : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private float _arrowSpeed;

    public void Spawn()
    {
        GameObject arrow = Instantiate(_arrow, transform.position, transform.rotation);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Spawn();
        }
    }
}
