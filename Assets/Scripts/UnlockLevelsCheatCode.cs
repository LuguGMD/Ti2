using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;

public class UnlockLevelsCheatCode : MonoBehaviour
{
    [SerializeField] GameObject level2Button;
    [SerializeField] SaveSystem saveSystem;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 4)
        {
            level2Button.GetComponent<Button>().interactable = true;
            SetPlayerData();
        }
    }

    // Unlocks all levels
    public void SetPlayerData()
    {
        PlayerData playerData = saveSystem.LoadPlayerData();

        foreach (Level level in playerData.levels)
        {
            level.unlocked = true;
        }

        saveSystem.SavePlayerData(playerData);
    }
}
