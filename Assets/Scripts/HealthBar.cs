using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform topBarTransform;
    private float ogSize;
    private float ogPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ogSize = topBarTransform.sizeDelta.x;
        ogPosition = topBarTransform.anchoredPosition.x;
        updateBar(1f); // Initialize the health bar to full size
    }

    public void updateBar(float percentHealth)
    {
        topBarTransform.sizeDelta = new Vector2(
            Mathf.Clamp(percentHealth * ogSize, 0, ogSize),
            topBarTransform.sizeDelta.y
        );
        topBarTransform.anchoredPosition = new Vector2(
            Mathf.Clamp( (percentHealth - 1) * ogSize * 0.5f, -ogSize*0.5f, ogSize*0.5f),
            topBarTransform.anchoredPosition.y
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
