using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Level thisLevel;
    public bool isLevelDone;

    private GameObject Grid;
    private GameObject[] PuzzlePieces;

    [SerializeField] 
    private TextMeshProUGUI txtLevel;
    [SerializeField]
    private Button btnNextLevel;


    void Start()
    {
        ReadJsonFile(StaticLevelInfo.LevelDifficulty, StaticLevelInfo.LevelNumber);
        InitializeLevel();
    }

    void Update()
    {
        if (isLevelDone == false)
            CheckGrid();
    }

    private void CheckGrid()
    {
        isLevelDone = false;
        try
        {
            if (Grid == null)
            {   // so that it does not search in every frame
                Grid = GameObject.Find("GridBoard").gameObject;
            }
            
            for (int i = 0; i < Grid.transform.childCount; i++)
            {
                if (Grid.transform.GetChild(i).gameObject.GetComponent<GridTile>().isCovered == false)
                {
                    return;
                }
            }
            // If all pieces covered wont enter if and will continue after for loop
            if (CheckIfAllPiecesPlaced() == true)
            {
                isLevelDone = true;
            }
        }
        catch (System.Exception)
        {
            isLevelDone = false;
            Debug.Log("Grid haven't genareted yet");
        }
        
        if (isLevelDone == true)
        {
            LevelEnder();
        }
    }


    private void InitializeLevel()
    {
        Grid = null;

        gameObject.GetComponent<GridBoardGenerator>().GenerateTheGrid();
        gameObject.GetComponent<PieceGenerator>().GenerateThePieces();
        txtLevel.text = thisLevel.difficulty + " " + thisLevel.level;

        isLevelDone = false;
    }

    private void LevelEnder()
    {
        DisableDragger(); // Disable Dragger component from Puzzle pieces
        btnNextLevel.gameObject.SetActive(true);
    }

    private bool CheckIfAllPiecesPlaced()
    {
        bool result = true;
        GameObject PuzzlePiecesParent = GameObject.Find("PuzzlePieces").gameObject;

        PuzzlePieces = new GameObject[thisLevel.pieceNumber];
        for (int i = 0; i < thisLevel.pieceNumber; i++)
        {
            PuzzlePieces[i] = PuzzlePiecesParent.transform.GetChild(i).gameObject;
            if (PuzzlePieces[i].GetComponent<Dragger>().isPlaced == false)
            {
                result = false;
            }
        }
        return result;
    }

    private void DisableDragger()
    {
        for (int i = 0; i < thisLevel.pieceNumber; i++)
        {
            PuzzlePieces[i].GetComponent<Dragger>().enabled = false;


            Color materialColor = PuzzlePieces[i].GetComponent<MeshRenderer>().material.color;
            materialColor.a = 1;
            PuzzlePieces[i].GetComponent<MeshRenderer>().material.color = materialColor;
        }
    }
    public void LoadNextLevel()
    {
        CloseThisLevel();
        ReadJsonFile(thisLevel.difficulty, thisLevel.level + 1);

        Invoke("InitializeLevel", 0.1f); // Wait Objects to destroy
    }

    private void CloseThisLevel()
    {
        btnNextLevel.gameObject.SetActive(false);   //Disable the button
        Object.Destroy(GameObject.Find("GridBoard").gameObject);
        Object.Destroy(GameObject.Find("PuzzlePieces").gameObject);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReadJsonFile(string Difficulty, int Level)
    {
        string Path = "";
        if (Difficulty == "Easy")
            Path = "Levels/E" + Level.ToString();
        else if (Difficulty == "Medium")
            Path = "Levels/M" + Level.ToString();
        else if (Difficulty == "Hard")
            Path = "Levels/H" + Level.ToString();

        try
        {
            TextAsset jsonFile = Resources.Load(Path) as TextAsset;
            thisLevel = JsonUtility.FromJson<Level>(jsonFile.ToString());
        }
        catch (System.Exception)
        {
            if (Difficulty == "Easy")
                ReadJsonFile("Medium", 1);
            else if (Difficulty == "Medium")
                ReadJsonFile("Hard", 1);
            else
                SceneManager.LoadScene("MainMenu");
        }
    }

    [System.Serializable]
    public class Level
    {
        public string difficulty;
        public int level;
        public int gridDim;
        public int pieceNumber;
        public List<Piece> pieces;
    }

    [System.Serializable]
    public class Piece
    {
        public Vector2[] vertices;
    }

}
