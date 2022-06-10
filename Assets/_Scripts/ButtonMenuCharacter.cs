using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMenuCharacter : MonoBehaviour, IPointerClickHandler {

    private Menu menuScript;
    private Button button;
    private void Start() {
        menuScript = GameObject.Find("Menu").GetComponent<Menu>();
        button = GetComponent<Button>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        
        if (eventData.button == PointerEventData.InputButton.Right) {
            //Debug.Log("Right click");
            eventData.Reset();
            if (button.transform.tag == "Weapon")
                menuScript.ClearCell(button);
            if (button.transform.tag == "Extra")
                menuScript.ClearCellExtra(button);
            //Debug.Log("tag of button: " + button.transform.tag);
        }

    }

}
