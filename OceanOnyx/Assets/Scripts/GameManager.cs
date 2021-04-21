using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioSource aud;
    public string introSceneName;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.Play();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(introSceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeaveMenu()
    {
        FindObjectOfType<LevelSelectScreen>().LeaveMenu();
    }
}
