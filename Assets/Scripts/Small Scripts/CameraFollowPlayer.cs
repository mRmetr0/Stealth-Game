using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    
    [SerializeField]
    private float yDist = 30;
    [SerializeField]
    private float zDist = 70;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update() {
        transform.position = player.position + new Vector3(0, yDist, zDist);;
    }
}
