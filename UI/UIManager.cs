﻿using UniRx;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject scena;
    [SerializeField] private GameObject background;
    private bool isPaused = false;

    
 

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        gameUI.SetActive(true);
        startUI.SetActive(true);
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(gameUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(winUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(loseUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.blevotina.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(loseUI);
            })
            .AddTo(subscriptions);

    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    public void ActivateMenu(GameObject _menu)
    {
        gameUI.SetActive(false);
        startUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);


        _menu.SetActive(true);
    }

    //Level functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        int newCurrentLevel = PlayerPrefs.GetInt("currentLevel", 1) + 1;
        int newLoadingLevel = PlayerPrefs.GetInt("loadingLevel", 1) + 1;

        if (newLoadingLevel >= SceneManager.sceneCountInBuildSettings)
            newLoadingLevel = 1;

        PlayerPrefs.SetInt("currentLevel", newCurrentLevel);
        PlayerPrefs.SetInt("loadingLevel", newLoadingLevel);

        SceneManager.LoadScene(newLoadingLevel);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            
            scena.SetActive(true);
            background.SetActive(true);
            Time.timeScale = 0;
            
        }
        else
        {
            
            scena.SetActive(false);
            background.SetActive(false);
            Time.timeScale = 1;
            

        }
    }

   

    public void Continue()
    {
        GameEvents.instance.gameStarted.SetValueAndForceNotify(true);
    }

}
