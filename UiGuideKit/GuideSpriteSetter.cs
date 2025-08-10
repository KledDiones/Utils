#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class GuideSpriteSetter
{
    private const string GuideObjectName = "Guide";

    [MenuItem("Assets/Definir como guia", true)]
    private static bool Validate()
    {
        return GetSpriteFromSelection() != null;
    }

    [MenuItem("Assets/Definir como guia")]
    private static void SetAsGuide()
    {
        Sprite sprite = GetSpriteFromSelection();
        if (sprite == null) return;

        Transform guideTransform = GameObject.Find("Canvas")?.transform.Find("Guide");
        GameObject guideObj = guideTransform?.gameObject;
        if (guideObj == null)
        {
            Debug.LogWarning("Nenhum objeto chamado 'Guide' encontrado na cena.");
            return;
        }

        Image image = guideObj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogWarning("O objeto 'Guide' não possui um componente Image.");
            return;
        }

        image.sprite = sprite;

        // força stretch total com offset 0
        RectTransform rt = image.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
        }

        EditorUtility.SetDirty(image);
        Debug.Log("Sprite definido como guia com sucesso.");
    }

    private static Sprite GetSpriteFromSelection()
    {
        var obj = Selection.activeObject;
        if (obj is Sprite sprite)
            return sprite;

        if (obj is Texture2D tex)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            var all = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
            foreach (var subAsset in all)
            {
                if (subAsset is Sprite s)
                    return s;
            }
        }
        return null;
    }
}
#endif