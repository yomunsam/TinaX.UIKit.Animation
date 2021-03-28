using TinaX.UIKit.Animation;
using TinaXEditor.Tween.CustomEditors;
using TinaXEditor.Utils;
using UnityEditor;
using UnityEngine;

namespace TinaXEditor.UIKit.Animation.CustomEditors
{
    [CustomEditor(typeof(RectTransformRotationEulerAnglesTween))]
    public class RectTransformRotationEulerAnglesTweenCustomEditor : PingPongTweenRxComponentBaseCustomEditorGeneric
    {
        private SerializedProperty _localRotate;
        private GUIContent GC_LocalRotate;


        protected override void OnEnable()
        {
            base.OnEnable();

            //定义标题
            switch (Application.systemLanguage)
            {
                default:
                    Title = "Rotation (EulerAngles)";
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    Title = "旋转（欧拉角）";
                    break;
            }

            if (SetOriginValueOnClicked == null)
                SetOriginValueOnClicked = (targetSP, fromSP) =>
                {
                    var rectTrans = targetSP.objectReferenceValue as RectTransform;
                    if (_localRotate != null)
                    {
                        if (_localRotate.boolValue)
                            fromSP.vector3Value = rectTrans.localEulerAngles;
                        else
                            fromSP.vector3Value = rectTrans.eulerAngles;
                    }
                    else
                        fromSP.vector3Value = rectTrans.localEulerAngles;
                };

            if(SetTargetValueOnClicked == null)
                SetTargetValueOnClicked = (targetSP, toSP) =>
                {
                    var rectTrans = targetSP.objectReferenceValue as RectTransform;
                    if (_localRotate != null)
                    {
                        if (_localRotate.boolValue)
                            toSP.vector3Value = rectTrans.localEulerAngles;
                        else
                            toSP.vector3Value = rectTrans.eulerAngles;
                    }
                    else
                        toSP.vector3Value = rectTrans.localEulerAngles;
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
            UIDraw.DrawFromValue(ref _serializedObject);
            UIDraw.DrawToValue(ref _serializedObject);
            UIDraw.DrawAutoOriginValue(ref _serializedObject);
            UIDraw.DrawAutoTargetValue(ref _serializedObject);
            UIDraw.DrawSetAsOriginValueOrTargetValue(ref SetOriginValueOnClicked, ref SetTargetValueOnClicked);
            this.DrawLocalRotate(ref _serializedObject);
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

        private void DrawLocalRotate(ref SerializedObject serializedObject)
        {
            if (_localRotate == null && serializedObject != null)
                _localRotate = serializedObject.FindProperty("_LocalRotate");

            if (GC_LocalRotate == null)
            {
                switch (Application.systemLanguage)
                {
                    default:
                        GC_LocalRotate = new GUIContent("Local Rotate");
                        break;
                    case SystemLanguage.Chinese:
                    case SystemLanguage.ChineseSimplified:
                        GC_LocalRotate = new GUIContent("本地坐标旋转");
                        break;
                }
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_localRotate, GC_LocalRotate, true);
            EditorGUILayout.EndHorizontal();
        }
    }
}

