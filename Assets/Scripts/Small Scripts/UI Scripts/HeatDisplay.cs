using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatDisplay : MonoBehaviour
{
    private Text heat;
    private PlayerControls player;

    private int intHeat;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        heat = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        heat.color = Color.Lerp(Color.black, Color.red, player.GetHeat() / player.GetMaxHeat());

        intHeat = (int)player.GetHeat();
        heat.text = intHeat.ToString();        
    }
}
