using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class RiddleMision : MonoBehaviour
{
    public SpatialQuest quest;


    public void CompleteMission(int i)
    {
        quest.tasks[i].CompleteTask();
    }
}
