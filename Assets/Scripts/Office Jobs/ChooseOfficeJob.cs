using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseOfficeJob : MonoBehaviour
{

    public OfficeItemType.Type objectType;
    public PickOfficeTirem pick;
    public float moveSpeed = 3f;
    public Transform pos;
    [HideInInspector] public bool completed;
    public SpatialInteractable interactable;

    [Header("CanvasUI")]
    public TMP_Text textJob;
    public GameObject CanvasUI;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        textJob = CanvasUI.GetComponent<TMP_Text>();
    }
    private void Start()
    {
        interactable.onInteractEvent.unityEvent.AddListener(TryPlaceObject);
        textJob.text = objectType.ToString();
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
            StartCoroutine(MoveToPosition(pick.currentObject, pos));
            pick.Release();
            completed = true;
            ActivateFinalJobs.instance.AreAllComplete();
            interactable.enabled = false;
            CanvasUI.SetActive(true);
        }
    }

    private IEnumerator MoveToPosition(GameObject obj, Transform target)
    {
        while (obj != null && Vector3.Distance(obj.transform.position, target.position) > 0.05f)
        {
            obj.transform.position = Vector3.Lerp(
                obj.transform.position,
                target.position,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }

        if (obj != null)
        {
            obj.transform.position = target.position;
            obj.transform.SetParent(target);
            obj.transform.rotation = Quaternion.LookRotation(-target.forward, Vector3.up);
        }
    }
}
