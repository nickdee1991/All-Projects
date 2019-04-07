using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Camera normalCam1;
    public Camera deathCam1;

    public Camera normalCam2;
    public Camera deathCam2;

    public Head2collision head2;
    public Head1collision head1;

    public GameObject Player2head;
    public GameObject Player1head;

    public GameObject playerWin1;
    public GameObject playerWin2;

    public bool player1Win = false;
    public bool player2Win = false;

    static int levelRound;

    //static LevelManager instance = null;

    void Awake()
    {
        //    //handy code for checking music manager between scenes and avoiding duplication 
        //    //this is called a singleton (must reuse)
        //    if (instance != null)
        //    {
        //        Destroy(gameObject);
        //        print("Duplicate Scene Manager found, commiting suicide");
        //    }
        //    else
        //    {
        //        instance = this;
        //        GameObject.DontDestroyOnLoad(gameObject);
        //    }
    }

    private void Start()
    {
        head2 = Player2head.GetComponent<Head2collision>();        
        head1 = Player1head.GetComponent<Head1collision>();

        if (levelRound == 3)
        {
            //SceneManager.LoadScene("StartMenu");
            levelRound = 0;
        }
    }

    IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        player1Win = false;
        player2Win = false;
        print("waited 3 seconds");
        //SceneManager.LoadScene("Woorld");
    }   

    private void Update()
    {        

        if (player1Win == true)
        {
            Debug.Log("Player1Win");
            playerWin1.SetActive(true);
            normalCam2.enabled = false;
            normalCam1.enabled = false;
            deathCam2.enabled = true;
            player1Win = false;
            player2Win = false;
            levelRound++;
            Debug.Log("Round " + levelRound);

            StartCoroutine("WaitThreeSeconds");
        }

        if (player2Win == true)
        {
            playerWin2.SetActive(true);
            normalCam2.enabled = false;
            normalCam1.enabled = false;
            deathCam1.enabled = true;
            player1Win = false;
            player2Win = false;
            levelRound++;
            Debug.Log("Round " + levelRound);

            StartCoroutine("WaitThreeSeconds");
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitRequest(string name)
    {
        Debug.Log("Level quit requested for: " + name);
        Application.Quit();
    }
}
