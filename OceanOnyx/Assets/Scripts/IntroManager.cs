using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject IntroUI;
    public GameObject OptionsUI;
    public GameObject CreditsUI;

    private void Start()
    {
        IntroUI.SetActive(true);
        OptionsUI.SetActive(false);
        CreditsUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        IntroUI.SetActive(false);
        OptionsUI.SetActive(true);
    }

    public void Credits()
    {
        IntroUI.SetActive(false);
        OptionsUI.SetActive(false);
        CreditsUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        IntroUI.SetActive(true);
        OptionsUI.SetActive(false);
        CreditsUI.SetActive(false);
    }
}
