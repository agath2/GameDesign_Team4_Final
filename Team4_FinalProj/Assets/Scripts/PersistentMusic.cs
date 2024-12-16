using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    private string sceneName = "";
    public AudioSource musicScene1;
    public AudioSource musicScene2;
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

    void Update(){
        if (SceneManager.GetActiveScene().name == sceneName) return;
        sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName == "DogNaming") {
            musicScene1.Stop();
        } else if (sceneName == "Lvl3") {
            musicScene1.Play();
        } else if (sceneName == "Lvl4"){
            musicScene1.Stop();
            musicScene2.Play();
        } else if (sceneName == "EndScene") {
            Destroy(gameObject);
        }

    }
}