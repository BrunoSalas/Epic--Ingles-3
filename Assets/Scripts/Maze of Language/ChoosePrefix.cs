using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePrefix : MonoBehaviour
{

    public PrefixType.Type objectType;
    public PickPrefix pick;
    public float moveSpeed = 3f;
    public GameObject ActivateGameObject;
    [HideInInspector] public bool completed;
    public SpatialInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    private void Update()
    {
        if (completed)
        {
            interactable.enabled = false;
        }
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            ActivateGameObject.SetActive(true);
            pick.currentObject.SetActive(false);
            pick.Release();
            completed = true;
            ActivateFinalPrefix.instance.AreAllComplete();
            interactable.enabled = false;
        }
    }
}
