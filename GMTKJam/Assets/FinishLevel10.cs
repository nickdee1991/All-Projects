using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel10 : MonoBehaviour
{
    private GameManager GM;
    private AudioManager AM;
    public Animator EndScene;
    public float endTimer;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 10)
            {
                AM.PlaySound("EndSong");
                EndScene.SetBool("EndScene", true);
                StartCoroutine("EndTimer");
            }
        }
    }

    IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(endTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
