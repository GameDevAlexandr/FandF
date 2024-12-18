using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;
using static GeneralData;

namespace Tutorial
{
    public class CoalHintEvent : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic _coalAnimation;
        [SerializeField] private Text _bubbleText;
        [SerializeField] private Text _pressToContinue;
        [SerializeField] private TutorialHandler _tutorial;
        private bool _isCloseRady;
        private void Start()
        {
            Sounds.chooseSound.hint.Play();
            Controll.clickAnyPlaceEvent.AddListener(Close);
        }
        public void SetCoalHint(string message)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            _bubbleText.text = message;
            _coalAnimation.AnimationState.SetAnimation(0, "start", false);
            _coalAnimation.AnimationState.AddAnimation(0, "idle", true, 0.83f);
            StartCoroutine(OpenBubble(0.5f));
            if (Sounds.chooseSound)
            {
                Sounds.chooseSound.hint.Play();
            }
            _isCloseRady = false;
        }

        public void Close()
        {
            if (_isCloseRady)
            {
                Time.timeScale = 1;
                _isCloseRady = false;
                _pressToContinue.enabled = false;
                gameObject.SetActive(false);
                _tutorial.CompleteIteration();
            }
        }

        IEnumerator OpenBubble(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            _isCloseRady = true;
            _pressToContinue.enabled = true;
        }
    }
}
