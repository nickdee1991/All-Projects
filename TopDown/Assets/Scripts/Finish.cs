using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    public GameObject drainLidComputer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && drainLidComputer.GetComponent<DrainTerminal>().drainLidOpened == true)
        {
            Debug.Log("finish");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
