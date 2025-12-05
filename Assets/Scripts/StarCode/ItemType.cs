using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum Type
    {
        none,
        good,
        wrong
    }

    public Type type = Type.none;

    public SpatialInteractable interactable;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetTransform()
    {
        transform.SetParent(null);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
        if (interactable != null) interactable.enabled = true;
    }
}
