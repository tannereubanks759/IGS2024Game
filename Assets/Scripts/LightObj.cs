using UnityEngine;
using System.Collections;


[System.Serializable]
[CreateAssetMenu(fileName = "LightObj", menuName = "Scriptables/LightObj", order = 1)]
public class LightObj : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
