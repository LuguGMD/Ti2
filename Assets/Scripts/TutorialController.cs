using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public bool tutorial;

    [SerializeField] float minDist;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    [SerializeField] GameObject target;

    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<int> type;

    [SerializeField] Move move;
    float moveSpd;

    // Start is called before the first frame update
    void Start()
    {
        moveSpd = move.XSpeed;
        tutorial = false;
    }

    private void Update()
    {
        if (enemies.Count > 0)
        {
            if (!tutorial && enemies[0] != null)
            {
                float dist = Mathf.Abs(enemies[0].transform.position.x - target.transform.position.x);
                if (dist < minDist)
                {
                    StartTutorial(type[0]);
                    tutorial = true;
                }
            }
            else
            {
                if (enemies[0] == null)
                {
                    enemies.RemoveAt(0);
                    type.RemoveAt(0);
                    tutorial = false;
                    EndTutorial();
                }
            }
        }
    }

    public void StartTutorial(int tutorial)
    {
        switch (tutorial)
        {
            //Air tutorial
            case 0:
                leftHand.SetActive(true);
                break;
            //Ground tutorial
            case 1:
                rightHand.SetActive(true);
                break;
            //Defense tutorial
            case 2:
                leftHand.SetActive(true);
                rightHand.SetActive(true);
                break;
        }

        FreezeGame();

    }

    public void EndTutorial()
    {
        AudioController.instance.PlayMusic();
        move.XSpeed = moveSpd;
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }

    public void FreezeGame()
    {
        AudioController.instance.PauseMusic();
        move.XSpeed = 0;
    }

}
