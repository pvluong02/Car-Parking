using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GoogleMobileAds.Api;

public class Game : MonoBehaviour
{
    public AudioClip audioClip;
    public static Game Instance;
    public bool gameOver = false;
    [HideInInspector] public List<Route> readlyRoutes = new();
    private int totalRoutes;
    public LevelManager levelManager;
    [HideInInspector]public int succesFullParks;
    public UnityAction<Route> onCarEnterPark;
    public UnityAction onCarCollision;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        succesFullParks = 0;
        onCarEnterPark += onCarEnterParkHandle;
        onCarCollision += onCarCollisionHandle;
        
    }
    private void onCarCollisionHandle()
    {
        Debug.Log("GameOver");
        gameOver = true;

        Invoke("ShowAds", 3f);

    }

    public void LoadGame()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }
    public void ShowAds ()
    {
        if (gameOver)
        {
            AdsManager.Instance.ShowInterstitialAd();
        }
    }

    private void onCarEnterParkHandle(Route route)
    {
        route.car.StopDancingAnim();
        succesFullParks++;

        if (succesFullParks == totalRoutes)
        {
            Debug.Log("You win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.5f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                {
                    UnlockNewLevel();
                    SceneManager.LoadScene(nextLevel);
                    AudioManager.Instance.PlayBGM(audioClip);
                }
                else
                {
                    Debug.LogError("No level loading");

                }
            }
            );
        }
    }

    public void RegisterRouter(Route route)
    {
        readlyRoutes.Add(route);

        if (readlyRoutes.Count == totalRoutes)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach( var route in readlyRoutes )
        {
            route.car.Move(route.linePoints);
        }
    }

    public void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachecdIndex")) { 
            PlayerPrefs.SetInt("ReachecdIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
