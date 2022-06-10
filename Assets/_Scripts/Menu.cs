using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
    private Animator animator;
    public Image mainWeaponSprite;
    public List<Button> buttonsForWeapons = new List<Button>();
    public List<Button> buttonsForExtra = new List<Button>();
    public List<Button> buttonsForTaskItems = new List<Button>();

    [HideInInspector]
    public List<Cell> cellsForWeapon = new List<Cell>();
    [HideInInspector]
    public List<Cell> cellsForExtra = new List<Cell>();
    [HideInInspector]
    public List <Cell> cellsForTaskItems = new List<Cell>();
    private Text coinsAmount;
    private Text damageAmount;

    public List<Cell> GetWeapons() {
        return cellsForWeapon;
    }

    public List<Cell> GetTaskItems() {
        return cellsForTaskItems;
    }

    public List<Cell> GetExtra() {
        return cellsForExtra;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        coinsAmount = GameObject.Find("Coins").GetComponent<Text>();
        if (coinsAmount == null)
            Debug.Log("coinsAmount == null");
        damageAmount = GameObject.Find("DamageAmount").GetComponent<Text>();
        damageAmount.text = GameManager.instance.GetWeaponDamage().ToString();
        for (int i = 0; i < buttonsForWeapons.Count; i++) {
            //if (cellsForWeapon.Count != buttonsForWeapons.Count) {
                cellsForWeapon.Add(new Cell());
                //Debug.Log("cellsForWeapon.Add(new Cell());");
            //}
            cellsForWeapon[i].button = buttonsForWeapons[i];
            //if (cellsForWeapon[i].empty == false) {
                 //cellsForWeapon[i].button.GetComponent<Image>().sprite = Items.items.GetWeaponSprite(cellsForWeapon[i].nameOfObject);
            //}
        }
        for (int i = 0; i < buttonsForExtra.Count; i++) {
            //if (cellsForExtra.Count != buttonsForExtra.Count) {
                cellsForExtra.Add(new Cell());
                //Debug.Log("cellsForWeapon.Add(new Cell());");
            //}
            cellsForExtra[i].button = buttonsForExtra[i];
            //if (cellsForExtra[i].empty == false) {
            //    cellsForExtra[i].button.GetComponent<Image>().sprite = Items.items.GetWeaponSprite(cellsForExtra[i].nameOfObject);
            //}
        }
        for (int i = 0; i < buttonsForTaskItems.Count; i++) {
            cellsForTaskItems.Add(new Cell());
            cellsForTaskItems[i].button = buttonsForTaskItems[i];
        }
        
    }

    public void ChangeDamageAmountInMenu(int damage) {
        damageAmount.text = damage.ToString();
    }

    public void ChangeCoinsInMenu(int coins) {
        coinsAmount.text = coins.ToString();
    }

    public int CheckTheSameCell(string nameOfCell, List<Cell> list) {
        for (int i = 0; i < list.Count; i++) {
            if (list[i].nameOfObject == nameOfCell) {
                return i;
            }
        }
        return -1;
    }

    private void PutInOrder(List<Cell> list) {
        for (int i = 0; i < list.Count; i++) {
            if (list[i].empty == true) {
                for (int j = i + 1; j < list.Count; j++) {
                    if (list[j].empty == false) {
                        list[i].nameOfObject = list[j].nameOfObject;
                        list[j].nameOfObject = null;
                        list[i].button.GetComponent<Image>().sprite = list[j].button.GetComponent<Image>().sprite;
                        list[j].button.GetComponent<Image>().sprite = null;
                        list[i].empty = false;
                        list[j].empty = true;
                        return;
                    }
                }
            }
        }
    }

    public void PressButtonForWeapon(Button button) {
        for (int i = 0; i < cellsForWeapon.Count; i++) {
            if (cellsForWeapon[i].button == button && cellsForWeapon[i].empty == false) {
                Sprite tempSprite = mainWeaponSprite.GetComponent<Image>().sprite;
                string tempName = mainWeaponSprite.GetComponent<Image>().sprite.name;
                //Debug.Log("name of main weapon sprite: " + tempName);
                mainWeaponSprite.GetComponent<Image>().sprite = Items.items.GetWeaponSprite(cellsForWeapon[i].nameOfObject);
                GameManager.instance.ChangeWeapon(cellsForWeapon[i].nameOfObject);
                cellsForWeapon[i].button.GetComponent<Image>().sprite = tempSprite;
                cellsForWeapon[i].nameOfObject = tempName;
                return;
            }
        }
    }

    public void PressButtonForExtra(Button button) {
        for (int i = 0; i < cellsForExtra.Count; i++) {
            if (cellsForExtra[i].button == button && cellsForExtra[i].empty == false) {
                cellsForExtra[i].button.GetComponent<Image>().sprite = null;
                Debug.Log("Name of extra: " + cellsForExtra[i].nameOfObject);
                Extra.instance.ExtraBehavior(cellsForExtra[i].nameOfObject);
                cellsForExtra[i].nameOfObject = null;
                cellsForExtra[i].empty = true;
                return;
            }
        }
    }

    public void ClearCell(Button button) {
        for (int i = 0; i < cellsForWeapon.Count; i++) {
            if (cellsForWeapon[i].button == button) {
                cellsForWeapon[i].button.GetComponent<Image>().sprite = null;
                cellsForWeapon[i].nameOfObject = null;
                cellsForWeapon[i].empty = true;
                return;
            }
        }
    }

    public void ClearCellExtra(Button button) {
        for (int i = 0; i < cellsForExtra.Count; i++) {
            if (cellsForExtra[i].button == button) {
                cellsForExtra[i].button.GetComponent<Image>().sprite = null;
                cellsForExtra[i].nameOfObject = null;
                cellsForExtra[i].empty = true;
                return;
            }
        }
    }

    public bool SetItem(string item) {
        /* int index;
        if (CheckTheSameCell(item, cellsForWeapon) != -1) {
            index = CheckTheSameCell(item, cellsForWeapon);
            cellsForWeapon[index].amount++;
            Debug.Log("Weapon's amount: " + cellsForWeapon[index].amount);
            return true;
        } */
        int index = GetFreeCellIndex(cellsForWeapon);
        
        if (index == -1) 
            return false;
        
        cellsForWeapon[index].nameOfObject = item;
        cellsForWeapon[index].button.GetComponent<Image>().sprite = Items.items.GetWeaponSprite(item);
        cellsForWeapon[index].amount++;
        cellsForWeapon[index].empty = false;
        return true;
    }

    public bool SetItemExtra(string item) {
        int index = GetFreeCellIndex(cellsForExtra);
        if (index == -1)
            return false;
        cellsForExtra[index].nameOfObject = item;
        cellsForExtra[index].button.GetComponent<Image>().sprite = Items.items.GetExtraSprite(item);
        cellsForExtra[index].empty = false;
        return true;
    }

    public bool SetTaskItem(string item) {
        int index = GetFreeCellIndex(cellsForTaskItems);
        if (index == -1)
            return false;
        cellsForTaskItems[index].nameOfObject = item;
        cellsForTaskItems[index].button.GetComponent<Image>().sprite = Items.items.GetExtraSprite(item);
        cellsForTaskItems[index].empty = false;
        return true;
    }

    public int GetFreeCellIndex(List<Cell> list) {
        for (int i = 0; i < list.Count; i++) {
            if (list[i].empty)
                return i;
        }
        return -1;
    }

    /*public int GetFreeCellIndexExtra() {
        for (int i = 0; i < cellsForExtra.Count; i++) {
            if (cellsForExtra[i].empty)
                return i;
        }
        return -1;
    } */

    public Cell GetFreeCell() {
        foreach (Cell cell in cellsForWeapon) {
            if (cell.empty) {
                return cell;
            }
        }
        return null;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            animator.SetTrigger("Show");
            PutInOrder(cellsForWeapon);
            PutInOrder(cellsForExtra);
        }
    }
}
