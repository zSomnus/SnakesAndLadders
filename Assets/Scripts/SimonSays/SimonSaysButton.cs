using UnityEngine;
using UnityEngine.UI;

public class SimonSaysButton : MonoBehaviour
{
    private const int FlashSpeed = 10;
    [SerializeField] Image image;
    [SerializeField] AudioSource clickAudio;

    Color originalColor;

    void Awake()
    {
        originalColor = image.color;
    }

    void Update()
    {
        image.color = Color.Lerp(image.color, originalColor, Time.deltaTime * FlashSpeed);
        image.rectTransform.localScale = Vector3.Lerp(image.rectTransform.localScale, Vector3.one, Time.deltaTime * FlashSpeed);
    }

    public void Flash()
    {
        image.color = Color.white;
        image.rectTransform.localScale = Vector3.one * 1.25f;
        clickAudio.Play();
    }
}
