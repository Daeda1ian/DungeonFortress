using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable_Items : Collectable {

    public float interactionPosition = 0.3f;
    protected override void Update() {
        base.Update();
        Interaction();
    }

    protected override void Interaction() {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < interactionPosition) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (transform.tag == "Weapon") {
                    if (GameManager.instance.GetWeapon(GetComponent<SpriteRenderer>().sprite.name))
                        Destroy(gameObject);
                }
                if (transform.tag == "Extra") {
                    //Debug.Log("Extra's sprite's name: " + GetComponent<SpriteRenderer>().sprite.name);
                    if (GameManager.instance.GetExtra(GetComponent<SpriteRenderer>().sprite.name))
                        Destroy(gameObject);
                }
                if (transform.tag == "Task_Item") {
                    if (GameManager.instance.GetTaskItem(GetComponent<SpriteRenderer>().sprite.name))
                        Destroy(gameObject);
                }
            }
        }
    }
}
