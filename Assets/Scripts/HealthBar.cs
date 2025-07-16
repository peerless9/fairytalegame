using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform topBarTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    public void updateBar(float percentHealth)
    {
        topBarTransform.sizeDelta = new Vector2(
            topBarTransform.sizeDelta.x,
            Mathf.Clamp(percentHealth * 100, 0, 100) // Assuming the height is in percentage
        );
        topBarTransform.anchoredPosition = new Vector2(
            topBarTransform.anchoredPosition.x,
            Mathf.Clamp(percentHealth * 100, 0, 100) // Adjusting the position based on health percentage
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
