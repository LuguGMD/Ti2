using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private Transform enemiesParent; // Parent of all enemies that moves with the background
    public float backgroundSpeed;
    
    public TextAsset jsonFile; // Json file that determines enemies positions
    public Track track = new Track();

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
        track = JsonUtility.FromJson<Track>(jsonFile.text);
    }

    public void BuildLevel()
    {
        enemiesParent = GameObject.Find("EnemyLayout").GetComponent<Transform>();
        foreach (Note note in track.notes)
        {
            Vector3 position;
            GameObject enemy;
            float positionX = note.time * backgroundSpeed; // Position in X axis that the enemy will be placed
            switch (note.name)
            {
                case "C0":
                    break;
                case "C1":
                    break;
                case "D0":
                    position = new Vector3(positionX, enemyPrefabs[2].transform.position.y, enemyPrefabs[2].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[2], position, enemyPrefabs[2].transform.rotation, enemiesParent);
                    enemy.GetComponent<EnemyBehaviour>().noteDuration = note.duration;
                    break;
                case "D1":
                    break;
                case "E0":
                    break;
                case "E1":
                    break;
                case "F0":
                    break;
                case "F1":
                    break;
                case "G0":
                    break;
                case "G1":
                    position = new Vector3(positionX, enemyPrefabs[9].transform.position.y, enemyPrefabs[9].transform.position.z);
                    enemy = Instantiate(enemyPrefabs[9], position, enemyPrefabs[9].transform.rotation, enemiesParent);
                    enemy.GetComponent<EnemyBehaviour>().noteDuration = note.duration;
                    break;
                case "A0":
                    break;
                case "A1":
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
