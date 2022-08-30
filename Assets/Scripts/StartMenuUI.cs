using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles Start Menu functionality
/// </summary>
public class StartMenuUI : MonoBehaviour
{
    // To be set in Unity
    public TMP_InputField nameInput;
    public Button startButton;

    // Start is called just before any of the Update methods is called for the first time
    private void Start()
    {
        if (Persistence.Instance != null)
        {
            SetPlayerName(Persistence.Instance.PlayerName);
        }
    }

    // POLYMORPHISM
    // Method overload - the given parameter(s) determine which SetPlayerName method will be executed
    /// <summary>
    /// Gets the player name from the name input field and sets it to the persistence instance
    /// </summary>
    public void SetPlayerName()
    {
        Persistence.Instance.PlayerName = nameInput.text;
        startButton.interactable = Persistence.Instance.PlayerName.Length > 0;
    }

    /// <summary>
    /// Sets the given name to the name input field
    /// </summary>
    /// <param name="name">name to set</param>
    private void SetPlayerName(string name)
    {
        nameInput.text = name;
    }

    /// <summary>
    /// Loads the main game scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Exits the Application
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
