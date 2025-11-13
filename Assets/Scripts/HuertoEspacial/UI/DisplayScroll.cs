using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScroll : MonoBehaviour
{
    public Animator animator;
    bool isOpen = false;
    public void OpenClose()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen",isOpen);
    }
}
