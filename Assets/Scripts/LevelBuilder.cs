using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private Transform enemiesParent; // Parent of all enemies that moves with the background
    public float backgroundSpeed;
    
    public TextAsset jsonFile; // Json file that determines enemies positions
    public Note[] notes;

    // Classes used to read json file
    [System.Serializable]
    public class Note
    {
        public string name;
        public int midi;
        public float time;
        public float velocity;
        public float duration;
    }

    [System.Serializable]
    public class Track
    {
        public Note[] notes;
    }

    // Start is called before the first frame update
    public void ReadJson()
    {
        notes = JsonUtility.FromJson<Track>(jsonFile.text).notes;
    }

    public void BuildLevel()
    {
        enemiesParent = GameObject.Find("EnemyLayout").GetComponent<Transform>();
        for (int i = 0; i < notes.Length; i++)
        {
            Vector3 position;
            GameObject enemy;
            float[] notesDuration;
            float positionX = notes[i].time * backgroundSpeed; // Position in X axis that the enemy will be placed
            switch (notes[i].name)
            {
                case "C0":
                    position = new Vector3(positionX, enemyPrefabs[0].transform.position.y, enemyPrefabs[0].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[0], position, enemyPrefabs[0].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().nextPositions = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().nextPositions[0] = notes[i + 1].time * backgroundSpeed; // New enemy position after hit 

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[2];
                    notesDuration = enemy.GetComponent<EnemyBehaviour>().notesDuration;
                    notesDuration[0] = notes[i].duration;
                    notesDuration[1] = notes[i++].duration;
                    break;
                case "C1":
                    position = new Vector3(positionX, enemyPrefabs[1].transform.position.y, enemyPrefabs[1].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[1], position, enemyPrefabs[1].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().nextPositions = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().nextPositions[0] = notes[i + 1].time * backgroundSpeed; // New enemy position after hit 

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[2];
                    notesDuration = enemy.GetComponent<EnemyBehaviour>().notesDuration;
                    notesDuration[0] = notes[i].duration;
                    notesDuration[1] = notes[i++].duration;
                    break;
                case "D0":
                    position = new Vector3(positionX, enemyPrefabs[2].transform.position.y, enemyPrefabs[2].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[2], position, enemyPrefabs[2].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
                case "D1":
                    position = new Vector3(positionX, enemyPrefabs[3].transform.position.y, enemyPrefabs[3].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[3], position, enemyPrefabs[3].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
                case "E0":
                    position = new Vector3(positionX, enemyPrefabs[4].transform.position.y, enemyPrefabs[4].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[4], position, enemyPrefabs[4].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().nextPositions = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().nextPositions[0] = notes[i + 1].time * backgroundSpeed; // New enemy position after hit 

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[2];
                    notesDuration = enemy.GetComponent<EnemyBehaviour>().notesDuration;
                    notesDuration[0] = notes[i].duration;
                    notesDuration[1] = notes[i++].duration;
                    break;
                case "E1":
                    position = new Vector3(positionX, enemyPrefabs[5].transform.position.y, enemyPrefabs[5].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[5], position, enemyPrefabs[5].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().nextPositions = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().nextPositions[0] = notes[i + 1].time * backgroundSpeed; // New enemy position after hit 

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[2];
                    notesDuration = enemy.GetComponent<EnemyBehaviour>().notesDuration;
                    notesDuration[0] = notes[i].duration;
                    notesDuration[1] = notes[i++].duration;
                    break;
                case "F0":
                    position = new Vector3(positionX, enemyPrefabs[6].transform.position.y, enemyPrefabs[6].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[6], position, enemyPrefabs[6].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
                case "F1":
                    position = new Vector3(positionX, enemyPrefabs[7].transform.position.y, enemyPrefabs[7].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[7], position, enemyPrefabs[7].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
                case "G0":
                    position = new Vector3(positionX, enemyPrefabs[8].transform.position.y, enemyPrefabs[8].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[8], position, enemyPrefabs[8].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
                case "G1":
                    position = new Vector3(positionX, enemyPrefabs[9].transform.position.y, enemyPrefabs[9].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[9], position, enemyPrefabs[9].transform.rotation, enemiesParent);

                    enemy.GetComponent<EnemyBehaviour>().notesDuration = new float[1];
                    enemy.GetComponent<EnemyBehaviour>().notesDuration[0] = notes[i].duration;
                    break;
            }
        }
    }

    public void DeleteEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            DestroyImmediate(enemies[i]);
        }
    }
}
