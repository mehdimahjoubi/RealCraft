using UnityEngine;
using System.Collections;

public class NotificationScript : MonoBehaviour {

    public AnimatedGUITextureButton scanButton;
    public GUITexture notice;
    public GUIText notifGUIText;
    public float popUpSpeed = 5;
    public AnimatedGUITextureButton okButton;
    public AnimatedGUITextureButton cancelButton;
    public AnimatedGUITextureButton overrideButton;
    public AnimatedGUITextureButton newFileButton;
    Vector3 initialnoticecale;
    bool noticeVisible = false;
    bool allowNoticeAutoHiding = true;
    Vector3 initialTextScale;

    void Awake()
    {
        initialTextScale = notifGUIText.transform.localScale;
        initialnoticecale = notice.transform.localScale;
        notice.transform.localScale = Vector3.zero;
        notice.gameObject.SetActive(false);
    }

    void Update()
    {
        if (allowNoticeAutoHiding && noticeVisible && ((Input.touchCount > 0 && notice.HitTest(Input.GetTouch(0).position))
                 || Input.GetKeyDown(KeyCode.Escape)
                 || (Input.GetMouseButtonDown(0) && notice.HitTest(Input.mousePosition))))
        {
            HideNotification();
        }
    }

    public void HideButtons()
    {
        okButton.ResetButtonAnims();
        cancelButton.ResetButtonAnims();
        overrideButton.ResetButtonAnims();
        newFileButton.ResetButtonAnims();
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        overrideButton.gameObject.SetActive(false);
        newFileButton.gameObject.SetActive(false);
        allowNoticeAutoHiding = true;
    }

    public void HideNotification()
    {
        StopCoroutine("TogglenoticeCoroutine");
        StartCoroutine("TogglenoticeCoroutine", false);
    }

    public void ShowNotification(string text, bool targetScanning = false, float textSizeRatio = 1, FontStyle font = FontStyle.Normal, bool fileSaving = false)
    {
        if (textSizeRatio != 1)
            notifGUIText.transform.localScale *= textSizeRatio;
        notifGUIText.fontStyle = font;
        notifGUIText.text = text;
        if (targetScanning && !fileSaving)
        {
            okButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            allowNoticeAutoHiding = false;
        }
        else if (fileSaving && !targetScanning)
        {
            overrideButton.gameObject.SetActive(true);
            newFileButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            allowNoticeAutoHiding = false;
        }
        else
            allowNoticeAutoHiding = true;
        StopCoroutine("TogglenoticeCoroutine");
        StartCoroutine("TogglenoticeCoroutine", true);
    }

    public void ShowDelayedNotification(string text, float delay, float textSizeRatio = 1)
    {
        notifGUIText.transform.localScale *= textSizeRatio;
        StartCoroutine(DelayedNotificationCoroutine(text, delay));
    }

    IEnumerator DelayedNotificationCoroutine(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowNotification(text, false);

    }

    IEnumerator TogglenoticeCoroutine(bool show)
    {
        Vector3 targetScale = Vector3.zero;
        if (show)
        {
            scanButton.EnableButton(false);
            targetScale = initialnoticecale;
            notice.gameObject.SetActive(true);
            noticeVisible = true;
        }
        else
            targetScale = Vector3.zero;
        while ((notice.transform.localScale - targetScale).magnitude > 0)
        {
            notice.transform.localScale = Vector3.MoveTowards(notice.transform.localScale, targetScale, Time.deltaTime * popUpSpeed);
            yield return new WaitForEndOfFrame();
        }
        if (!show)
        {
            okButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            notice.gameObject.SetActive(false);
            noticeVisible = false;
            scanButton.EnableButton(true);
            notifGUIText.transform.localScale = initialTextScale;
        }
    }
}
