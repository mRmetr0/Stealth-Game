using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private AudioSource Closed;
    [SerializeField]
    private bool canOpen = false;

    // Start is called before the first frame update
    private void Start()
    {
        KeyScript.OnPickedUp += Open;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = transform.GetComponentInChildren<Animator>();
        Closed = transform.GetComponentInChildren<AudioSource>();
    }
    private void OnDestroy() {
        KeyScript.OnPickedUp -= Open;
    }

    // Update is called once per frame
    private void Open()
    { 
        Debug.Log("The Door can now be opened");
        canOpen = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && canOpen) {
            anim.SetBool("Open", true);
        } else if (other.gameObject.tag == "Player") {
            Closed.Play();
        }
    }
}
