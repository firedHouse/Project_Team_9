using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    void Update()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj;
        player.transform.position = playerObj.transform.position;
        player.transform.rotation = playerObj.transform.rotation;
        Vector3 Pos = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
        transform.position = Pos;
    }
}
