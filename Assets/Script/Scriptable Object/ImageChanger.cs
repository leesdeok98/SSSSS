using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ImageChanger")]

public class ImageChanger : ScriptableObject
{
    // public string objectName;
    public Sprite storyImage;
    public string storyText;
}