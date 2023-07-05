using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    GameManager gManager;

    [SerializeField]
    private Text killDisplay;
    [SerializeField]
    private Text NoticeDisplay;
    [SerializeField]
    private Text FailDisplay;
    [SerializeField]
    private Text Score;

    private bool canPress = false;
    // Start is called before the first frame update
    private void Start()
    {
        gManager = GameManager.GetManager();

        //Show all player data:
        killDisplay.text = "Kills: "+ gManager.GetKills().ToString() + "/12";
        NoticeDisplay.text = "Times noticed: "+ gManager.GetNotices().ToString();
        FailDisplay.text = "Times failed: "+gManager.GetFails().ToString();

        //Show final score:
        Score.text = SettleScoreTitle(SettleScore(gManager.GetKills(), gManager.GetNotices(), gManager.GetFails()));

        Invoke("SetCanPress", 2);
    }

    private void Update() {
        if (Input.anyKeyDown && canPress) {
            Destroy(gManager);
            SceneManager.LoadScene(0);
        }
    }

    private void SetCanPress () {
        canPress = true;
    }

    private int SettleScore(int kills, int notices, int fails) {
        kills = kills * 2;                          //Each kill is 2 points; 
        notices = Mathf.Clamp(notices, 0, 20);      //Each notices is -1 point;
        fails = Mathf.Clamp (fails, 0, 5) * 5;      //Each fail is - 5 points:

        return kills + notices + fails;
    }

    private string SettleScoreTitle(int score) {
        if (score == 0) {
            Score.color = Color.yellow;
            return "S";    
        } else if (score >= 1 && score <= 24) {
            return "A";
        } else if (score >= 25 && score <= 34) {
            return "B";
        } else if (score >= 35 && score <= 49) {
            return "C";
        }
        return "D";
    }
}
