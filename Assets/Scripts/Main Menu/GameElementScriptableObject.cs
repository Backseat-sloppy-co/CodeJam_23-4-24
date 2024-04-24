using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameElement", menuName = "ScriptableObjects/GameElement", order = 1)]
public class GameElementScriptableObject : ScriptableObject
{
    public string title;
    public Sprite icon;
    public string sceneName;
}
