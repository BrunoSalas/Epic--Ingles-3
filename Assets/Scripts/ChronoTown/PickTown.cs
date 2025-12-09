using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PickTown : MonoBehaviour
{
    public TypeTown.Type currentType;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);



    private void Update()
    {
        if (isMoving)
        {
            if (currentObject == null) return;
            if (SpatialBridge.actorService == null) return;

            Vector3 targetPos = SpatialBridge.actorService.localActor.avatar
                .GetAvatarBoneTransform(HumanBodyBones.Head).position + offset;

            currentObject.transform.position = Vector3.Lerp(
                currentObject.transform.position,
                targetPos,
                Time.deltaTime * moveSpeed
            );



            // Ajuste final si está muy cerca
            if (Vector3.Distance(currentObject.transform.position, targetPos) < 0.05f)
            {
                currentObject.transform.position = targetPos;
                moveSpeed = 20f;
            }
        }
    }

    public void PickUp(GameObject obj)
    {
        if (currentObject != null)
            return;
        currentObject = obj;

        isMoving = true;

        TypeTown typeObj = obj.GetComponentInChildren<TypeTown>();
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
}
