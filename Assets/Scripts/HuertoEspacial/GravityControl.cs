using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    private float earthGravity = -9.81f;
    private float moonGravity = -1.62f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0,moonGravity,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
