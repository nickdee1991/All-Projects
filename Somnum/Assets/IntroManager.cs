using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    private bool canSkip;
    public float canSkipTime;

    private void Start()
    {
        StartCoroutine("IntroTimer");
        canSkip = false;
    }

    IEnumerator IntroTimer()
    {
        yield return new WaitForSeconds(canSkipTime);
        canSkip = true;
        text.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkip == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
