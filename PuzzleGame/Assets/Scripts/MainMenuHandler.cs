using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Button[] DifficultyButtons;
    public Button[] LevelButtons;

    public GameObject LevelPanel;
    public GameObject DifficultyPanel;
    private void Start()
    {
        for (int i = 0; i < DifficultyButtons.Length; i++)
        {
            string Difficulty = DifficultyButtons[i].name;
            DifficultyButtons[i].onClick.AddListener(delegate { SelectDifficulty(Difficulty); });
        }
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            int Level = int.Parse(LevelButtons[i].name);
            LevelButtons[i].onClick.AddListener(delegate { SelectLevel(Level); });
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            LevelPanelDisable();
        }
    }

    public void SelectDifficulty(string difficulty)
    {
        StaticLevelInfo.LevelDifficulty = difficulty;
    }
    public void SelectLevel(int level)
    {
        StaticLevelInfo.LevelNumber = level;
        SceneManager.LoadScene("LevelScene");
    }
    public void LevelPanelEnable()
    {
        LevelPanel.SetActive(true);
        DifficultyPanel.SetActive(false);
    }

    public void LevelPanelDisable()
    {
        LevelPanel.SetActive(false);
        DifficultyPanel.SetActive(true);
    }

}
