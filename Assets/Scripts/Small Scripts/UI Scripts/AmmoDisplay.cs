using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    private Text ammo;
    private Image logo;
    private PlayerControls player;



    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        ammo = GetComponent<Text>();
        logo = GetComponentInChildren<Image>();
        ammo.color = Color.black;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.GetHasGun()) {
            ammo.text = player.GetAmmo().ToString();
            logo.enabled = true;
        } else {
            logo.enabled = false;
            ammo.text = null;
        }
    }
}
