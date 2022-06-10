using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape_Menu : MonoBehaviour {

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            animator.SetTrigger("Show");
        }
    }

    public void ContinueGame() {
        animator.SetTrigger("Show");
    }

    public void QuitToStartMenu() {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
