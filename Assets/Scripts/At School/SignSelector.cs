using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignSelector : MonoBehaviour
{
    public int currentIndex;
    public List<GameObject> buttons;
    public List<GameObject> signs;
    public List<SpatialInteractable> interactables;
    public SpatialQuest quest;

    public void SelectSign(int index)
    {
        currentIndex = index;
    }

    public void PlaceSign(int index)
    {
        if(currentIndex == index)
        {
            signs[currentIndex].SetActive(true);
            buttons[currentIndex].SetActive(false);
            interactables[currentIndex].enabled = false;
            CheckCompleted();
        }
    }

    public void CheckCompleted()
    {
        foreach (var item in signs)
        {
            if (!item.activeSelf)
            {
                return;
            }
            
        }

        quest.tasks[0].CompleteTask();
    }
}
