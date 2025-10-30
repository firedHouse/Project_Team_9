using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    void Update()
    {
        Vector3 Pos = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
        transform.position = Pos;
    }
}
