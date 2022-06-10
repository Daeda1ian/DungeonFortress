using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public static Items items;
    public List<GameObject> weapons = new List<GameObject>();
    public List<Sprite> weaponsSprites = new List<Sprite>();
    public List<Sprite> extraSprites = new List<Sprite>();

    private void Awake() {
        if (Items.items != null) {
            Destroy(gameObject);
            return;
        }
        items = this;
    }

    public Sprite GetExtraSprite(string name) {
        foreach (Sprite item in extraSprites) {
            if (item.name == name)
                return item;
        }
        return null;
    }
    public GameObject GetWeaponItem(string name) {
        foreach (GameObject item in weapons) {
            if (item.name == name) {
                return item;
            }
        }
        return null;
    }

    public Sprite GetWeaponSprite(string name) {
        foreach (Sprite item in weaponsSprites) {
            if (item.name == name) {
                return item;
            }
        }
        return null;
    }

}
