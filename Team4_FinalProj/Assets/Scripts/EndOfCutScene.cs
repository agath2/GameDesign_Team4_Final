using UnityEngine;
using UnityEngine.Playables; // Required for Timeline
using UnityEngine.SceneManagement; // Required for scene management

public class TimelineSceneLoader : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector; // Serialized for Inspector
    public string nextSceneName; // Name of the next scene to load

    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        // Subscribe to the Timeline's "stopped" event
        playableDirector.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        // Load the next scene when the Timeline finishes
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        playableDirector.stopped -= OnTimelineFinished;
    }
}
