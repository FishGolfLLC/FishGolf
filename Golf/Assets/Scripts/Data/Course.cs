using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Course", menuName = "Data Objects/Course")]
public class Course : ScriptableObject
{
    [SerializeField]
    string courseName;

    [SerializeField]
    Image courseIcon;

    [HideInInspector, SerializeField, Tooltip("For pushing updates to content. Only update when pushing a new version to builds. HIDDEN FOR NOW")]
    int version = 1;

    [SerializeField]
    List<Hole> holes;

    public string Name => courseName;
    public Image Icon => courseIcon;
    public int Version => version;
    public List<Hole> Holes => holes;

    public List<Hole> GetRandomHoles(int amount)
    {
        if (amount > holes.Count)
        {
            Debug.LogWarning($"[Course Data] Tried getting {amount} holes but I only have {holes.Count}!", this);
            return holes;
        }
        else if (amount <= 0)
        {
            Debug.LogWarning($"[Course Data] Received amount of {amount}. Defaulting to 1.");
        }

        amount = Mathf.Clamp(amount, 1, holes.Count);
        var random = new System.Random();
        return holes.OrderBy(x => random.Next()).Take(amount).ToList();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var hole in holes) hole.OnValidate();
    }
#endif
}

[Serializable]
public class Hole
{
    public const string EMPTYSCENE = "empty";

    public string name = "Replace";
    public int par = 3;
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public string sceneStr = EMPTYSCENE;
#if UNITY_EDITOR
    public SceneAsset scene;

    // Hole.scene should be able to handle initializing of Hole on open

    public void OnValidate()
    {
        if (!scene)
        {
            if (sceneStr != EMPTYSCENE)
                sceneStr = EMPTYSCENE;
        }
        else if (sceneStr != scene.name)
        {
            sceneStr = scene.name;
        }
    }
#endif
}

#if UNITY_EDITOR
public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif