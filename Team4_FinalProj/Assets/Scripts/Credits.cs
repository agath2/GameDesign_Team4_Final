using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{

    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReturnToMenu());
        int finalTotalCollected = CoinManager.instance.GetFinalTotalCollected();
        coinText.text = "You collected " + finalTotalCollected.ToString() + "/30 coins";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReturnToMenu(){
        yield return new WaitForSeconds(37f);
        SceneManager.LoadScene("StartScene");
    }
}
