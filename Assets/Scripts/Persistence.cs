using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles scene persistent data
/// </summary>
public class Persistence : MonoBehaviour
{
    // ENCAPSULATION
    // The Instance can be accessed from anywhere but it can only be changed in this class
    public static Persistence Instance { get; private set; }

    // ENCAPSULATION
    // The setter prevents the name from being set to null or a string consisting only of white spaces
    private string m_playerName = "";
    public string PlayerName
    { 
        get { return m_playerName; } 
        set 
        { 
            if (string.IsNullOrWhiteSpace(value))
            {
                m_playerName = "";
            }
            else
            {
                m_playerName = value;
            }
        } 
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
