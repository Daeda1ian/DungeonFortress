using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Static_Movable {

    public int coins;

    protected override void OnCollide(Collider2D col) {
        if (col.transform.tag == "Player") {
            GameManager.instance.ShowText("+" + coins + " coins!", 20, Color.white, transform.position, Vector3.up * 50, 2.0f);
            GameManager.instance.ChangeCoins(coins);
            Destroy(gameObject);
        }
    }
}
