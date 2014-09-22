using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
[RequireComponent(typeof(AudioSource))]
public class AnimatedGUITextureButton : MonoBehaviour {

	public Color touchColor = Color.green;
	public Color disabledButtonColor = Color.gray;
	public float scalingRatio = 0.2f;
	public float scalingSpeed = 6;
	public float colorChangeSpeed = 6;
	public float scalingErrorMargin = 0.01f;
	public GUIText label;
	public AudioClip buttonDownClip;
	public float buttonDownSoundVolume = 1;
	public AudioClip buttonUpClip;
	public float buttonUpSoundVolume = 1;
	protected Color currentButtonColor;
	protected Color initialButtonColor;
	protected Vector3 initialScale;
	Touch buttonTouch;
	bool buttonTouched = false;
	bool labelClicked = false;
    public bool ButtonEnabled { get; private set; }
	float customDeltaTime;
	float timeToLatestFrame;

	protected void Awake ()
	{
        ButtonEnabled = true;
        initialScale = transform.localScale;
		initialButtonColor = guiTexture.color;
		currentButtonColor = initialButtonColor;
	}

	protected void Start ()
	{
		timeToLatestFrame = Time.realtimeSinceStartup;
	}

	protected void Update ()
	{
		float currentTime = Time.realtimeSinceStartup;
		customDeltaTime = currentTime - timeToLatestFrame;
		timeToLatestFrame = currentTime;
		if (ButtonEnabled) {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase==TouchPhase.Began
			    && (guiTexture.HitTest(Input.GetTouch(0).position)||
			    (label != null && label.HitTest(Input.GetTouch(0).position))))
			{
				buttonTouched = true;
				buttonTouch = Input.GetTouch (0);
				OnButtonDown();
				PlayButtonDownAnim ();
				if (buttonDownClip != null){
					audio.volume = buttonDownSoundVolume;
					audio.PlayOneShot(buttonDownClip);
				}
			}
			if (buttonTouched && buttonTouch.phase==TouchPhase.Ended)
			{
				if (InputInButtonArea())
					OnButtonUp();
				PlayButtonUpAnim ();
				buttonTouched = false;
				if (buttonUpClip != null){
					audio.volume = buttonUpSoundVolume;
					audio.PlayOneShot(buttonUpClip);
				}
			}
			if (label != null)
			{
				if (Input.GetMouseButtonDown(0) && label.HitTest(Input.mousePosition))
				{
					OnButtonDown ();
					PlayButtonDownAnim ();
					labelClicked = true;
					if (buttonDownClip != null){
						audio.volume = buttonDownSoundVolume;
						audio.PlayOneShot(buttonDownClip);
					}
				}
				else if (labelClicked && Input.GetMouseButtonUp(0))
				{
					if (InputInButtonArea())
						OnButtonUp ();
					PlayButtonUpAnim ();
					labelClicked = false;
					if (buttonUpClip != null){
						audio.volume = buttonUpSoundVolume;
						audio.PlayOneShot(buttonUpClip);
					}
				}
			}
		}
	}

	void OnMouseDown ()
	{
		if (ButtonEnabled) {
			OnButtonDown ();
			PlayButtonDownAnim ();
			if (buttonDownClip != null){
				audio.volume = buttonDownSoundVolume;
				audio.PlayOneShot(buttonDownClip);
			}
		}
	}

	void OnMouseUp ()
	{
		if (ButtonEnabled) {
			if (InputInButtonArea())
				OnButtonUp ();
			PlayButtonUpAnim ();
			if (buttonUpClip != null){
				audio.volume = buttonUpSoundVolume;
				audio.PlayOneShot(buttonUpClip);
			}
		}
	}

	private void PlayButtonDownAnim ()
	{
		StopCoroutine ("ScaleDownCoroutine");
		StartCoroutine ("ScaleUpCoroutine");
	}

	private void PlayButtonUpAnim ()
	{
		StopCoroutine ("ScaleUpCoroutine");
		StartCoroutine ("ScaleDownCoroutine");
	}

	protected virtual void OnButtonDown ()
	{
		// button down event processing
	}

	protected virtual void OnButtonUp ()
	{
		// button up event processing
	}

	private bool InputInButtonArea ()
	{
		if (Input.touchCount > 0 &&
		    (guiTexture.HitTest(Input.GetTouch(0).position) || (label != null && label.HitTest(Input.GetTouch(0).position))))
			return true;
		if (guiTexture.HitTest(Input.mousePosition) || (label != null && label.HitTest(Input.mousePosition)))
			return true;
		return false;
	}

	IEnumerator ScaleUpCoroutine ()
	{
		var targetScale = initialScale * (1 + scalingRatio);
		targetScale.z = initialScale.z;
		var targetReached = false;
		while (!targetReached)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, customDeltaTime * scalingSpeed);
			guiTexture.color = Color.Lerp (guiTexture.color, touchColor, customDeltaTime * colorChangeSpeed);
			if ((transform.localScale - targetScale).magnitude < scalingErrorMargin)
			{
				transform.localScale = targetScale;
				guiTexture.color = touchColor;
				targetReached = true;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator ScaleDownCoroutine ()
	{
		var targetReached = false;
		while (!targetReached)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, initialScale, customDeltaTime * scalingSpeed);
			guiTexture.color = Color.Lerp (guiTexture.color, currentButtonColor, customDeltaTime * colorChangeSpeed);
			if ((transform.localScale - initialScale).magnitude < scalingErrorMargin)
			{
				transform.localScale = initialScale;
				guiTexture.color = currentButtonColor;
				targetReached = true;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	public void ResetButtonAnims ()
	{
		StopCoroutine ("ScaleDownCoroutine");
		StopCoroutine ("ScaleUpCoroutine");
		transform.localScale = initialScale;
		guiTexture.color = currentButtonColor;
	}

	public void EnableButton (bool enabled)
	{
		ButtonEnabled = enabled;
		if (enabled) {
			guiTexture.color = initialButtonColor;
			currentButtonColor = initialButtonColor;
		}
		else {
			guiTexture.color = disabledButtonColor;
			currentButtonColor = disabledButtonColor;
		}
	}

}
