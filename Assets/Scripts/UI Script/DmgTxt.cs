using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTxt : MonoBehaviour
{
    public TextMesh dmgText;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }

    public void DisplayDamage(float dmg)
    {
        dmgText.text = dmg.ToString();

    }
}
