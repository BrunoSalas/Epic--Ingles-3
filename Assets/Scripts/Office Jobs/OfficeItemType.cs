using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeItemType : MonoBehaviour
{
    public enum Type
    {
        Doctor,
        Firefighter,
        PoliceOfficer,
        Teacher,
        Vet,
        Farmer,
        Chef,
        Pilot,
        Builder,
        MailCarrier,
        Waiter,
        Mechanic,
        Artist,
        Singer,
        Musician,
        Dancer,
        Actor,
        Photographer,
        Astronaut,
        Scientist,
        Gardener,
        Dentist,
        Explorer,
        Judge
    }
    public Type type;
    [HideInInspector]public SpatialInteractable interactable;

    [Header("Initial Transform")]
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        interactable = GetComponent<SpatialInteractable>();
    }

    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
