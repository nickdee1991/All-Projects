using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine("IntroTimer");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator IntroTimer()
    {
        yield return new WaitForSeconds(23f);
        SceneManager.LoadScene("Level");
    }
}
