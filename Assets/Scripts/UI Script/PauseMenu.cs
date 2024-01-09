using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;

    public GameObject go_BaseUi;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPause)
            {
                CallMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                CloseMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
        }

    }

    private void CallMenu()
    {
        isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;

    }

    private void CloseMenu()
    {
        isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;

    }

    public void ClickExit()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void ResumeGame()
    {
        isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;

    }
}
