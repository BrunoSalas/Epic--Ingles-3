using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPrefix : MonoBehaviour
{
    public PrefixType.Type currentType;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Update()
    {
        if (currentObject==null)
        {
            return;
        }
        Vector3 targetPos = SpatialBridge.actorService.localActor.avatar
           .GetAvatarBoneTransform(HumanBodyBones.Head).position + offset;

        currentObject.transform.position = Vector3.Lerp(
            currentObject.transform.position,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        if (SpatialBridge.cameraService != null)
        {
            currentObject.transform.LookAt(SpatialBridge.cameraService.position);

            //currentObject.transform.Rotate(0, 180f, 0);
        }

        // Ajuste final si está muy cerca
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
            ChangeObject();
        }

        currentObject = obj;
        obj.SetActive(true);
        isMoving = true;

        PrefixType typeObj = obj.GetComponent<PrefixType>();
        interactable = typeObj.interactable;
        interactable.enabled = false;

        if (typeObj != null)
        {
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
        }
    }

    public void ChangeObject()
    {
        if (currentObject != null)
        {
            PrefixType typeObj = currentObject.GetComponent<PrefixType>();
            if (typeObj != null)
            {
                typeObj.ResetTransform();
            }

            if (interactable != null)
            {
                interactable.enabled = true;
            }

            currentObject = null;
            isMoving = false;
        }
    }
}
