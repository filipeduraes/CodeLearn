using System;
using UnityEngine;

namespace CodeLearn.Tutorial
{
    public class TutorialButton : MonoBehaviour
    {
        public static event Action OnTutorialPress = delegate { };
        public void TutorialButtonPress()
        {
            OnTutorialPress();
        }
    }
}
