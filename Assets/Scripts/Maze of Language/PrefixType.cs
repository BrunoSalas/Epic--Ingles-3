using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixType : MonoBehaviour
{
    public enum Type
    {
        IN,
        IM,
        IR
    }
    public Type type;
    public SpatialInteractable interactable;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
