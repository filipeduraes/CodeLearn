using CodeLearn.CodeEditor;
using CodeLearn.UI.CodeEditor.ViewModel;
using UnityEngine;

namespace CodeLearn.SnakeGame.Linker
{
    public class SnakeGameRuntime : MonoBehaviour
    {
        [SerializeField] private SnakeGameView snakeGameView;
        [SerializeField] private CodeEditorCompiler compiler;
        [SerializeField] private SnakeGameInputModule inputModule;
        
        private void OnEnable()
        {
            compiler.OnCompile += Link;
            compiler.OnInterpreterLoop += snakeGameView.Tick;
            compiler.OnExecutionStopped += snakeGameView.ResetGame;
        }

        private void OnDisable()
        {
            compiler.OnCompile -= Link;
            compiler.OnInterpreterLoop -= snakeGameView.Tick;
            compiler.OnExecutionStopped -= snakeGameView.ResetGame;
        }
        
        private void Link(MemoryModule memoryModule)
        {
            SnakeGameLinker _ = new(snakeGameView.SnakeTheGame, inputModule, memoryModule);
        }
    }
}