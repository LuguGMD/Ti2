using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;

public class UnlockLevelsCheatCode : MonoBehaviour
{
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
            foreach (GameObject button in SaveManager.instance.levelButtons)
            {
                button.GetComponent<Button>().interactable = true;
            }
            SetPlayerData();
        }
    }

    // Unlocks all levels
    public void SetPlayerData()
    {
        PlayerData playerData = saveSystem.LoadPlayerData();

        foreach (LevelSaveData level in playerData.levels)
        {
            level.unlocked = true;
        }

        saveSystem.SavePlayerData(playerData);
    }
}
