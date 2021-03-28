using TinaX.UIKit.Animation;
using TinaXEditor.Tween.CustomEditors;
using UnityEditor;
using UnityEngine;

namespace TinaXEditor.UIKit.Animation.CustomEditors
{
    [CustomEditor(typeof(CanvasGroupAlphaTween))]
    public class CanvasGroupAlphaTweenCustomEditor : PingPongTweenRxComponentBaseCustomEditorGeneric
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            //定义标题
            switch (Application.systemLanguage)
            {
                default:
                    Title = "Canvas Group Alpha";
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    Title = "Canvas Group 透明通道";
                    break;
            }


            //定义两个按钮

            if (SetOriginValueOnClicked == null)
                SetOriginValueOnClicked = (targetSP, fromSP) =>
                {
                    var cg = targetSP.objectReferenceValue as CanvasGroup;
                    if (cg == null)
                        return;
                    fromSP.floatValue = cg.alpha;
                };

            if (SetTargetValueOnClicked == null)
                SetTargetValueOnClicked = (targetSP, toSP) =>
                {
                    var cg = targetSP.objectReferenceValue as CanvasGroup;
                    if (cg == null)
                        return;
                    toSP.floatValue = cg.alpha;
                };
        }
    }
}
