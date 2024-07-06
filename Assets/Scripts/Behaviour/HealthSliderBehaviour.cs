using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderBehaviour : MonoBehaviour
{
    private TMP_Text m_Text;
    private Slider m_Slider;

    void Start()
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Slider = GetComponent<Slider>();
    }

    void Update()
    {
        m_Text.text = (m_Slider.value*100).ToString();
    }
}
