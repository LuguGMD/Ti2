using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    public GameObject[] tiles;
    public Vector3 triggerPosition;
    public Vector3 instatiatePosition;
    public Vector3 offset;

    private Transform playerTransform;
    private Transform tilesParent;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        tilesParent = GameObject.Find("Tiles").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x >= triggerPosition.x)
        {
            InstatiateTile();
        }
    }

    void InstatiateTile()
    {
        int index = Random.Range(0, tiles.Length);
        Instantiate(tiles[index], instatiatePosition, Quaternion.identity, tilesParent);
        triggerPosition += offset;
        instatiatePosition += offset;

        // Deletes tiles off screen
        Destroy(tilesParent.GetChild(0).gameObject);
    }
}
