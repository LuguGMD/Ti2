using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public bool tutorial;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    [SerializeField] Move move;
    [SerializeField] float moveSpd;

    // Start is called before the first frame update
    void Start()
    {
        moveSpd = move.XSpeed;
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

    }

    public void EndTutorial()
    {
        AudioController.instance.PlayMusic();
        move.XSpeed = moveSpd;
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        EnableControls();
    }

    public void FreezeGame()
    {
        AudioController.instance.PauseMusic();
        move.XSpeed = 0;
    }

    public void DisableControls()
    {

    }

    public void EnableControls()
    {

    }

}
