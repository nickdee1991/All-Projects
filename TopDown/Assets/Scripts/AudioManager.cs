using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1.0f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, +randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, +randomPitch / 2f)); ;
        source.Play();
    }
}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    [SerializeField]
    public Sound[] Sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }else {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + Sounds[i].name);
            _go.transform.SetParent(this.transform);
            Sounds[i].SetSource (_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].name == _name)
            {
                Sounds[i].Play();
                return;
            }
        }

        //no sounds with _name found
        Debug.Log("AudioManager: No sound found in list, " + _name);
    }
}
