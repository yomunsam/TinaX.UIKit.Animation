using TinaX.UIKit.Animation;
using TinaXEditor.Tween.CustomEditors;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TinaXEditor.UIKit.Animation.CustomEditors
{
    [CustomEditor(typeof(ImageColorTween))]

    public class ImageColorTweenCustomEditor : PingPongTweenRxComponentBaseCustomEditorGeneric
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            //定义标题
            switch (Application.systemLanguage)
            {
                default:
                    Title = "uGUI Image Color";
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    Title = "uGUI Image 颜色";
                    break;
            }


            //定义两个按钮

            if (SetOriginValueOnClicked == null)
                SetOriginValueOnClicked = (targetSP, fromSP) =>
                {
                    var image = targetSP.objectReferenceValue as Image;
                    if (image == null) 
                        return;
                    fromSP.colorValue = image.color;
                };

            if (SetTargetValueOnClicked == null)
                SetTargetValueOnClicked = (targetSP, toSP) =>
                {
                    var image = targetSP.objectReferenceValue as Image;
                    if (image == null) 
                        return;
                    toSP.colorValue = image.color;
                };
        }
    }
}
