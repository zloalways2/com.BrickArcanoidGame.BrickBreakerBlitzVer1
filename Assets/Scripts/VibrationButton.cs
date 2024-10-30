using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonSprite();
    }

    public void ToggleVibration()
    {
        VibrationManager.Instance.ToggleVibration();
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        buttonImage.sprite = VibrationManager.Instance.IsVibrationEnabled ? vibrationOnSprite : vibrationOffSprite;
    }
}