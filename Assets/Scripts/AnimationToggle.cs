using UnityEngine;
using UnityEngine.UI;

public class AnimationToggle : MonoBehaviour
{
    public Animator animator;  

    private bool swordFall = true; 

    public void AnimateSword()
    {       
        if (swordFall)

        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("IDLE"))
            {
                swordFall = !swordFall;

                Debug.Log("1");
                animator.SetTrigger("SwordFall");
            }
        } 
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                swordFall = !swordFall;
                Debug.Log("2");
                animator.SetTrigger("SwordUp");
            }
        }       
    }
}