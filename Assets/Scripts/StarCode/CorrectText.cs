using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorrectText : MonoBehaviour
{
    public bool completed;
    public GameObject correctGameObject;

    public void Complete()
    {
        completed = true;
        if (correctGameObject != null)
        {
            correctGameObject.SetActive(true);
        }
    }
}
