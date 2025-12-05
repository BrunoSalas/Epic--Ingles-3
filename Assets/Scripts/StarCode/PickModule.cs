using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickModule : MonoBehaviour
{
    public static PickModule instance;

    public ItemType.Type type;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;

    [Header("Offset al sostener (relativo a la cabeza)")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Update()
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

        if (SpatialBridge.cameraService != null)
        {
            //currentObject.transform.LookAt(SpatialBridge.cameraService.position);
        }

        if (Vector3.Distance(currentObject.transform.position, targetPos) < 0.05f)
        {
            currentObject.transform.position = targetPos;
            // Aumentar velocidad para el final (opcional)
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

        ItemType typeObj = obj.GetComponent<ItemType>();
        interactable = obj.GetComponent<SpatialInteractable>();
        if (interactable != null) interactable.enabled = false;

        ItemType item = obj.GetComponent<ItemType>();
        if (item != null)
        {
            type = item.type;
            Debug.Log($"Agarraste un {type}: {obj.name}");
        }
        else
        {
            Debug.LogWarning("El objeto no tiene ItemType.");
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
            ItemType t = currentObject.GetComponent<ItemType>();
            if (t != null)
            {
                t.ResetTransform();
            }

            if (interactable != null)
            {
                interactable.enabled = true;
            }

            currentObject = null;
            isMoving = false;
        }
    }

    public GameObject GetCurrentObject()
    {
        return currentObject;
    }
}
