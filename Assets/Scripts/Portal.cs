using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string url = "https://example.com";

    public void OpenPortalUrl()
    {
        // Si el Spatial SDK está inicializado, usa su servicio para abrir el URL (comportamiento cross-platform descrito en la doc)
        try
        {
            if (SpatialBridge.spaceService != null)
            {
                SpatialBridge.spaceService.OpenURL(url);
                return;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Spatial spaceService OpenURL fallo: " + e.Message);
        }
    }
}
