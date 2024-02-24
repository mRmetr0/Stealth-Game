using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 40;
    private Quaternion rotAngle;
    [SerializeField]
    private float viewDist;
    [SerializeField]
    private Transform ItemDrop;
    [Range(0,100)] [SerializeField]
    private int DropRate = 50;
    [Range(1,5)] [SerializeField]
    private int health = 2;
    
    bool alive = true;
    bool confused = false;

    private AudioSource bleep;
    private Transform player;
    private Animator anim;
    private Light lSource;

    public static Action OnNotice;
    public static Action OnKill;
    public static Action OnHeat;

    private void Awake()
    {
        //LOL
        if (viewDist <= 0) {
            viewDist = Mathf.Infinity;
        }
        if (rotSpeed > 0) {
            transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
        } else {
            rotAngle = transform.rotation;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        bleep = transform.GetComponent<AudioSource>();
        anim = transform.GetComponentInChildren<Animator>();
        lSource = transform.GetComponentInChildren<Light>();
    }

    private void Update() {
        if (alive) {
            LookAround();
            BuildHeat();
        }
    }

    private void LookAround() {
        RaycastHit hitPoint;
        Ray ray;
        if (confused) {    
            //Debug.Log("Confused");
            return;
        } else if (player == null) {

            if (rotSpeed > 0) {
                transform.Rotate(new Vector3(0, rotSpeed* Time.deltaTime, 0));  
            } else {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotAngle, 100f * Time.deltaTime);
            }

            ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hitPoint, viewDist)) {
                if (hitPoint.collider.CompareTag("Player")) {
                    Debug.Log("Player detected");
                    player = hitPoint.transform;
                    bleep.Play();
                    OnNotice?.Invoke();
                }
            }
        } else {
            transform.LookAt(player.position);

            Vector3 toPlayer = player.position - transform.position;
            ray = new Ray(transform.position, toPlayer);
            if (!Physics.Raycast(ray, out hitPoint, viewDist) || hitPoint.collider.tag != ("Player")) {
                Debug.Log("Player lost");
                player = null;
                confused = true;
                lSource.enabled = false;
                Invoke("SetConfused", 1);
            }
        }
    }

    private void SetConfused() {
        lSource.enabled = true;
        confused = false;
    }

    private void BuildHeat() {
        if (player != null) {
            OnHeat?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")){
            Destroy(other.gameObject);
            if (health <=1 && alive) {
                Killed();
            }
            health--;
        }
        
    }
    
    private void Killed () {
            Collider c = transform.GetComponent<Collider>();
            c.isTrigger = true;
            player = null;

            //Destroy(transform.gameObject);
            anim.SetBool("Died", true);

            // Registers kill to game manager:
            OnKill?.Invoke();

            if (UnityEngine.Random.Range(0, 100) <= DropRate && ItemDrop != null) {
                Instantiate(ItemDrop, transform.position, transform.rotation);
            };

            alive = false;
    }
}
