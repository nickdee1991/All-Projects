using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public bool DEBUGMODE;

    private GameObject player;

    public Transform[] spawnPoints;
    public Transform startSpawn;

    private AudioManager Aud;

    public bool isGameOver;

    private Animator anim;

    public int timesCaptured = 0;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //StartCoroutine("UpdateMesh");

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GameObject.Find("Main Camera").GetComponent<Animator>();
        Aud = FindObjectOfType<AudioManager>();


        isGameOver = false;
    }

    private void Update()
    {
        if (timesCaptured >= 3 && !isGameOver)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Captured()
    {
        if (timesCaptured < 3 && !isGameOver)
        {
            Debug.Log("Captured " + timesCaptured);
            StartCoroutine("CapturedTimer");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over " + timesCaptured);
        isGameOver = true;
        StartCoroutine("GameOverTimer");
    }

    IEnumerator CapturedTimer()
    {
        //anim.SetBool("CapturedTransition", true);
        player.GetComponent<PlayerSimpleMovement>().movementSpeed = 0;
        Aud.PlaySound("CapturedVoice");
        Aud.PlaySound("Violin");
        yield return new WaitForSeconds(3.5f);
        Aud.StopSound("Violin");
        timesCaptured++;
        anim.SetBool("CapturedTransition", false);
        player.transform.position = startSpawn.transform.position;
        player.transform.rotation = startSpawn.transform.rotation;
        player.GetComponent<PlayerSimpleMovement>().movementSpeed = 3;
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerCaught>().Captured = false;
    }

    IEnumerator GameOverTimer()
    {
        anim.SetBool("GameOverTransition", true);
        Aud.PlaySound("GameOverVoice");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }

    public void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
