using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PlaceTown : MonoBehaviour
{
    public TypeTown.Type objectType;
    public PickTown pick;
    public float moveSpeed = 3f;
    [HideInInspector] public bool completed;
    public FinalParte FinalParte;
    public GameObject error;
    public SpatialInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            pick.Release();
            completed = true;
            FinalParte.AreAllComplete();
            interactable.enabled = false;
            error.SetActive(false);
        }
        else
        {
            pick.currentObject.GetComponent<TypeTown>().Back();
            pick.Release();
            pick.interactable.enabled = true;
            error.SetActive(false);
            error.SetActive(true);
        }
    }
}
