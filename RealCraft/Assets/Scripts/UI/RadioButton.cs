using UnityEngine;
using System.Collections;

public class RadioButton : AnimatedGUITextureButton {

	public RadioButton[] linkedButtons;
	private Color enabledStateColor;
	public Color disabledStateColor;
	public bool IsEnabled {
		get;
		private set;
	}

	protected new void Awake ()
	{
		base.Awake ();
		enabledStateColor = currentButtonColor;
	}

	protected virtual void OnButtonEnabled ()
	{
		// Processing logic for button press
	}

	protected virtual void OnButtonDisabled ()
	{
		// Processing logic for linked button press
	}

	protected void EnableButton ()
	{
		IsEnabled = true;
		foreach (var b in linkedButtons) {
			b.DisableButton();
		}
		currentButtonColor = enabledStateColor;
		OnButtonEnabled ();
	}

	private void DisableButton ()
	{
		IsEnabled = false;
		currentButtonColor = disabledStateColor;
		guiTexture.color = disabledStateColor;
		OnButtonDisabled ();
	}

	protected sealed override void OnButtonUp ()
	{
		EnableButton();
		base.OnButtonUp ();
	}

}
