using TinaX.UIKit.Animation;
using TinaXEditor.Tween.CustomEditors;
using UnityEditor;
using UnityEngine;

namespace TinaXEditor.UIKit.Animation.CustomEditors
{
    [CustomEditor(typeof(RectTransformSizeDeltaTween))]
    public class RectTransformSizeDeltaTweenCustomEditor : PingPongTweenRxComponentBaseCustomEditorGeneric
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            //定义标题
            switch (Application.systemLanguage)
            {
                default:
                    Title = "SizeDelta";
                    break;
            }


            //定义两个按钮

            if (SetOriginValueOnClicked == null)
                SetOriginValueOnClicked = (targetSP, fromSP) =>
                {
                    var rectTrans = targetSP.objectReferenceValue as RectTransform;
                    if (rectTrans == null)
                        return;
                    fromSP.vector2Value = rectTrans.sizeDelta;
                };

            if (SetTargetValueOnClicked == null)
                SetTargetValueOnClicked = (targetSP, toSP) =>
                {
                    var rectTrans = targetSP.objectReferenceValue as RectTransform;
                    if (rectTrans == null)
                        return;
                    toSP.vector2Value = rectTrans.sizeDelta;
                };
        }
    }
}
