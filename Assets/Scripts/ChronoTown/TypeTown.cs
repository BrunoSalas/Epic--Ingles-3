
using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class TypeTown : MonoBehaviour
{
    public enum Type
    {
        Null,
        One,
        Secund,
        Terce
    }

    public Type type;
    [HideInInspector] public SpatialInteractable interactable;

    private Vector3 initialPosition;

    private void Start()
    {
        interactable = GetComponent<SpatialInteractable>();
        initialPosition = transform.localPosition;
    }
    public void Back()
    {
        interactable.enabled = true;
        transform.localPosition = initialPosition;
    }
}
