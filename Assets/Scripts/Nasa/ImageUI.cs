using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUI : MonoBehaviour
{
    public GameObject telescope;
    public Animator animator;
    float timer;
    void Update()
    {
        if (animator.GetBool("Active") == true)
        {
            if (timer >= 0.5f)
            {
                if (Input.anyKeyDown)
                {
                    animator.SetBool("Active", false);
                    timer = 0;
                }

                if (Input.touchCount > 0)
                {
                    animator.SetBool("Active", false);
                    timer = 0;
                }

                Debug.Log(Vector3.Distance(SpatialBridge.actorService.localActor.avatar.position, telescope.transform.position));
                if (Vector3.Distance(SpatialBridge.actorService.localActor.avatar.position, telescope.transform.position) >= 6f)
                {
                    animator.SetBool("Active", false);
                    timer = 0;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        


    }
}
