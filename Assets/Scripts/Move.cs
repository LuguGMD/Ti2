using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float XSpeed;
    public float ZSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3 (XSpeed, 0, ZSpeed);

        transform.Translate(direction * Time.deltaTime, Space.World);
    }
}
