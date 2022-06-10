using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {
    
    public float timeBetweenShow = 0.2f;
    
    protected bool jump = false;
    protected float lastShownSpriteTime;
    protected int numberOfSprite = 0;
    protected float timeJumpMin = 0.5f;
    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];

    public  List<Sprite> spritesForIdle = new List<Sprite>();
    public  List<Sprite> spritesForRun = new List<Sprite>();
    public Sprite spriteForJump;

    private void Awake() {
        lastShownSpriteTime = Time.time;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update() {
        
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    public void ChangeSprite(List<Sprite> list) {
        if (Time.time - lastShownSpriteTime > timeBetweenShow) {
            if (numberOfSprite >= list.Count) {
                numberOfSprite = 0;
            }
            GetComponent<SpriteRenderer>().sprite = list[numberOfSprite];
            numberOfSprite++;
            if (list.Count == numberOfSprite) {
                numberOfSprite = 0;
            }
            lastShownSpriteTime = Time.time;
        }
    }

    protected virtual void OnCollide(Collider2D collision) { }

    
    protected bool CheckObstaclesX(float x, float xSpeed) {
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(x * xSpeed, 0), Mathf.Abs(x * xSpeed * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null) {
            return false;
        }
        return true;
    }

    protected bool CheckObstaclesY(float y, float ySpeed) {
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, y * ySpeed), Mathf.Abs(y * ySpeed * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null) {
            return false;
        }
        return true;
    }
}
