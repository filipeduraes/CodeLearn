using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace CodeLearn.Tutorial
{
    public class VideoPlayerController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Material playButtonMaterial;
        [SerializeField] private Material pauseBottonMaterial;
        [SerializeField] private RawImage playPauseButton;

        private void OnEnable()
        {
            PlayPauseButton.OnPlayPause += PlayPause;
            SkipButton.OnSkipPress += Skip;
            TutorialButton.OnTutorialPress += ReWatchTutorial;
            videoPlayer.loopPointReached += PlayAgain;
        }

        private void OnDisable()
        {
            PlayPauseButton.OnPlayPause -= PlayPause;
            SkipButton.OnSkipPress -= Skip;
            TutorialButton.OnTutorialPress -= ReWatchTutorial;
            videoPlayer.loopPointReached -= PlayAgain;
        }

        private void Update()
        {
            UpdatePlayPauseButton();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayPause();
            }
        }

        private void UpdatePlayPauseButton()
        {
            playPauseButton.material = videoPlayer.isPlaying ? playButtonMaterial : pauseBottonMaterial;
        }

        private void PlayPause()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }

        private void PlayAgain(VideoPlayer source)
        {
            playPauseButton.material = playButtonMaterial;
        }

        private void Skip()
        {
            canvas.gameObject.SetActive(false);
        }

        private void ReWatchTutorial()
        {
            canvas.gameObject.SetActive(enabled);
        }
    }
}
