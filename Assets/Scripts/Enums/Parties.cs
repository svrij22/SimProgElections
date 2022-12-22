using UnityEngine;

public enum Parties
{
    None,
    Gray,
    Green,
    Red,
    Yellow,
    Blue
}
public static class ColourHelper
{
    public static Material Load(Parties color)
    {
        var mat = Resources.Load($"Materials/{color}", typeof(Material)) as Material;
        return mat;
    }
}