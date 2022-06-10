using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float interactionPosition = 0.2f;
    public string nameOfKey;

    private GameObject itemsMenu;
    private Task_Items_Menu itemsMenuScript;

    private void Start() {
        itemsMenu = GameObject.Find("Task_Item_Menu");
        itemsMenuScript = itemsMenu.GetComponent<Task_Items_Menu>();
    }

    public void CheckKey(string name) {
        if (nameOfKey == name) {
            Destroy(gameObject);
            itemsMenuScript.ShowMenu(this);
        }
    }

    private void Update() {
        Interaction();
    }

    private void Interaction() {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < interactionPosition) {
            if (Input.GetKeyDown(KeyCode.E)) {
                itemsMenuScript.ShowMenu(this);
                itemsMenuScript.UpdateItemsIcons();
            } 
        } 
    }

}
