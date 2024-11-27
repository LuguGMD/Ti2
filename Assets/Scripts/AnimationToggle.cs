using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationToggle : MonoBehaviour
{
    public Animator animator;

    public List<GameObject> disableMenus;
    public List<GameObject> enableMenus;

    private bool swordFall = true; 

    public void AnimateSword()
    {       
        if (swordFall)

        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("IDLE"))
            {
                swordFall = !swordFall;
                animator.SetTrigger("SwordFall");
                for (int i = 0; i < disableMenus.Count; i++)
                {
                    disableMenus[i].SetActive(false);
                }
            }
        } 
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                swordFall = !swordFall;
                animator.SetTrigger("SwordUp");
            }
        } 
        
        for (int i = 0;i < enableMenus.Count;i++)
        {
            enableMenus[i].SetActive(swordFall);
        }
    }
}