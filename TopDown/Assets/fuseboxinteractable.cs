using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuseboxinteractable : MonoBehaviour {

    public GameObject explosionParticle;
    public GameObject fuseBoxKey;
    private GameObject fireEffect;
    private GameObject player;
    private GameObject serverTerminal;
    private GameObject L3elevator;

    public Transform explosionParticleSpawn;
    public AudioSource AudOpen;
    public AudioSource AudExplosion;
    public AudioSource AudAlarm;
    private Animator anim;
    public Animator serverCabinetExplosion;

    private bool doorOpened;
    public bool fuseBoxDestroyed;
    public bool finaldoorsopen;
    public bool fuseKeyaquired;



    private void Start()
    {
        L3elevator = GameObject.Find("L3elevator");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        fireEffect = GameObject.FindGameObjectWithTag("fireEffect");
        serverTerminal = GameObject.Find("monitor (serverterminal)");
        fireEffect.SetActive(false);
        fuseBoxDestroyed = false;
        doorOpened = false;
    }

    private void OnMouseOver()
    {
        if (doorOpened == false && fuseKeyaquired == true && Input.GetKeyDown(KeyCode.E))
        {
            AudOpen.Play();
            anim.SetBool("fusedooropen", true);
            doorOpened = true;
        }
        if (fuseBoxDestroyed == true && L3elevator.GetComponent<FinishL3>().dataRetreived == true && doorOpened == true && Input.GetKeyDown(KeyCode.E))
        {
            AudExplosion.Play();
            AudAlarm.Play();
            explosionParticleSpawn = Instantiate(explosionParticle.transform, explosionParticleSpawn.transform.position, Quaternion.identity);
            Debug.Log("FuseBoxDestroyed");
            finaldoorsopen = true;
            fireEffect.SetActive(true);
            serverCabinetExplosion.SetBool("serverroomdestroyed", true);
        }
    }
}
