using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timerText == null)
        {
            timerText = GameObject.Find("timer").GetComponent<Text>();
        }

        timerText.text = timer.ToString();
        timer = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.T))
        {
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
