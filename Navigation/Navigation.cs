using UnityEngine;

namespace Common.WebGL.Network
{
    public class Navigation : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void LoadURL(string url);
#endif

        public static void GoToPage(string url)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadURL(url);
#else
            Debug.Log("Ir para: " + url);
#endif
        }
    }
}