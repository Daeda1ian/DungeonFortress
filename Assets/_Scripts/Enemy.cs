using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movable {

    public int health = 3;
    public float speed;
    public float positionOfPatrolX;
    public float positionOfPatrolY;
    public Transform pointOfPatrol;
    public float stoppingDistance;
    public float kickDistance;
    //public float stopTime = 0.3f;

    private Animator animator;
    //private Animator currentAnimation;
    private Transform playerPosition;
    private float lastStop = 0f;
    private bool isAlive;
    private bool movingRight;

    bool chill = false;
    bool angry = false;
    bool goBack = false;

    private void Start() {
        isAlive = true;
        animator = GetComponent<Animator>();
        
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected override void Update() {
        base.Update();

        if (isAlive) {
            ChangeSprite(spritesForRun);

            //float distanceBetweenPlayerAndEnemy = Vector2.Distance(transform.position, pointOfPatrol.position);
            if (Vector2.Distance(transform.position, pointOfPatrol.position) < positionOfPatrolX && !angry) {
                chill = true;
                goBack = false;
            }
            if (Vector2.Distance(transform.position, playerPosition.position) < stoppingDistance) {
                angry = true;
                chill = false;
                goBack = false;
            }
            if (Vector2.Distance(transform.position, playerPosition.position) > stoppingDistance) {
                goBack = true;
                //chill = false;
                angry = false;
            }

            if (chill) {
                Chill();
            }
            else if (angry) {
                Angry();
            }
            else if (goBack) {
                GoBack();
            }
        }

        /*Vector3 pos = transform.position;
        if (playerPosition.position.x > pos.x) {
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (playerPosition.position.x < pos.x) {
            transform.localScale = new Vector3(-1, 1, 0);
        }*/
    }

    public bool AnimationPlaying(string animationName) {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        
        if (info.IsName(animationName)) {
            //Debug.Log("is kicking");
            return true;
        }
        return false;
    }

    protected void Chill() {
        if (transform.position.x > pointOfPatrol.position.x + positionOfPatrolX) {
            movingRight = false;
        }
        else if (transform.position.x < pointOfPatrol.position.x - positionOfPatrolX) {
            movingRight = true;
        }
        Vector2 pos = transform.position;
        if (movingRight) {
            pos.x = transform.position.x + speed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if (!movingRight) {
            pos.x = transform.position.x - speed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 0);
        }
        transform.position = pos;
    }

    protected void Angry() {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        Vector3 pos = transform.position;

        if (Random.Range(0, 1000) > 995 && Vector2.Distance(transform.position, playerPosition.position) < kickDistance) {
            if (transform.localScale.x > 0)
                animator.SetTrigger("Kick");
            if (transform.localScale.x <= 0)
                animator.SetTrigger("Kick_Mirror");
        }
        if (AnimationPlaying("Enemy_White_Kick") || AnimationPlaying("Enemy_White_Kick_Mirror"))
            return;
        if (playerPosition.position.x > pos.x) {
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (playerPosition.position.x <= pos.x) {
            transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    protected void GoBack() {
        transform.position = Vector2.MoveTowards(transform.position, pointOfPatrol.position, speed * Time.deltaTime);
    }

    protected override void OnCollide(Collider2D collision) {
        if (collision.transform.tag == "Weapon") {
            //Debug.Log("Weapon is onCollide in Enemy_0");
            if (!Weapon.block)
                health -= GameManager.instance.GetWeaponDamage();
            if (health <= 0 && isAlive) {
                if (transform.localScale.x > 0)
                    animator.SetTrigger("Death");
                if (transform.localScale.x < 0)
                    animator.SetTrigger("Mirror_Death");
                GameManager.instance.GetPrize(transform);
                Invoke("DestroyEnemy", 2f);
                isAlive = false;
                return;
            }
            animator.SetTrigger("Swing");
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }

    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

}
