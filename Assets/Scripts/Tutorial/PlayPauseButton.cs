using System;
using UnityEngine;

namespace CodeLearn.Tutorial
{
    public class PlayPauseButton : MonoBehaviour
    {
        public static event Action OnPlayPause = delegate { };
        public void PlayPauseButtonPress()
        {
            OnPlayPause();
        }
    }
}
