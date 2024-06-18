using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] keys;
    [SerializeField] private float speedAnimation = 1;
    [SerializeField] private bool playOnAwake;
    [SerializeField] private bool playOnEnable;
    [SerializeField] private bool loop;
    [SerializeField] private float delay;
    private Image image;
    private int currenKey;
    private bool isPlayed;
    private Sprite startSprite;
    private bool isEnable;

    private void Awake()
    {
       image = GetComponent<Image>();
       isPlayed = playOnAwake;
        startSprite = image.sprite;
    }
    IEnumerator StartAnimation()
    {
        while (isPlayed)
        {
            yield return new WaitForSecondsRealtime(1.0f / (24.0f * speedAnimation));
            if (currenKey < keys.Length)
            {
                image.sprite = keys[currenKey];
                currenKey++;
            }
            else
            {
                isPlayed = false;
                if (loop)
                {
                    StartCoroutine(DelayAnimation());
                }
                currenKey = 0;
            }
            if (!isPlayed)
            {
                image.sprite = startSprite;
            }
        }
    }
    IEnumerator DelayAnimation()
    {
        yield return new WaitForSecondsRealtime(delay);
        isPlayed = true;
        StartCoroutine(StartAnimation());
    }
    public void Play()
    {
        if (!isPlayed&&isEnable)
        {
            if (!image)
            {
                image = GetComponent<Image>();
            }
            isPlayed = true;
            if (gameObject.activeSelf)
            {
               StartCoroutine(StartAnimation());
            }
        }
    }
    public void Stop()
    {
        isPlayed = false;
    }
    private void OnEnable()
    {

        isEnable = true;
        if (playOnEnable)
        {
            isPlayed = true;
        }
        if (isPlayed)
        {
            if (image.gameObject.activeSelf)
            {
               StartCoroutine(StartAnimation());
            }
        }
    }
    private void OnDisable()
    {
        isEnable = false;
    }

}
