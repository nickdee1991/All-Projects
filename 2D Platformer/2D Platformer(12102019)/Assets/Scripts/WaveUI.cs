using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{

    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

    // Use this for initialization
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }

        if (waveCountdownText == null)
        {
            Debug.LogError("No CountdownText referenced");
            this.enabled = false;
        }

        if (waveCountText == null)
        {
            Debug.LogError("No waveCountText referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            // When spawner is counting down, play the countdown animation
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;

            // When enemies are spawning, play the wave incoming animation
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.State;
    }
    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
            //Debug.Log("COUNTING");
        }

        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
       
    }

    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true); 

            waveCountText.text = spawner.NextWave.ToString();

            Debug.Log("SPAWNING");
        }
       
    }

}
