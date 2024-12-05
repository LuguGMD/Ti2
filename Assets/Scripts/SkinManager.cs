using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    int currentSkin;
    public List<SkinInfo> skinsList;
    public TextMeshProUGUI buyText;

    public List<SkinnedMeshRenderer> skinPreview;


    public void BuySkin()
    {
        PlayerData playerData = SaveManager.instance.playerData;

        int currentCoins = playerData.currentCoins;
        if (skinsList[currentSkin].price <= currentCoins)
        {
            skinsList[currentSkin].unlocked = true;
            currentCoins -= skinsList[currentSkin].price;
            SaveManager.instance.UpdateCurrentCoinsText(currentCoins);

            // Update save file
            playerData.currentCoins = currentCoins;
            playerData.skins[currentSkin] = true;
            SaveManager.instance.saveSystem.SavePlayerData(playerData);

            // Updates buy skins achivements
            for (int i = 7;i <= 8; i++)
            {
                AchievementSystem.instance.UpdateAchievement(i, 1); // Achivement Id = 7 -> Buy 1 skin achivement
                                                                    // Achivement Id = 8 -> Buy all skins achivement   
            }

            AchievementSystem.instance.CheckUnlocked();
        }
    }

    public void ChooseSkin()
    {
        if (skinsList[currentSkin].unlocked)
        {
            GameManager.playerSkin = skinsList[currentSkin].mat;

            // Updates save file to save equipped skin
            PlayerData playerData = SaveManager.instance.playerData;
            playerData.equippedSkinIndex = currentSkin;
            SaveManager.instance.saveSystem.SavePlayerData(playerData);
        }
        else
        {
            BuySkin();
        }

        UpdateText();

    }

    public void ScrollSkin(int side)
    {
        currentSkin += side;

        if(currentSkin >= skinsList.Count) currentSkin = 0;
        if (currentSkin < 0) currentSkin = skinsList.Count - 1;

        for (int i = 0; i < skinPreview.Count; i++)
        {
            skinPreview[i].material = skinsList[currentSkin].mat;
        }

        UpdateText();
    }

    public void SetSkin(int index)
    {
        currentSkin = index;

        GameManager.playerSkin = skinsList[currentSkin].mat;
        for (int i = 0; i < skinPreview.Count; i++)
        {
            skinPreview[i].material = skinsList[currentSkin].mat;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        if (skinsList[currentSkin].unlocked)
        {
            if (GameManager.playerSkin == skinsList[currentSkin].mat)
            {
                buyText.text = "Eqquiped";
            }
            else
            {
                buyText.text = "Change";
            }
        }
        else
        {
            buyText.text = $"BUY: {skinsList[currentSkin].price}";
        }

    }

}

[System.Serializable]
public class SkinInfo
{
    public Material mat;
    public int price;
    public bool unlocked;
}
