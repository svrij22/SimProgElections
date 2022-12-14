using UnityEngine;

public enum Colours
{
    Gray,
    Green,
    Red,
    Yellow,
    Blue
}
public static class ColourHelper
{
    public static Material Load(Colours color)
    {
        var mat = Resources.Load($"Materials/{color}", typeof(Material)) as Material;
        return mat;
    }
}