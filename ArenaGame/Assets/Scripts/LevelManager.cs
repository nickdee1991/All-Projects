using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelManager : MonoBehaviour {

    public Camera normalCam1;
    public Camera deathCam1;

    public Camera normalCam2;
    public Camera deathCam2;

    private Rigidbody rbEnemy;

    public Head2collision head2;
    public Head1collision head1;

    public int Player1hit;
    public int Player2hit;

    private void Start()
    {
        normalCam2.enabled = true;
        deathCam2.enabled = false;

        deathCam1.enabled = false;
        normalCam1.enabled = true;

        DontDestroyOnLoad(this);

        head2 = GetComponent<Head2collision>();
        head1 = GetComponent<Head1collision>();

        head2.Player2hit = 0;
        head1.Player1hit = 0;

        if (head2.Player1Win == true)
        {
            normalCam2.enabled = false;
            deathCam2.enabled = true;

            StartCoroutine("WaitThreeSeconds");

            normalCam2.enabled = true;
            deathCam2.enabled = false;
            head2.Player1Win = false;
        }

        if (head1.Player2Win == true)
        {
            normalCam1.enabled = false;
            deathCam1.enabled = true;

            StartCoroutine("WaitThreeSeconds");

            deathCam1.enabled = false;
            normalCam1.enabled = true;
            head1.Player2Win = false;
        }
    }

    IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        print("waited 3 seconds");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void LoadLevel(string name)
	{
        Debug.Log ("Level load requested for: " + name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void QuitRequest(string name)
	{
		Debug.Log ("Level quit requested for: " + name);
		Application.Quit ();
	}
}
