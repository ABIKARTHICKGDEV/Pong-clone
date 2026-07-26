using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public TMP_Text playerscoreText, computerscoreText;
   [HideInInspector] public int playerscore, computerscore;
    public int scoreToBeat = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreasePlayerScore()
    {
        playerscore++;
        playerscoreText.text = playerscore.ToString();

        if (playerscore == scoreToBeat )
        {
            // lode win scean
            SceneManager.LoadScene("Win");
        }
    }
    public void IncreaseComputerScore()
    {
        computerscore++;
        computerscoreText.text = computerscore.ToString();

        if (computerscore == scoreToBeat)
        {
            // lode lose scean
            SceneManager.LoadScene("Lose");
        }
    }
}
