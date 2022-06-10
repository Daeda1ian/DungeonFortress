using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task_Items_Menu : MonoBehaviour {

    public List<Button> buttonsForTaskItemsMenu = new List<Button>();

    private List<Cell> cellsForTaskItemsMenu = new List<Cell>();
    private Menu menuScript;
    private Animator animator;
    private Door currentDoor;

    void Start() {
        animator = GetComponent<Animator>();
        menuScript = GameObject.Find("Menu").GetComponent<Menu>();
        //Debug.Log("count of cells: " + menuScript.cellsForTaskItems.Count);
        for (int i = 0; i < 4; i++) {
            cellsForTaskItemsMenu.Add(new Cell());
            cellsForTaskItemsMenu[i].button = buttonsForTaskItemsMenu[i];
        }
    }

    public void PressButton(Button button) {
        for (int i = 0; i < cellsForTaskItemsMenu.Count; i++) {
            if (cellsForTaskItemsMenu[i].button == button) {
                currentDoor.GetComponent<Door>().CheckKey(cellsForTaskItemsMenu[i].nameOfObject);
            }
        }
    }

    public void ShowMenu(Door door) {
        animator.SetTrigger("Show");
        currentDoor = door;
    }
    public void UpdateItemsIcons() {
        for (int i = 0; i < cellsForTaskItemsMenu.Count; i++) {
            Debug.Log("update icons");
            cellsForTaskItemsMenu[i].nameOfObject = menuScript.cellsForTaskItems[i].nameOfObject;
            cellsForTaskItemsMenu[i].button.GetComponent<Image>().sprite = menuScript.cellsForTaskItems[i].button.GetComponent<Image>().sprite;
            cellsForTaskItemsMenu[i].empty = menuScript.cellsForTaskItems[i].empty;
        }
    }

}
