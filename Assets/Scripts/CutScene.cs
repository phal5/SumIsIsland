using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Image[] cutImages; // Assign the 4 images in the inspector
    public float fadeDuration = 1.0f; // Duration of the fade-in effect
    public float displayDuration = 2.0f; // Duration each image is displayed before moving to the next cut
    public Button OKbutton;

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        for (int i = 0; i < cutImages.Length; i++)
        {
            yield return StartCoroutine(FadeInImage(cutImages[i]));
            if (i == 3)
            {
                StartCoroutine(ShakeScreen());
            }
            yield return new WaitForSeconds(displayDuration);
        }
        OKbutton.gameObject.SetActive(true);
    }

    private IEnumerator FadeInImage(Image img)
    {
        Color color = img.color;
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            color.a = alpha;
            img.color = color;
            yield return null;
        }
    }

    private IEnumerator ShakeScreen()
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float shakeDuration = 0.5f;
        float shakeAmount = 0.1f;

        while (shakeDuration > 0)
        {
            Camera.main.transform.position = originalPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }
}
