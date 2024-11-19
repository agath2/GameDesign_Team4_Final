using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSystemLogic : MonoBehaviour
{
    public GameObject TrafficLightRed;
    public GameObject TrafficLightGreen;
    public GameObject Car;

    private float timer;
    private bool isRedLight = true;
    private float redLightDuration = 6f;
    private float greenLightDuration = 3f;

    private float scaleTimer;
    private int carLoop = 3;

    private Vector3 originalScale;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        isRedLight = true; 
        timer = redLightDuration;
        originalScale = Car.transform.localScale; 
        originalColor = Car.GetComponent<Renderer>().material.color; 
        UpdateStoplight();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isRedLight = !isRedLight;
            timer = isRedLight ? redLightDuration : greenLightDuration;
            UpdateStoplight();
        }

        if (isRedLight) MoveCar();
    }

    private void UpdateStoplight()
    {
        TrafficLightRed.SetActive(isRedLight);
        TrafficLightGreen.SetActive(!isRedLight);
        Car.SetActive(isRedLight);
    }

    private void MoveCar(){
        scaleTimer += Time.deltaTime / redLightDuration * carLoop;

        if (scaleTimer > 1 || !isRedLight) scaleTimer = 0;

        float scaleLerp = Mathf.Lerp(1.0f, 4.0f, scaleTimer); 

        Car.GetComponent<BoxCollider2D>().enabled = scaleLerp > 3.5;

        Car.transform.localScale = new Vector3(scaleLerp, scaleLerp, 1.0f);

        float alphaLerp = Mathf.Lerp(0f, 1f, scaleTimer); 
        Color carColor = originalColor;
        carColor.a = alphaLerp; 
        Car.GetComponent<Renderer>().material.color = carColor;
    }

}
