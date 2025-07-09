using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used only to create tutorials
[ExecuteInEditMode]
public class FPSCap : MonoBehaviour
{
    public int targetFps = 30;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
#endif
    }
}
