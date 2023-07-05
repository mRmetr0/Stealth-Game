using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControls : MonoBehaviour
{
    //Movement elements:
    [SerializeField]
    private float walkSpeed = 7;    
    [SerializeField]
    private float runSpeed = 10;
    private float HorizontalSpeed;
    private float VerticalSpeed;
    private Vector3 move;

    //HUD elements
    [SerializeField]
    private GameObject bullet;
    [Range (0.1f, 100f)][SerializeField]
    private float heatBuild = 10f;
    [Range (0, 100f)][SerializeField][Tooltip("Should not be larger then heatBuild")]
    private float heatDecrease = 7f; 
    private float heat = 0;
    [SerializeField][Range(1, 1000)]
    private float maxHeat = 100;
    private int ammo = 0;
    
    private bool hasGun = false;
    private bool alive = true;

    private Rigidbody rb;
    private Animator pAnimator;
    private AudioSource shot;
    [SerializeField]
    private bool GodMode = false;

    private void Awake() {
        if (GodMode) {
            hasGun = true;
            ammo = 100;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        pAnimator = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        shot = transform.GetComponent<AudioSource>();

        EnemyScript.OnHeat += BuildHeat;

    }


    private void OnDestroy() {
        EnemyScript.OnHeat -= BuildHeat;
    }

    // Update is called once per frame
    private void Update()
    {      
            HorizontalSpeed = Input.GetAxis("Horizontal");
            VerticalSpeed = Input.GetAxis("Vertical");

            move = new Vector3(HorizontalSpeed, 0, VerticalSpeed);
            pAnimator.SetBool("Walking", move.magnitude > 0.5f);

            Shoot();

            //Handle heat:
            heat-= heatDecrease * Time.deltaTime;
            heat = Mathf.Clamp(heat, 0, maxHeat);
    }

    private void FixedUpdate()
    {
        if (alive) {
            rb.AddForce(move.normalized * SetSpeed() * Time.deltaTime, ForceMode.Force);
            RotatePlayer();
        }

    }

    //Lets the player run when holding down l>shift:
    private float SetSpeed() {
        if (Input.GetKey(KeyCode.LeftShift)) 
            return runSpeed;
        return walkSpeed;
    }

    //When the player moves around, it will automatically rotate towards them:
    private void RotatePlayer() {
        if (move.magnitude>0) {
            rb.rotation = Quaternion.LookRotation(move);
        }
    }

    //PickUp Interactions:
    private void OnTriggerEnter(Collider other)
    {        
        //To pick up key:
        if (other.CompareTag("PickUp")) {
            Destroy(other.gameObject);
        //To pick up ammo:
        } else if (other.CompareTag("Ammo")) {
            if (hasGun) {
                ammo += 5;
                Destroy(other.gameObject);
            }
        //To pick up Gun:
        } else if (other.CompareTag("Gun")) {
            ammo += 5;
            hasGun = true;
            Destroy(other.gameObject);
        }
    }

    private void Shoot () {
        if (Input.GetKeyDown(KeyCode.Space) && hasGun && ammo > 0 && alive) {
            shot.Play();
            ammo--;
            Instantiate(bullet, rb.position + new Vector3(0, 0.5f,0), transform.rotation);   
        }
    }
    //public methods for other scripts to work with the playerscript:
    public float GetHeat() {
        return heat;
    }
    public int GetAmmo() {
        return ammo;

    }
    public bool GetHasGun() {
        return hasGun;
    }
    public float GetMaxHeat()  {
        return maxHeat;
    }

    //Methods that are connected to other scripts (through subscription or their public methods):
    private void BuildHeat() {
        heat+= heatBuild * Time.deltaTime;
        heat = Mathf.Clamp(heat, 0, maxHeat);
        if (heat > maxHeat -1 && alive) {
            alive = false;
            pAnimator.SetBool("Died", true);
            Invoke("Failed", 2.0f);
        }
    }
    private void Failed () {
        GameManager.GetManager().AddFail();
    }
}
