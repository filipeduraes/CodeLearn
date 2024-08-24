using System.Collections.Generic;
using UnityEngine;

namespace CodeLearn.Minigame
{
    [CreateAssetMenu(menuName = "CodeLearn/Minigame")]
    public class MinigameSetup : ScriptableObject
    {
        [SerializeField] private VariableDeclaration[] variableDeclarations;

        public IEnumerable<VariableDeclaration> VariableDeclarations => variableDeclarations;
    }
}