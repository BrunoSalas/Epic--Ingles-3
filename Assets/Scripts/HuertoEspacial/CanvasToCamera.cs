using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToCamera : MonoBehaviour
{

    void Update()
    {
        Quaternion cameraRotation = SpatialBridge.cameraService.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.forward,
                            cameraRotation * Vector3.up);
    }
  
}
