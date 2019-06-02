using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardRed : MonoBehaviour
{

    public GameObject player;

    private void OnMouseDown()
    {
        player.GetComponent<Player>().hasKeycard = true;
        Destroy(this.gameObject, 1f);
    }
}
