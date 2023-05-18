using UnityEngine;

namespace RamenSea.Foundation3D.Extensions {
    public static class GeneralUnityExtensions {
        public static void CopyToClipboard(this string s) {
            GUIUtility.systemCopyBuffer = s;
            // TextEditor t = new TextEditor();
            // t.text = s;
            // t.SelectAll();
            // t.Copy();
        }
    }
}