using System;
using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public GameObject HeightMarker;

    void OnTriggerEnter2D(Collider2D col)
    {
        var pos = col.gameObject.transform.position;
        col.gameObject.transform.position = new Vector3(pos.x, HeightMarker.transform.position.y, pos.z);
    }
}
