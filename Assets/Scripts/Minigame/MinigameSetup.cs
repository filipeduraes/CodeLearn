using UnityEngine;

namespace CodeLearn.Minigame
{
    [CreateAssetMenu(menuName = "CodeLearn/Minigame")]
    public class MinigameSetup : ScriptableObject
    {
        [SerializeField] private VariableDeclaration[] variableDeclarations;
        [SerializeField] private string[] callableFunctions;

        public VariableDeclaration[] VariableDeclarations => variableDeclarations;
        public string[] CallableFunctions => callableFunctions;
    }
}