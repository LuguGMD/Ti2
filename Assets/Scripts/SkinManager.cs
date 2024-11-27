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
        //if (skinsList[currentSkin].price <= money)
        //{
            skinsList[currentSkin].unlocked = true;
            //money -= skinsList[currentSkin].price;
        //}
    }

    public void ChooseSkin()
    {
        if (skinsList[currentSkin].unlocked)
        {
            GameManager.playerSkin = skinsList[currentSkin].mat;
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
    public float price;
    public bool unlocked;
}
