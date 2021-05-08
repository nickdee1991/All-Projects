using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    private GameObject player;
    public Transform StartSpawn;
    private AudioManager Aud;

    public MeshRenderer keyHand;
    public MeshRenderer hammerHand;
    public MeshRenderer chiselHand;

    public MeshRenderer keyObj;
    public MeshRenderer hammerObj;
    public MeshRenderer chiselObj;

    public bool EndKey;
    public bool Hammer;
    public bool Chisel;

    public bool isGameOver;

    private Animator anim;

    public int timesCaptured = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //update the mesh
        StartCoroutine("UpdateMesh");

        keyObj = GameObject.Find("ObjKey").GetComponent<MeshRenderer>();
        hammerObj = GameObject.Find("ObjHammer").GetComponent<MeshRenderer>();
        chiselObj = GameObject.Find("ObjChisel").GetComponent<MeshRenderer>();

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
            keyHand.enabled = true;
        }else{
            keyHand.enabled = false;
            keyObj.enabled = true;
        }
        if (Hammer)
        {
            hammerHand.enabled = true;
        }else{
            hammerHand.enabled = false;
            hammerObj.enabled = true;
        }
        if (Chisel)
        {
            chiselHand.enabled = true;
        }else{
            chiselHand.enabled = false;
            chiselObj.enabled = true;
        }
        #endregion

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
        anim.SetBool("CapturedTransition", true);
        player.GetComponent<PlayerSimpleMovement>().movementSpeed = 0;
        Aud.PlaySound("CapturedVoice");
        Aud.PlaySound("Violin");
        yield return new WaitForSeconds(3.5f);
        Aud.StopSound("Violin");
        timesCaptured++;
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
