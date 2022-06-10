using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText {

    public GameObject textPrefab;
    public GameObject go;
    public bool active;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    private void Update() {
        UpdateFloatingText();
    }

    /*public void CreateTextMessage(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        text.text = msg;
        text.fontSize = fontSize;
        text.color = color;
        go.transform.position = position;
        this.motion = motion;
        this.duration = duration;
    } */
    public void Show() {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }

    public void Hide() {
        active = false;
        go.SetActive(false);
    }

    public void UpdateFloatingText() {
        if (!active)
            return;
        if (Time.time - lastShown > duration) {
            Hide();
        }
        go.transform.position += motion * Time.deltaTime;
    }

}
