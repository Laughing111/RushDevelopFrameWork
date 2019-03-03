using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public float fpsMeasuringDelta = 2.0f;
    private float timePassed;
    private int m_FrameCount = 0;
    private float m_FPS = 0.0f;
    //显示Fps示数
    public Text Text;

    // Use this for initialization
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_FrameCount = m_FrameCount + 1;
        timePassed = timePassed + Time.deltaTime;
        if (timePassed > fpsMeasuringDelta)
        {
            m_FPS = m_FrameCount / timePassed;

            timePassed = 0.0f;
            m_FrameCount = 0;
        }
        Text.text = m_FPS.ToString();
    }
}
