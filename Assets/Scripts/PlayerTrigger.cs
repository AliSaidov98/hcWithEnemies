using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private GameEvents _gameEvents;

    private void Awake()
    {
        _gameEvents = FindObjectOfType<GameEvents>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") && !_gameEvents.gameOver)
            _gameEvents.SetGameWin();
    }
}
