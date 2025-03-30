using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UISetting : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Giữ lại giữa các Scene
    }

    public void LoadHome(GameObject panel)
    {
        panel.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void Resume(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void Setting(GameObject panel)
    {
        panel.SetActive(true);
    }


}
