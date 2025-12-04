using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys;
using SpatialSys.UnitySDK;
public class CompleteTask : MonoBehaviour
{
    public SpatialQuest quest;
    
    public void CompleteTaskByIndex(int index)
    {
        quest.tasks[index].CompleteTask();
    }
}
