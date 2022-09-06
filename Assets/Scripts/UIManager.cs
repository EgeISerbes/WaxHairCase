using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private System.Action<bool> _onGameStart;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private float _waitSeconds;
    public void Init(System.Action<bool> GameStarted)
    {
        _onGameStart = GameStarted;
        _playButton.SetActive(true);
        _pauseButton.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    public void StartPlaying()
    {
        _playButton.SetActive(false);
        GameResumed();
    }
    public void GamePaused()
    {
        _pauseButton.SetActive(false);
        _pauseMenu.SetActive(true);
        _onGameStart(false);
    }

    public void GameResumed()
    {
        _pauseButton.SetActive(true);
        _pauseMenu.SetActive(false);
        _onGameStart(true);
    }
    public void GameRestarted()
    {
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
