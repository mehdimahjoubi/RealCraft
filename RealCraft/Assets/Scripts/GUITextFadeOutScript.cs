using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class GUITextFadeOutScript : MonoBehaviour {

    public float fadeOutSpeed = 50;
    public float fadeOutStartTimer = 3;
    Color initialColor;

    public void Awake()
    {
        initialColor = guiText.color;
    }

    public void OnEnable()
    {
        Debug.Log("fade out!!!!!");
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        Debug.Log("fadeout started!");
        yield return new WaitForSeconds(fadeOutStartTimer);
        while (guiText.color.a > 0)
        {
            var c = guiText.color;
            c.a -= fadeOutSpeed * Time.deltaTime;
            guiText.color = c;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
        guiText.color = initialColor;
    }
	
}
