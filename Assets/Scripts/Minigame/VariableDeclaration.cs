using System;
using UnityEngine;

namespace CodeLearn.Minigame
{
    [Serializable]
    public struct VariableDeclaration
    {
        [SerializeField] private VariableType type;
        [SerializeField] private string name;

        public VariableType Type => type;
        public string Name => name;

        public enum VariableType
        {
            Numeric,
            Logic
        }
    }
}