using System.Collections;
using System.IO;
using System;
using UnityEngine;

public class Texture2D_stringConverter
{

    public static Texture2D Base64ToTexture2D(string base64String)
    {
        var tex = new Texture2D(4, 4);
        byte[] png = Convert.FromBase64String(base64String);
        tex.LoadImage(png);
        return tex;
    }

    public static string Texture2D_To_Base64(Texture2D tex)
    {
        return Convert.ToBase64String(tex.EncodeToPNG());
    }

    //---------------- solution without base64 encoding-----------

    public static Texture2D PNG_To_Texture2D(byte[] png)
    {
        var tex = new Texture2D(4, 4);
        tex.LoadImage(png);
        return tex;
    }

    public static byte[] Texture2D_To_PNG(Texture2D tex)
    {
        return tex.EncodeToPNG();
    }

    //--------------------inspiration...--------------------------

    //public static string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    //{
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        // Convert Image to byte[]
    //        image.Save(ms, format);
    //        byte[] imageBytes = ms.ToArray();

    //        // Convert byte[] to Base64 String
    //        string base64String = Convert.ToBase64String(imageBytes);
    //        return base64String;
    //    }
    //}

    //public static System.Drawing.Image Base64ToImage(string base64String)
    //{
    //    // Convert Base64 String to byte[]
    //    byte[] imageBytes = Convert.FromBase64String(base64String);
    //    MemoryStream ms = new MemoryStream(imageBytes, 0,
    //      imageBytes.Length);

    //    // Convert byte[] to Image
    //    ms.Write(imageBytes, 0, imageBytes.Length);
    //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
    //    return image;
    //}

}
