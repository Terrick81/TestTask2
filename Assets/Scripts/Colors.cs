using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType
{
    green,
    yellow,
    cyan,
    red,
    orange,
    purple,
    white,
    gray
}

public class Colors
{
    public static int colorsCount;

    internal static void Awake()
    {
        colorsCount = ColorType.GetNames(typeof(ColorType)).Length;
    }

    public static Color GetColorInColection(ColorType cl, float opacity = 1f)
    {
        switch (cl)
        {
            case ColorType.green: return new Color(0.25f, 0.64f, 0.12f, opacity);
            case ColorType.yellow: return new Color(0.84f, 0.74f, 0.11f, opacity);
            case ColorType.cyan: return new Color(0.19f, 0.62f, 0.85f, opacity);
            case ColorType.red: return new Color(0.77f, 0.27f, 0.27f, opacity);
            case ColorType.orange: return new Color(0.81f, 0.48f, 0.16f, opacity);
            case ColorType.purple: return new Color(0.65f, 0.34f, 0.83f, opacity);
            case ColorType.white: return new Color(1f, 1f, 1f, opacity);
            case ColorType.gray: return new Color(0.59f, 0.59f, 0.59f, opacity);
        }
        return Color.black;
    }
}
