using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptionsPanel : MonoBehaviour
{
    public GameObject[] panels;

    public void ClosePanels()
    {
        for (int i = 0; i < panels.Length; i++) 
        {
            panels[i].SetActive(false);
        }
    }

}
