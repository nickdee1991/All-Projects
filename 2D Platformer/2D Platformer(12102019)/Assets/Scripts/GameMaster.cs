using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GameMaster : MonoBehaviour
    {

    public static GameMaster gm;
    private GameObject player;
    private GameObject cam;
    private Camera2DFollow camFollow;

    [SerializeField]
    private int maxLives = 3;

    private static int _remainingLives;
    public static int RemainingLives
    {
         get { return _remainingLives;  }
    }

    private void Awake()
    {
        //cam = GameObject.FindGameObjectWithTag("Camera");
        //camFollow = cam.GetComponent<Camera2DFollow>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public float spawnDelay = 2;
    public bool PlayerDead;
    public GameObject UIoverlay;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public Transform spawnPrefab;
    public string spawnSoundName = "Spawn";
    public string respawnCountdownSoundName = "RespawnCountdown";
    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    //cache
    private AudioManager audioManager;

    void Start()
    {
        PlayerDead = false;
        UIoverlay = GameObject.Find("UIOverlay");
        UIoverlay.GetComponent<Canvas>().enabled = true;

        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }

        {
            _remainingLives = maxLives;
        }
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Freak Out! No audiomanager found in scene");
        }
    }

    public void EndGame()
    {

        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("GAME OVER, BITCH!");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {

        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay); 

        audioManager.PlaySound(spawnSoundName);       
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer (Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.PlayerDead = true;
            gm.EndGame();
        } else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }

        
    }


    public static void KillEnemy (Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        _enemy.GetComponent<EnemyAI>().target = null;

        // playing sounds
        audioManager.PlaySound(_enemy.deathSoundName);

        // add particles
        Transform _clone = Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone, 3f);

        // go camerashake
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        //destroy enemy object
        Destroy(_enemy.gameObject, 2f);
    }
}
