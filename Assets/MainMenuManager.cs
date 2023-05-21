using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator swordAnimator;
    [SerializeField] private Animator UIAnimator;
    // Start is called before the first frame update
    private bool changeScene;
    void Start()
    {
        changeScene = false;
        swordAnimator.SetTrigger("StillUndrawn");
    }

    private void Update()
    {
        if (changeScene && swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("Still"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void Play()
    {
        UIAnimator.SetTrigger("FadeOut");
        swordAnimator.SetTrigger("DrawSword");
        changeScene = true;
    }


}
