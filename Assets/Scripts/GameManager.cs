using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int notices = 0;
    private int kills = 0;
    private int fails = 0;

    public static GameManager GetManager() {
        return currentManager;
    }

    static GameManager currentManager = null;

    void Awake(){
        if (currentManager == null) {
            currentManager = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyScript.OnKill += AddKill;
        EnemyScript.OnNotice += AddNotice;
    }

    private void OnDestroy() {
                
        EnemyScript.OnKill -= AddKill;
        EnemyScript.OnNotice -= AddNotice;
    }
    public void AddKill () {
        kills++;
        Debug.Log("Kills:"+kills);
    }
    public void AddNotice () {
        notices ++;
        Debug.Log("Notices" + notices);
    }
    public void AddFail() {
        fails++;
        kills = 0;
        Debug.Log("Fails"+fails);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public int GetKills() {
        return kills;
    }
    public int GetNotices() {
        return notices;
    }
    public int GetFails () {
        return fails;
    }
}
