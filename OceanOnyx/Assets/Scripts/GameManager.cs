using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _posTemp;

    private void Awake()
    {
        _posTemp = GameObject.Find("PositionTemplate");
        _posTemp.SetActive(false);
    }
}
