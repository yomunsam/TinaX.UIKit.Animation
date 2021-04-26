using TinaX.UIKit.Animation;
using TinaXEditor.Tween.CustomEditors;
using TinaXEditor.Utils;
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

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            var _serializedObject = this.serializedObject;
            UIDraw.DrawTitle(this.Title);
            EditorGUIUtil.HorizontalLine(1, Color.gray);
            EditorGUILayout.Space();

            UIDraw.DrawTarget(ref _serializedObject);
            //UIDraw.DrawFromValue(ref _serializedObject);
            UIDraw.DrawFromValueSlider(ref _serializedObject, 0f, 1f);
            //UIDraw.DrawToValue(ref _serializedObject);
            UIDraw.DrawToValueSlider(ref _serializedObject, 0f, 1f);
            UIDraw.DrawAutoOriginValue(ref _serializedObject);
            UIDraw.DrawAutoTargetValue(ref _serializedObject);
            UIDraw.DrawSetAsOriginValueOrTargetValue(ref SetOriginValueOnClicked, ref SetTargetValueOnClicked);
            EditorGUILayout.Space();
            EditorGUIUtil.HorizontalLine(1, Color.gray);
            EditorGUILayout.Space();

            UIDraw.DrawDuration(ref _serializedObject);
            UIDraw.DrawTweenRxEaseValue(ref _serializedObject);
            UIDraw.DrawPlayOnAwake(ref _serializedObject);
            UIDraw.DrawDelayBefore(ref _serializedObject);

            EditorGUILayout.Space();
            EditorGUIUtil.HorizontalLine(1, Color.gray);
            EditorGUILayout.Space();

            UIDraw.DrawPingPong(ref _serializedObject);

            EditorGUILayout.Space();
            EditorGUIUtil.HorizontalLine(1, Color.gray);
            EditorGUILayout.Space();

            UIDraw.DrawDescription(ref _serializedObject);
            EditorGUILayout.Space();
            UIDraw.DrawEvents_FinishAndStop(ref _serializedObject);

            _serializedObject.ApplyModifiedProperties();
        }
    }
}
