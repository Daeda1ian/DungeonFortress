using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movable {
    
    public float xSpeed = 1;
    public float ySpeed = 1;
    public float gravity = 0.2f;
    
    public GameObject weapon;

    [HideInInspector]
    public Vector3 weaponPosition;
    private float yPosition;
    private float timeJump;

    private void Start() {
        weaponPosition = weapon.transform.localPosition;
        //Debug.Log("main weapon: " + GameManager.instance.GetMainWeaponName());
        //Debug.Log("player's current weapon: " + weapon.GetComponent<SpriteRenderer>().sprite.name);
        if (GameManager.instance.GetMainWeaponName() != weapon.GetComponent<SpriteRenderer>().sprite.name) {
            GameManager.instance.ChangeWeapon(GameManager.instance.GetMainWeaponName());
        } 
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && jump == false) {
            Jump(Vector2.up);
        }
        if (jump == true && Time.time - timeJump >= timeJumpMin) {
            if (transform.position.y <= yPosition) {
                jump = false;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x == 0 && y == 0) {
            if (jump == false)
                ChangeSprite(spritesForIdle);
        }

        if (x < 0) {
            if (jump == false)
                ChangeSprite(spritesForRun);
            transform.localScale = new Vector3(-1, 1, 0);
        }
        else if (x > 0) {
            if (jump == false)
                ChangeSprite(spritesForRun);
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if (x == 0 && y != 0) {
            if (jump == false)
                ChangeSprite(spritesForRun);
        }

        Vector3 pos = transform.position;
        pos.x += x * xSpeed * Time.deltaTime;
        pos.y += y * ySpeed * Time.deltaTime;

        if (CheckObstaclesX(x, xSpeed) || jump == true) {
            pos.x = transform.position.x;
        }
        if (CheckObstaclesY(y, ySpeed) || jump == true) {
            pos.y = transform.position.y;
        }
        
        transform.position = pos;
    }

    public void Jump(Vector2 v) {
        jump = true;
        timeJump = Time.time;
        yPosition = transform.position.y;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        rb.AddForce(v * 0.75f, ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().sprite = spriteForJump;
    }

    protected override void OnCollide(Collider2D collision) {
        
        if (collision.transform.tag == "Enemy" && jump == false) {
            HitPlayer(collision);
        }
        if (collision.transform.tag == "Heart") {
            GameManager.instance.ChangeHealth(10);
            Destroy(collision.gameObject);
        }
    }

    private void HitPlayer(Collider2D collision) {
        Transform transform = collision.transform;
        if (transform.localScale.x > 0) {
            Jump(new Vector2(0.5f, 1f));
        }
        else if (transform.localScale.x < 0) {
            Jump(new Vector2(-0.5f, 1f));
        }
        GameManager.instance.ChangeHealth(-5);
    }

    
}
