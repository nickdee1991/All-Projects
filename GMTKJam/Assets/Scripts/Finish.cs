using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private GameManager GM;
    private AudioManager AM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GM.hasFinished = true;
            GM.StartCoroutine("LevelComplete");
            if (SceneManager.GetActiveScene().buildIndex != 4)
            {
                AM.PlaySound("FinishLevel");
            }else{
                AM.PlaySound("FinishGame");
            }
        }
    }
}
