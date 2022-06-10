using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra : MonoBehaviour {

    public static Extra instance;

    private void Awake() {
        if (Extra.instance != null) {
            Destroy(gameObject);
            return;
        }
        Extra.instance = this;
    }
    public void ExtraBehavior(string nameOfExtra) {
        if (nameOfExtra == "green_potion_big") {
            GameManager.instance.ChangeHealth(10);
        }
        if (nameOfExtra == "green_potion_small") {
            GameManager.instance.ChangeHealth(5);
        }
    }

}
