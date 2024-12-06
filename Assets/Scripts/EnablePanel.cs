using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePanel : MonoBehaviour
{
    public GameObject panel;
    private bool panelEnabled;

    private void Update()
    {
        panelEnabled = panel.activeSelf;
    }

    public void ChangePanelState()
    {
        if (!panelEnabled)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }

        panelEnabled = !panelEnabled;
    }
}
