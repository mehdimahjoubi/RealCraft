using UnityEngine;
using System.Collections;

public class ColorHexConverter
{

    public static string RGB_To_Hex(Color color)
    {
        var red = color.r * 255;
        var green = color.g * 255;
        var blue = color.b * 255;
        var a = GetHex((int)Mathf.Floor(red / 16));
        var b = GetHex((int)Mathf.Round(red % 16));
        var c = GetHex((int)Mathf.Floor(green / 16));
        var d = GetHex((int)Mathf.Round(green % 16));
        var e = GetHex((int)Mathf.Floor(blue / 16));
        var f = GetHex((int)Mathf.Round(blue % 16));
        var z = a + b + c + d + e + f;
        return z;
    }

    public static Color HexToRGB(string color)
    {
        var red = (HexToInt(color[1]) + HexToInt(color[0]) * 16.000) / 255;
        var green = (HexToInt(color[3]) + HexToInt(color[2]) * 16.000) / 255;
        var blue = (HexToInt(color[5]) + HexToInt(color[4]) * 16.000) / 255;
        var finalColor = new Color();
        finalColor.r = (float)red;
        finalColor.g = (float)green;
        finalColor.b = (float)blue;
        finalColor.a = 1;
        return finalColor;
    }

    static string GetHex(int v)
    {
        var alpha = "0123456789ABCDEF";
        var result = "" + alpha[v];
        return result;
    }

    static int HexToInt(char hexChar)
    {
        var hex = "" + hexChar;
        switch (hex)
        {
            case "0": return 0;
            case "1": return 1;
            case "2": return 2;
            case "3": return 3;
            case "4": return 4;
            case "5": return 5;
            case "6": return 6;
            case "7": return 7;
            case "8": return 8;
            case "9": return 9;
            case "A": return 10;
            case "B": return 11;
            case "C": return 12;
            case "D": return 13;
            case "E": return 14;
            case "F": return 15;
            default: return 0;
        }
    }

}
