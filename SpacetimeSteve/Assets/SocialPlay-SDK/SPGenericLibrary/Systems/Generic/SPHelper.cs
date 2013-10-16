using UnityEngine;
using System.Collections;

public class SPHelper
{
    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        foreach (Transform T in obj.GetComponentsInChildren<Transform>())
        {
            T.gameObject.layer = layer;
        }
    }

    public static string FormatStringToLength(string original, int length = 45, string newLineChar = "\n")
    {
        string formated = string.Empty;
        string[] words = original.Split(' ');
        int CurrentLineLength = 0;
        foreach (string word in words)
        {
            if (CurrentLineLength + word.Length >= length)//Starts a new line.
            {
                formated = formated.Insert(formated.Length, "\n");
                CurrentLineLength = 0;
            }
            else
            {
                if (formated.Length != 0)
                {
                    formated = formated.Insert(formated.Length, " ");
                    CurrentLineLength++;
                }
            }
            CurrentLineLength += word.Length;
            formated = formated.Insert(formated.Length, word);
        }

        return formated;
    }

    public static string SetTextColor(Color color)
    {
        return string.Format("[{0}]", NGUITools.EncodeColor(color));
    }

    public static string SetTextColor(string passedString, Color color)
    {
        return string.Format("[{0}]{1}[-]", NGUITools.EncodeColor(color), passedString);
    }

}

public static class MyExtensions
{
    public static T GetIComponent<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponent(typeof(T)) as T;
    }

    public static T GetIComponentInChildren<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponentInChildren(typeof(T)) as T;
    }
}
