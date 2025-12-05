using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoolForAnim : MonoBehaviour
{
    public Animator animInspector;
    public void ActivateAnim(Animator animator)
    {
        animator.SetBool("Activate", true);
    }
    public void ActivateAnimInspe()
    {
        animInspector.SetBool("Activate", true);
    }
}
