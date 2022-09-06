using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private HairManager _hairManager;
    private Stick _stickCharacter;
    [SerializeField] private float _waitSeconds;
    [SerializeField] private float _endSeconds;
    [SerializeField] private UIManager _uiManager;
    private void Awake()
    {
        _hairManager = new HairManager(GameFinished);
        _hairManager.FindList();
        _stickCharacter = Object.FindObjectOfType<Stick>();
        _uiManager.Init(GameState);
    }


    public void GameState(bool isPlaying)
    {
        _hairManager.GameState(isPlaying);
        if (isPlaying)
        {
            Time.timeScale = 1f;
            _stickCharacter.StartStates();
        }
        else
        {
            Time.timeScale = 0f;
            _stickCharacter.StopStates();
        }

    }
    public void GameFinished()
    {
        StartCoroutine(GameFinishRoutines());
        
    }
    IEnumerator GameFinishRoutines()
    {
        yield return new WaitForSeconds(_waitSeconds);
        _stickCharacter.StopStates();
        _stickCharacter.FinishMove();
        StartCoroutine(GameEnd());
    }
    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(_endSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public class HairManager
    {
        private Hair[] _hairList;
        private int count;
        private System.Action onFinishgame;

        public HairManager(System.Action hasGameFinished)
        {
            this.onFinishgame = hasGameFinished;
        }
        public void FindList()
        {
            _hairList = Object.FindObjectsOfType<Hair>();
            count = _hairList.Length;
            foreach (Hair hair in _hairList)
            {
                hair.Init(OnCollisionWithWax);

            }
        }
        void OnCollisionWithWax()
        {
            count -= 1;
            if (count <= 0)
            {
                onFinishgame();
            }
        }
        public void GameState(bool hasStarted)
        {
            if (hasStarted)
            {
                foreach (Hair hair in _hairList)
                {
                    hair.State(hasStarted);
                }
            }
        }
    }
}
