using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.Events;

public class FinalParte : MonoBehaviour
{
    public List<PlaceTown> objects = new List<PlaceTown>();
    public SpatialQuest quest;
    public int index;
    public UnityEvent Unitevent;
    public void AreAllComplete()
    {
        foreach (PlaceTown obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        Unitevent.Invoke();
        quest.tasks[index].CompleteTask();
    }
}
