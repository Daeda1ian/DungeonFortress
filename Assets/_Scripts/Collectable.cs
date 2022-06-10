using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    protected BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    
    protected Animator animator;
    protected float lastShownSpriteTime;

    private void Awake() {
        if (GetComponent<BoxCollider2D>() != null)
            boxCollider = GetComponent<BoxCollider2D>();
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        lastShownSpriteTime = Time.time;
    }
    protected virtual void Update() {
        if (GetComponent<BoxCollider2D>() != null) {
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i] == null)
                    continue;

                OnCollide(hits[i]);
                hits[i] = null;
            }
        }
    }

    protected virtual void Interaction() { }

    protected virtual void OnCollide(Collider2D col) { }

}
