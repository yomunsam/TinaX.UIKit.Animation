using System.Collections;
using System.Collections.Generic;
using TinaX.Tween.Components;
using TinaX.UIKit.Animation.Const;
using UnityEngine;

namespace TinaX.UIKit.Animation
{
    [AddComponentMenu(UIAniConst.ComponentMenuBasePath + "UI Page Animation")]
    [RequireComponent(typeof(UIPage))]
    public class UIPageAnimationComponents : MonoBehaviour
    {
        public TweenComponentBase OnUIOpen;
        public TweenComponentBase OnUIShow;
        public TweenComponentBase OnUIHide;
        public TweenComponentBase OnUIClose;

        private UIPage m_UIPage;
        private TweenComponentBase m_CurrentTween;


        protected virtual void Awake()
        {
            m_UIPage = this.GetComponent<UIPage>();


            m_UIPage.OnCloseUI += OnCloseUIEvent;
            m_UIPage.OnDestroyUI += OnDestoryUIEvent;

            if(OnUIClose != null)
            {
                m_UIPage.DestroyDelay = OnUIClose.Duration + OnUIClose.DelayBefore;
            }

            this.OnOpenUIEvent(); //此时此刻，UIPage的OpenUI事件已经触发过了，所以无法像其他事件一样由UIPage来触发OpenUI
        }


        protected virtual void OnOpenUIEvent()
        {
            if(OnUIOpen != null)
            {
                if(m_CurrentTween == null)
                {
                    m_CurrentTween = OnUIOpen;
                    if (!OnUIOpen.Playing)
                    {
                        OnUIOpen.OnFinish += OnTweenFinishOrStop;
                        OnUIOpen.OnStop += OnTweenFinishOrStop;
                        OnUIOpen.BeginPlay();
                    }
                }
            }
        }

        protected virtual void OnShowUIEvent()
        {
        }

        protected virtual void OnHideUIEvent()
        {

        }

        protected virtual void OnCloseUIEvent(float destroyDelay)
        {
            //Debug.Log("[UI Page Ani]关闭UI事件到了，延迟:" + destroyDelay);
            if(OnUIClose != null)
            {
                if (m_CurrentTween != null && m_CurrentTween != OnUIClose)
                    m_CurrentTween.Stop();

                if(destroyDelay > 0)
                {
                    m_CurrentTween = OnUIClose;
                    if (!OnUIClose.Playing)
                    {
                        OnUIClose.OnFinish += OnTweenFinishOrStop;
                        OnUIClose.OnStop += OnTweenFinishOrStop;
                        //Debug.Log("[UI Page Ani]开始播动画");
                        OnUIClose.BeginPlay();
                    }
                }
            }
        }

        protected virtual void OnDestoryUIEvent()
        {
            //Debug.Log("正儿八经要销毁UI了现在");
        }


        private void OnTweenFinishOrStop()
        {
            if(m_CurrentTween != null)
            {
                m_CurrentTween.OnFinish -= OnTweenFinishOrStop;
                m_CurrentTween.OnStop -= OnTweenFinishOrStop;
            }
            m_CurrentTween = null;
        }
        

    }
}

