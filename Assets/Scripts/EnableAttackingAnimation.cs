using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAttackingAnimation : MonoBehaviour
{
    [SerializeField] private TouchManager touchManager;

    public void SetAttacking()
    {
        touchManager.SetAttacking(false);
    }
}
