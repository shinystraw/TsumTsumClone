using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool countDown;
    [SerializeField] private bool startTimer;
    [SerializeField] private float delayStart;

    [Header("Timelimit Settings")]
    [SerializeField] private bool hasLimit;
    [SerializeField] private float timeLimit;
    [SerializeField] private bool limitReached;

    // Start is called before the first frame update
    void Start()
    {
        SetText();

        if (startTimer == false)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayStart);
        startTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (countDown)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime += Time.deltaTime;
            }

            if (currentTime <= 11 && countDown)
            {
                SetFinalCountDown();
            }

            if (hasLimit && ((countDown && currentTime <= timeLimit) || (!countDown && currentTime >= timeLimit)))
            {
                limitReached = true;
                timerText.text = timeLimit.ToString();
            }
            else
            {
                SetText();
            }
        }
    }

    public bool GetLimitReached()
    {
        return limitReached;
    }

    void SetFinalCountDown()
    {
        timerText.color = Color.yellow;
    }

    void SetText()
    {
        timerText.text = currentTime.ToString("0");
    }
}
