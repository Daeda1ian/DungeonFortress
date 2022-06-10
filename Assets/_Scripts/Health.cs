using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public List<Sprite> heartsSprites = new List<Sprite>();
    public List<GameObject> hearts = new List<GameObject>();
    public GameObject player;

    private int numberOfHearts;

    private void Start() {
        for (int i = 0; i < hearts.Count; i++) {
            hearts[i].GetComponent<Image>().sprite = heartsSprites[0];
        }
    }

    public void ChangeSprite(int health, int maxHealth) {

        if (health > 0) {
            for (int i = 0; i < hearts.Count; i++) {
                hearts[i].GetComponent<Image>().sprite = heartsSprites[2];
            }
            numberOfHearts = health / 10;
            for (int j = 0; j < numberOfHearts; j++) {
                hearts[j].GetComponent<Image>().sprite = heartsSprites[0];
            }
            if (health % 10 != 0) {
                hearts[numberOfHearts].GetComponent<Image>().sprite = heartsSprites[1];
            }
        }
    }
}
