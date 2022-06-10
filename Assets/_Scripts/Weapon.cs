using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage;
    public static bool block = false;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            anim.SetTrigger("Swing");
            block = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            anim.SetTrigger("Block");
            block = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            anim.SetTrigger("End_of_block");
        }
    }

}
