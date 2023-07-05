using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string title = "Start";
    [SerializeField]
    private Text titleDisplay;
    // Start is called before the first frame update
    private void Awake()
    {
        titleDisplay.text = title;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void OnButtonPress() {
        SceneManager.LoadScene("Level1");
    }
}
