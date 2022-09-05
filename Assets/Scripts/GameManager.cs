using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HairManager _hairManager;
    private Stick _stickCharacter;
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
        if (isPlaying)
        {
            _stickCharacter.StartStates();
        }
        else
        {
            _stickCharacter.StopStates();
        }

    }
    public void GameFinished()
    {
        _stickCharacter.StopStates();
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
    }
}
