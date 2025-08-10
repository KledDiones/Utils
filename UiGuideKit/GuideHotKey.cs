#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

[InitializeOnLoad]
public static class GuideHotkey
{
    static bool guideVisible = false;
    static float guideAlpha = 0.5f;
    static GameObject guideObj;

    static bool altHeld = false;
    static bool guideToggled = false;

    static double lastAltTime = 0;
    const double doubleTapTime = 0.3;

    static GuideHotkey()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView view)
    {
        Event e = Event.current;
        if (e == null) return;

        if (guideObj == null)
            guideObj = GameObject.Find("Guide");

        // Detecta pressionar ALT
        if (e.type == EventType.KeyDown && e.alt)
        {
            double now = EditorApplication.timeSinceStartup;
            if (now - lastAltTime < doubleTapTime)
            {
                guideToggled = !guideToggled;
            }
            lastAltTime = now;
            altHeld = true;
        }

        // Libera ALT
        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftAlt)
        {
            altHeld = false;
        }

        // Ajuste alpha
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Equals || e.keyCode == KeyCode.KeypadPlus)
                guideAlpha = Mathf.Min(1f, guideAlpha + 0.1f);
            if (e.keyCode == KeyCode.Minus || e.keyCode == KeyCode.KeypadMinus)
                guideAlpha = Mathf.Max(0f, guideAlpha - 0.1f);
        }

        bool shouldShow = altHeld || guideToggled;

        if (guideObj != null)
        {
            guideObj.hideFlags = HideFlags.None;
            guideObj.SetActive(shouldShow);

            var img = guideObj.GetComponent<Image>();
            if (img != null)
            {
                var c = img.color;
                c.a = guideAlpha;
                img.color = c;
                EditorUtility.SetDirty(img);
            }
        }

        SceneView.RepaintAll();
    }
}
#endif