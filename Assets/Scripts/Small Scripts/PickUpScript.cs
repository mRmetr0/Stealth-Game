using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickUpScript : MonoBehaviour
{
    private float yMove;    
    private void Awake () {
        transform.Rotate(new Vector3 (0, Random.Range(0, 360) ,0));
    }
    // Update is called once per frame
    private void Update()
    {
        yMove = Mathf.Sin(Time.time) * 0.005f;
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, yMove, 0));
        transform.Rotate(0, 1, 0);
    }
}
