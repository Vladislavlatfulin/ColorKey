using UnityEngine;

public static class ColorExt {
    private static string ToHexString(this Color32 c){
        return string.Format ("{0:X2}{1:X2}{2:X2}", c.r, c.g, c.b);
    }
	
    public static string ToHexString(this Color color){
        Color32 c = color;
        return c.ToHexString();
    }

    private static int ToHex(this Color32 c){
        return (c.r<<16)|(c.g<<8)|(c.b);
    }
	
    public static int ToHex(this Color color){
        Color32 c = color;
        return c.ToHex();
    }
}