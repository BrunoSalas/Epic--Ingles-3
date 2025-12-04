using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoolForAnim : MonoBehaviour
{
    public void ActivateAnim(Animator animator)
    {
        animator.SetBool("Activate", true);
    }
}
