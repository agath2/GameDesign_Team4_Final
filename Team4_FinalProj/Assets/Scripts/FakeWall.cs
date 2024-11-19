using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : MonoBehaviour
{
    public GameObject obj;
    private Color alphaColor;
    private float fadeTime = 0.5f;

    public void Start() {
        alphaColor = obj.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }

    public void OnTriggerStay2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            obj.GetComponent<Renderer>().material.color = Color.Lerp(obj.GetComponent<Renderer>().material.color, alphaColor, fadeTime * Time.deltaTime);
        }
    }
}
