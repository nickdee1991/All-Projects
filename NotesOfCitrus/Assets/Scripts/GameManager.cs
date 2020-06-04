using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEditor.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    public Transform StartSpawn;
    private AudioManager Aud;

    public MeshRenderer key;
    public MeshRenderer hammer;
    public MeshRenderer chisel;

    public bool EndKey;
    public bool Hammer;
    public bool Chisel;

    public bool isGameOver;

    private Animator anim;

    public int timesCaptured = 0;

    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.AI.NavMeshBuilder.BuildNavMeshData();

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GameObject.Find("Main Camera").GetComponent<Animator>();
        Aud = FindObjectOfType<AudioManager>();

        EndKey = false;
        Hammer = false;
        Chisel = false;

        isGameOver = false;
    }

    private void Update()
    {
        if (timesCaptured >= 3 && !isGameOver)
        {
            GameOver();
        }

        #region checks for player items
        if (EndKey)
        {
            key.enabled = true;
        }else{
            key.enabled = false;
        }
        if (Hammer)
        {
            hammer.enabled = true;
        }else{
            hammer.enabled = false;
        }
        if (Chisel)
        {
            chisel.enabled = true;
        }else{
            chisel.enabled = false;
        }
        #endregion

    }

    public void Captured()
    {
        Debug.Log("Captured " + timesCaptured);
        timesCaptured++;
        StartCoroutine("CapturedTimer");
    }

    public void GameOver()
    {
        Debug.Log("Game Over " + timesCaptured);
        isGameOver = true;
        StartCoroutine("GameOverTimer");
    }

    IEnumerator CapturedTimer()
    {
        anim.SetBool("CapturedTransition", true);
        player.GetComponent<PlayerSimpleMovement>().movementSpeed = 0;
        Aud.PlaySound("CapturedVoice");
        yield return new WaitForSeconds(3.5f);
        EndKey = false;
        Hammer = false;
        Chisel = false;
        anim.SetBool("CapturedTransition", false);
        player.transform.position = StartSpawn.transform.position;
        player.transform.rotation = StartSpawn.transform.rotation;
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
}
