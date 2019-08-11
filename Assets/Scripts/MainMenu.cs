using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{

    private enum MainMenuPosition
    {
        SampleScene = 0, BrowseCards, Options, Exit
    }

    private MainMenuPosition m_selected = MainMenuPosition.SampleScene;

    // Update is called once per frame
    new protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++m_selected;
            if(m_selected > MainMenuPosition.Exit)
            {
                m_selected = MainMenuPosition.SampleScene;
                SelectionArrow.transform.position += Vector3.up * 6;
            }
            else
            {
                SelectionArrow.transform.position += Vector3.down * 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            --m_selected;
            if(m_selected < 0)
            {
                m_selected = MainMenuPosition.Exit;
                SelectionArrow.transform.position += Vector3.down * 6;
            }
            else
            {
                SelectionArrow.transform.position += Vector3.up * 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (m_selected)
            {
                case MainMenuPosition.SampleScene:
                    {
                        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                        break;
                    }
                case MainMenuPosition.BrowseCards:
                    {
                        GameObject.FindObjectOfType<UnitBrowser>().BrowseUnits();
                        break;
                    }
                case MainMenuPosition.Options:
                    {
                        break;
                    }
                case MainMenuPosition.Exit:
                    {
                        Application.Quit();
                        break;
                    }
            }

        }

        base.Update();
    }
}
