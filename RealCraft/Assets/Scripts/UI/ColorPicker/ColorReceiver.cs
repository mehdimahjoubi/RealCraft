using UnityEngine;

public class ColorReceiver : MonoBehaviour {

    //public Camera renderCam;
    public static Color SelectedColor { get; private set; }

	void OnColorChange(HSBColor color) 
	{
        SelectedColor = color.ToColor();
	}

    //void OnGUI()
    //{
    //    var r = renderCam.pixelRect;
    //    var rect = new Rect(r.center.x + r.height / 6 + 50, r.center.y, 100, 100);
    //    GUI.Label (rect, "#" + ToHex(color.r) + ToHex(color.g) + ToHex(color.b));	
    //}

    //string ToHex(float n)
    //{
    //    return ((int)(n * 255)).ToString("X").PadLeft(2, '0');
    //}
}
