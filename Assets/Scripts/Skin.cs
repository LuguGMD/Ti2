using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UpdateSkin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSkin()
    {
        GetComponent<SkinnedMeshRenderer>().material = GameManager.playerSkin;
    }
}
