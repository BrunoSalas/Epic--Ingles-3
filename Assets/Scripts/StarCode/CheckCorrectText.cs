using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckCorrectText : MonoBehaviour
{
    public List<CorrectText> correctTexts = new List<CorrectText>();

    public UnityEvent onCompleted;
    public void Check()
    {
        foreach (CorrectText item in correctTexts)
        {
            if (!item.completed)
            {
                return;
            }
        }

        onCompleted.Invoke();
    }
}
