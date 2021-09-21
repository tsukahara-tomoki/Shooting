using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpRegeneration : MonoBehaviour
{
    Slider m_sliderG;
    [SerializeField] public Slider m_sliderR;
    public int m_Mp = 0;
    int m_R;
    [SerializeField] int regenDelay = 5;
    float regenTitle = 0;
    [SerializeField] Text mpText;

    // Start is called before the first frame update
    void Start()
    {
        m_sliderG = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        regenTitle += Time.deltaTime;
        if (regenTitle >= 0.2)
        {
            if (m_Mp < m_sliderG.maxValue)
            {
                m_Mp += regenDelay;
                m_sliderG.value = ++m_Mp;
                regenTitle = 0;
                mpText.text = m_Mp.ToString();
            }
            else if(m_Mp >= m_sliderG.maxValue)
            {
                m_Mp = (int)m_sliderG.maxValue;
                mpText.text = m_Mp.ToString();
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (m_Mp <= m_sliderR.value)
        {
            m_sliderR.value -= 3;
        }
        
    }
}
