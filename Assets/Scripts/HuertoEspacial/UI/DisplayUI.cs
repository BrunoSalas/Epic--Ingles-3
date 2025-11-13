using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    public GameObject dropDown;
    public GameObject panelInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal"))
        {
            dropDown.SetActive(true);
            panelInfo.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal"))
        {
            dropDown.SetActive(false);
            panelInfo.SetActive(false);
        }
    }
}
