using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable{

    public float timeBetweenShow = 0.2f;
    public List<Sprite> spritesForOpening = new List<Sprite>();
    public int coins;
    public float interactionPosition = 0.3f;
    public Sprite empty;

    private int numberOfSprite = 0;
    
    private bool getOpen = false;
    private bool opened = false;
    private bool animationPlayed = false;

    protected override void Update() {
        base.Update();

        if (getOpen) {
            ChangeSprite();
            if (!animationPlayed) {
                animator.SetTrigger("Jump");
                
                animationPlayed = true;
            }
        }
        Interaction();
        if (opened) {
            GetComponent<SpriteRenderer>().sprite = empty;
        }
    }

    protected override void Interaction() {
        if (opened)
            return;
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < interactionPosition && animationPlayed) {
            if (Input.GetKeyDown(KeyCode.E)) {
                GameManager.instance.ShowText("+" + coins + " coins!", 20, Color.white, transform.position, Vector3.up * 50, 2.0f);
                GameManager.instance.ChangeCoins(coins);
                GetComponent<SpriteRenderer>().sprite = empty;
                opened = true;
            }
        }
    }

    private void ChangeSprite() {
        if (Time.time - lastShownSpriteTime > timeBetweenShow) {
            if (numberOfSprite >= spritesForOpening.Count) {
                getOpen = false;
                return;
            }
            GetComponent<SpriteRenderer>().sprite = spritesForOpening[numberOfSprite];
            numberOfSprite++;
            lastShownSpriteTime = Time.time;
        }
    }

    protected override void OnCollide(Collider2D col) {
        if (col.transform.tag == "Player") {
            getOpen = true;
        }
    }

}
