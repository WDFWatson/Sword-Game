using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator swordAnimator;
    public string mainScene;
    public string finalclipName;
    private bool isLoadingGame = false;

    private void Update()
    {
        if (isLoadingGame)
        {
            string clipName = swordAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (clipName == finalclipName)
            {
                Load();
            }
        }
    }

    public void Play()
    {
        swordAnimator.SetTrigger("Draw");
        isLoadingGame = true;
    }
    
    public void Load()
    {
        SceneManager.LoadScene(mainScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
