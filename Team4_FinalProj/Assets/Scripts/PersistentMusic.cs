using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    private string sceneName;
    public AudioSource musicScene1;
    public AudioSource musicScene2;
    public AudioSource musicEndScene;
    //public AudioSource menuMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start(){
        sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName == "Lvl4"){
            musicScene1.Stop();
            musicScene2.Play();
        }
        else if (sceneName == "EndScene") {
            musicScene2.Stop();
            musicEndScene.Play();
        }
        else if (sceneName == "StartScene"){
            Destroy(gameObject);
        }

    }
}