using System;
using UnityEngine;

namespace CodeLearn.SnakeGame.Linker
{
    public class SnakeGameRuntime : MonoBehaviour
    {
        [SerializeField] private SnakeGameView snakeGameView;

        private void Awake()
        {
            //new SnakeGameLinker(snakeGameView.SnakeTheGame, )
        }
    }
}