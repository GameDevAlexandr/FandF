using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Collections;
using static GeneralData;

public class CoalHintEvent : MonoBehaviour
{
    [SerializeField] private Hole _hole;
    [SerializeField] private SkeletonGraphic _coalAnimation;
    [SerializeField] private AnimationReferenceAsset _talk;
    [SerializeField] private AnimationReferenceAsset _start;
    [SerializeField] private Text _bubbleText;
    [SerializeField] private GameObject _bubble;
    [SerializeField] private Text _pressToContinue;
    [SerializeField] private TutorialHandler _tutorial;
    [SerializeField] private GameObject[] _mustBeClosed;
    private bool _isCloseRady;
    private int _nextIteration;
    private TutorialHandler.tData _tData;
    public bool _isSkip;
    //private void Start()
    //{
    //    Sounds.chooseSound.hint.Play();
    //}
    //public void SetCoalHint(TutorialHandler.tData data)
    //{
    //    gameObject.SetActive(true);
    //    _tData = data;
    //    _bubbleText.text = data.hinttext;
    //    _nextIteration = _tutorial.DefineIteration(data.nextIteration);
    //    _coalAnimation.AnimationState.SetAnimation(0, _start, false);
    //    _coalAnimation.AnimationState.AddAnimation(0, _talk, true,0).TimeScale = 1.5f;
    //    for (int i = 0; i < _mustBeClosed.Length; i++)
    //    {
    //        _mustBeClosed[i].SetActive(false);
    //    }
    //    StartCoroutine(OpenBubble(1.0f));
    //    if (Sounds.chooseSound)
    //    {
    //        Sounds.chooseSound.hint.Play();
    //    }
    //    _isSkip = false;
    //}

    //public void Close()
    //{
    //    if (_isCloseRady)
    //    {
    //        //_bubble.SetActive(false);
            
    //        _isCloseRady = false;            
    //        _pressToContinue.enabled = false;
    //        Time.timeScale = 1;
    //        _isSkip = true;
    //        if (_tData.hole)
    //        {
    //            _hole.transform.parent.gameObject.SetActive(true);
    //            _hole.SetNewHolePosition(_tData);
    //            gameObject.SetActive(false);
    //        }
    //        else if(_nextIteration!=0)
    //        {
    //            tutorData[_tutorial.DefineIteration(_tData.iterationName)] = true;
    //            _tutorial.SetNewIteration(_nextIteration);
    //            return;
    //        }
    //        gameObject.SetActive(false);
    //    }        
    //}

    //IEnumerator OpenBubble(float seconds)
    //{
    //    yield return new WaitForSecondsRealtime(seconds);
    //    if (!_bubble.activeSelf)
    //    {
    //        StartCoroutine(OpenBubble(1.0f));
    //        _bubble.SetActive(true);
    //    }
    //    else
    //    {
    //        _isCloseRady = true;
    //        _pressToContinue.enabled = true;
    //    }        
    //}
}
