using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("BackgroundTile"))
        {
            Destroy(col.gameObject);
        }
    }
}
