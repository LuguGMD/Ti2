using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgroundTile : MonoBehaviour
{
    public GameObject backgroundTilePrefab;
    public Transform spawnPoint;
    public Transform tileParent;

    private void Start()
    {
        spawnPoint = GameObject.Find("TileSpawnPoint").transform;
        tileParent = GameObject.Find("Background").transform;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Instantiate(backgroundTilePrefab, spawnPoint.position, spawnPoint.rotation, tileParent); // Instantiate a tile in the spawn point as a child of background
        }
    }

}
