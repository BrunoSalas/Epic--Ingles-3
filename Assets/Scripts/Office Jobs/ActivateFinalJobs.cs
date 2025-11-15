using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalJobs : MonoBehaviour
{
    public List<ChooseOfficeJob> objects = new List<ChooseOfficeJob>();
    public static ActivateFinalJobs instance;
    public SpatialQuest quest;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (ChooseOfficeJob obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        quest.tasks[0].CompleteTask();
    }
}
