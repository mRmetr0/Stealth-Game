using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(transform.gameObject, 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) {
            Destroy(transform.gameObject);
        }
    }
}
