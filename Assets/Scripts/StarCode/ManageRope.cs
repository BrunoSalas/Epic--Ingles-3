using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRope : MonoBehaviour
{
    public enum ropeType
    {
        none,
        oxigen,
        water,
        energy,
        danger
    }

    public ropeType currenType = ropeType.none;

    public static ManageRope instance;
    public List<RopeVerlet> ropes = new List<RopeVerlet>();
    public SpatialQuest quest;
    public int indexQuest;

    private void Awake()
    {
        instance = this;
    }


    public void Check()
    {
        foreach (RopeVerlet rope in ropes)
        {
            if (!rope.complete)
            {
                return;
            }
        }
        quest.tasks[indexQuest].CompleteTask();
    }

    public void DetachAll()
    {
        currenType = ropeType.none;
        foreach (RopeVerlet rope in ropes)
        {
            if (!rope.complete)
            {
                rope.DetachEnd();
                rope.interactable.SetActive(true);
            }
        }
    }
}
