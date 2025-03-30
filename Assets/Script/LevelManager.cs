using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
   
    [HideInInspector] public Button[] buttons;

    private void Awake()
    {
        int unlockLevel = PlayerPrefs.GetInt("UnlockLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
   
    public void NexLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(level);
    }

  

   
    public void ReloadLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }

    public void LoadLevelByID(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    //show Panel
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void OffPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
