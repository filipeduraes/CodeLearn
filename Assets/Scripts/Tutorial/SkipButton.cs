using System;
using UnityEngine;

namespace CodeLearn.Tutorial
{
    public class SkipButton : MonoBehaviour
    {
        public static event Action OnSkipPress = delegate { };
        public void SkipButtonPress()
        {
            OnSkipPress();
        }
    }
}
