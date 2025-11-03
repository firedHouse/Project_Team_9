using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTxt : MonoBehaviour
{
    public TextMesh dmgText;
    void Start()
    {
        if (dmgText == null)
        {
             dmgText = GetComponent<TextMesh>();
        }
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }

    public void DisplayDamage(float dmg)
    {
        if (dmgText == null)
        {
            dmgText = GetComponent<TextMesh>();
        }
        dmgText.text = ((int)dmg).ToString();

    }
}
