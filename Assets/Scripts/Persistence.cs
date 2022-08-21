using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour
{
    // ENCAPSULATION
    public static Persistence Instance { get; private set; }

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
