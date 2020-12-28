using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACHE
{
    /// <summary>
    /// Temporary string values that can be saved and loaded on command. Each value defaults to its index so we can error check.
    /// be warned that if your story depends on these values, as if a chapter includes these values in its dialogue past the input screen,
    /// you may want to save this string array into the file as well. It may be wise to save this list into your game files and reload
    /// when the game is loaded back. This just makes it easier to save variables without requiring a variable for each value.
    /// </summary>
    public static string[] tempVals = new string[9]
    {
        "1","2","3","4","5","6","7","8","9"
    };
}
