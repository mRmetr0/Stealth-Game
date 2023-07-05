using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotater : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 2;
    private void Update()
    {
        transform.Rotate (new Vector3(0, rotSpeed, 0));
        
    }

}
