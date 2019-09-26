using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEyeEffect : MonoBehaviour
{
    public Material mat;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
