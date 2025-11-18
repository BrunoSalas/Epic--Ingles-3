using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalPrefix : MonoBehaviour
{
    public List<ChoosePrefix> objects = new List<ChoosePrefix>();
    public static ActivateFinalPrefix instance;
    public SpatialQuest quest;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (ChoosePrefix obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        quest.tasks[0].CompleteTask();
    }
}
