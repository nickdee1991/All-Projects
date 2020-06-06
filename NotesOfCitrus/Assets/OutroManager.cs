using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine("OutroTimer");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Credits");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator OutroTimer()
    {
        yield return new WaitForSeconds(7.5f);
        SceneManager.LoadScene("Credits");
    }
}
