using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishL2 : MonoBehaviour {

    public GameObject atriumSwitch;

    private void OnMouseOver()
    {
        if (atriumSwitch.GetComponent<AtriumSwitch>().atriumSwitchActivated == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("finish");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
