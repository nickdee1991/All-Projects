using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishL2 : MonoBehaviour {

    private void OnMouseDown()
    {
        Debug.Log("finish");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
