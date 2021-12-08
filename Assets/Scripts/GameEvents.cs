using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameEvents : MonoBehaviour
{
    public bool gameOver;

    [SerializeField]
    private UnityEvent onGameOver;
    [SerializeField]
    private UnityEvent onGameWin;

    [SerializeField] private UnityEvent onRespawn;


    public void SetGameOver()
    {
        onGameOver?.Invoke();
        gameOver = true;
    }
    
    public void SetGameWin()
    {
        onGameWin?.Invoke();
        gameOver = true;
    }

    public void SetRespawn()
    {
        gameOver = false;
        onRespawn?.Invoke();
    }
}
