using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collectable {

    public string sceneForLoad;

    protected override void OnCollide(Collider2D col) {
        if (col.transform.tag == "Player") {
            //GameManager.instance.SaveState();
            GameManager.instance.Save();
            SceneManager.LoadScene(sceneForLoad);
        }
    }

}
