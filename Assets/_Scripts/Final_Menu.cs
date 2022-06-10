using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final_Menu : MonoBehaviour {

    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        Invoke("SetTriggerMenu", 2f);
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene("Main_Menu");
    }

    private void SetTriggerMenu() {
        animator.SetTrigger("Show");
    }
}
