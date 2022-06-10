using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static_Movable : Collectable
{
    public float timeBetweenChangeSprites = 0.1f;
    public List<Sprite> sprites = new List<Sprite>();
    //public Sprite idleSprite;

    //private float lastShownSpriteTime;
    private int numberOfSprite;

    private void Start() {
        lastShownSpriteTime = Time.time;
    }

    protected override void Update() {
        base.Update();

        if (sprites.Count > 0) {
            ChangeSprite();
        }
    }


    private void ChangeSprite() {
        if (Time.time - lastShownSpriteTime > timeBetweenChangeSprites) {
            if (numberOfSprite >= sprites.Count) {
                numberOfSprite = 0;
            }
            GetComponent<SpriteRenderer>().sprite = sprites[numberOfSprite];
            numberOfSprite++;
            if (sprites.Count == numberOfSprite) {
                numberOfSprite = 0;
            }
            lastShownSpriteTime = Time.time;
        }
    }
}
