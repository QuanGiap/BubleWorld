using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public GameObject menu;
    public bool isOn=true;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOn = !isOn;
            menu.SetActive(isOn);
            if (isOn)
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        isOn = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
