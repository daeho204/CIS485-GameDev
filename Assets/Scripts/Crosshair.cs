using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    //accuracy depending on the crosshair type
    private float gunAccuracy;

    //activate&deactivate crosshair
    [SerializeField]
    private GameObject go_CrosshhairHUD;
    // Start is called before the first frame update
   

    // Update is called once per frame
    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        animator.SetBool("Walking", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        animator.SetBool("Running", _flag);
    }

    public void JumpingAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void FireAnimation()
    {
        if (animator.GetBool("Walking"))
        {
            animator.SetTrigger("Walk_Fire");

        }
        else
        {
            animator.SetTrigger("Idle_Fire");
        }
    }
}
