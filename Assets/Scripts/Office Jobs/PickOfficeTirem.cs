using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOfficeTirem : MonoBehaviour
{
    public OfficeItemType.Type currentType;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;
    private float defaultMoveSpeed;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        defaultMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (currentObject == null) return;

        if (SpatialBridge.actorService == null) return;

        var headTransform = SpatialBridge.actorService.localActor.avatar
            .GetAvatarBoneTransform(HumanBodyBones.Head);

        if (headTransform == null) return;

        Vector3 targetPos = headTransform.position + offset;

        currentObject.transform.position = Vector3.Lerp(
            currentObject.transform.position,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        //if (SpatialBridge.cameraService != null)
        //{
        //    currentObject.transform.LookAt(SpatialBridge.cameraService.position);
        //    currentObject.transform.Rotate(0, 180f, 0);
        //}

        if (Vector3.Distance(currentObject.transform.position, targetPos) < 0.05f)
        {
            currentObject.transform.position = targetPos;
            moveSpeed = 20f;
        }
    }

    public void PickUp(GameObject obj)
    {
        if (currentObject != null)
        {
            OfficeItemType prevType = currentObject.GetComponent<OfficeItemType>();
            if (prevType != null)
            {
                if (prevType.interactable != null)
                    prevType.interactable.enabled = true;

                prevType.ResetTransform();
            }
            // limpiar referencia anterior antes de asignar la nueva
            currentObject = null;
            isMoving = false;
        }

        // Asignar y preparar el nuevo objeto
        currentObject = obj;
        if (currentObject == null) return;

        currentObject.SetActive(true);
        isMoving = true;
        moveSpeed = defaultMoveSpeed; // restaurar velocidad por si antes se aumentó

        OfficeItemType typeObj = obj.GetComponent<OfficeItemType>();
        if (typeObj != null)
        {
            interactable = typeObj.interactable;
            if (interactable != null)
                interactable.enabled = false;

            currentType = typeObj.type;
            Debug.Log($"Agarraste un {currentType}: {obj.name}");
        }
        else
        {
            Debug.LogWarning("El objeto no tiene TypeObject asignado");
        }
    }
    public void Release()
    {
        if (currentObject != null)
        {
            currentObject = null;
            isMoving = false;
            moveSpeed = defaultMoveSpeed;
        }
    }
}
