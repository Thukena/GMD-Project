using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        [Range(0f, 1f)]
        public float volume = 0.5f;
        [HideInInspector]
        public AudioSource source;
    }

    public List<Sound> sounds;
    private Dictionary<int, AudioSource> activeSounds = new ();
    private int currentId = 0;

    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
        Play("GamePlayMusic");
    }

    public int Play(string name)
    {
        currentId++;
        var sound = sounds.Find(sound => sound.name == name);
        sound.source.Play();

        if (sound.source.loop)
        {
            activeSounds[currentId] = sound.source;
        }
        
        return currentId;
    }
    
    public void Stop(int id)
    {
        if (!activeSounds.ContainsKey(id)) return;
        activeSounds[id].Stop();
        activeSounds.Remove(id);
    }
}