using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles main gameplay
/// </summary>
public class GameManager : MonoBehaviour
{
    // UI elements to be set in Unity
    [SerializeField] TextMeshProUGUI m_ScoreText;
    [SerializeField] Image m_HealthBar;
    [SerializeField] GameObject m_GameOverUI;
    [SerializeField] Button m_RestartButton, m_MenuButton;

    // ENCAPSULATION
    // Properties can be accessed from anywhere but can only be set in this class
    public int PlayerScore { get; private set; }
    public int PlayerHealth { get; private set; }
    public bool IsGameOver { get; private set; }

    // private fields
    private readonly int m_MaxHealth = 10;
    private string m_ScoreStr;
    private float timer = 5f;
    private ObjectPooler m_objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables
        m_objectPooler = FindObjectOfType<ObjectPooler>();
        IsGameOver = false;
        PlayerScore = 0;
        PlayerHealth = m_MaxHealth;

        if (Persistence.Instance != null)
        {
            m_ScoreStr = Persistence.Instance.PlayerName + "'s Score: ";
        }
        else
        {
            m_ScoreStr = "Player's Score: ";
        }

        // add functions to buttons
        m_RestartButton.onClick.AddListener(RestartGame);
        m_MenuButton.onClick.AddListener(ToMenu);

        // initialize UI elements
        m_GameOverUI.SetActive(false);
        m_HealthBar.fillAmount = 1f;
        m_ScoreText.text = m_ScoreStr + PlayerScore;

        // start game
        Time.timeScale = 1f;
        Invoke(nameof(Spawn), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameOver)
        {
            // increase game difficulty over time
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0f)
            {
                timer = 5f;
                Time.timeScale += 0.1f;
            }
        }
    }

    // ABSTRACTION
    // Method can be called with one line instead of copying/rewriting code
    /// <summary>
    /// Spawns new objects in game as long as game is not over
    /// </summary>
    private void Spawn()
    {
        if (IsGameOver)
        {
            return;
        }
        else
        {
            m_objectPooler.SpawnShootable();
            Invoke(nameof(Spawn), Random.Range(1f, 2f));
        }
    }

    /// <summary>
    /// Adds points to the player's score
    /// </summary>
    /// <param name="points">points to add</param>
    public void AddPoints(int points)
    {
        PlayerScore += points;
        m_ScoreText.text = m_ScoreStr + PlayerScore;
    }

    /// <summary>
    /// Adds health to the player's current health
    /// </summary>
    /// <remarks>Player health will always be between 0 and max health</remarks>
    /// <param name="health">amount of health to add - can be negative</param>
    public void AddHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth + health, 0, m_MaxHealth);
        m_HealthBar.fillAmount = (float)PlayerHealth / m_MaxHealth;

        if (PlayerHealth <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Stops the game and shows the game over screen
    /// </summary>
    private void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0;
        m_GameOverUI.SetActive(true);
    }

    /// <summary>
    /// Reloads the main game scene
    /// </summary>
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the Start Menu Scene
    /// </summary>
    private void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
