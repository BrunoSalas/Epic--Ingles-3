using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys;
using SpatialSys.UnitySDK;
public class CompleteTask : MonoBehaviour
{
    public static CompleteTask Instance;
    public SpatialQuest quest;

    private void Awake()
    {
        Instance = this;
    }
    public void CompleteTaskByIndex(int index)
    {
        quest.tasks[index].CompleteTask();
    }
}
