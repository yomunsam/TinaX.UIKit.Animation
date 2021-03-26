using System;
using TinaX.Tween.Components;
using TinaX.UIKit.Animation.Const;
using UniRx;
using UnityEngine;

namespace TinaX.UIKit.Animation
{
    [AddComponentMenu(UIAniConst.ComponentMenuBasePath + "RectTransform/Rotation Tween (EulerAngles)")]
    public class RectTransformRotationEulerAnglesTween : PingPongTweenRxComponentBase<RectTransform, Vector3>
    {
        public bool _LocalRotate = true;

        private Vector3? origin_value;
        private Vector3? target_value;

        private bool ready_flag = false; //如果执行过Ready，这里为true
        private bool valid_tween = true; //该组件的各项配置是否有效

        private bool pingpong_switch;

        public override bool Playing => this.TweenRxDisposable != null;

        public override RectTransform GetDefaultTarget()
        {
            if (this == null)
                return null;
            return this.GetComponent<RectTransform>();
        }

        public override void Ready()
        {
            if (ready_flag)
                return;
            ready_flag = true;

            if (Target == null)
            {
                Debug.LogError($"[TinaX.Tween]{nameof(RectTransformRotationEulerAnglesTween)} cannot get valid target.");
                valid_tween = false;
            }

            if (!this._AutoOriginValue)
            {
                if (_LocalRotate)
                    this.Target.localEulerAngles = this._FromValue;
                else
                    this.Target.eulerAngles = this._FromValue;

            }
            else
            {
                this._PingPong = false; //如果自动识别初始值，则不应该可以PingPong（规则是只有明确指定了初始值和目标值才可以PingPong）
                this._AutoTargetValue = false;
            }
            origin_value = this._AutoOriginValue ? (_LocalRotate ? this.Target.localEulerAngles : this.Target.eulerAngles) : this._FromValue;
            target_value = this._AutoTargetValue ? (_LocalRotate ? this.Target.localEulerAngles : this.Target.eulerAngles) : this._ToValue;

            TimeSpan_PingPongDelay = TimeSpan.FromSeconds(this.PingPongDelay);
            TimeSpan_PongDelay = TimeSpan.FromSeconds(this.PongDelay);

            valid_tween = true;
        }

        public override void BeginPlay()
        {
            if (Playing)
                return;

            if (!ready_flag)
                this.Ready();

            if (!valid_tween)
                return;

            if (origin_value.Value.Equals(target_value.Value))
            {
                this.Finish();
                return;
            }

            if (_LocalRotate)
            {
                this.TweenRxDisposable = TinaX.Tween.Tween.Play(
                    origin_value.Value,
                    target_value.Value,
                    this.Duration,
                    this._EaseType,
                    this.DelayBefore)
                    .Subscribe(value => { this.Target.localEulerAngles = value; }, tweenFinish)
                    .AddTo(this.Target);
            }
            else
            {
                this.TweenRxDisposable = TinaX.Tween.Tween.Play(
                    origin_value.Value,
                    target_value.Value,
                    this.Duration,
                    this._EaseType,
                    this.DelayBefore)
                    .Subscribe(value => { this.Target.eulerAngles = value; }, tweenFinish)
                    .AddTo(this.Target);
            }

        }


        private void tweenFinish()
        {
            if (this.PingPong)
            {
                this.pingpong_switch = !this.pingpong_switch;
                this.TweenRxDisposable?.Dispose();
                var obsv3 = TinaX.Tween.Tween.Play(!pingpong_switch ? this._FromValue : this._ToValue,
                    !pingpong_switch ? this._ToValue : this._FromValue,
                    this.Duration,
                    this._EaseType);
                //延迟处理
                if (!pingpong_switch)
                {
                    if (this.PingPongDelay > 0)
                        obsv3 = obsv3.Delay(TimeSpan_PingPongDelay);
                }
                else
                {
                    if (this.PongDelay > 0)
                        obsv3 = obsv3.Delay(TimeSpan_PongDelay);
                }

                if (_LocalRotate)
                {
                    this.TweenRxDisposable = obsv3.Subscribe(value => { this.Target.localEulerAngles = value; }, tweenFinish)
                        .AddTo(this.Target);
                }
                else
                {
                    this.TweenRxDisposable = obsv3.Subscribe(value => { this.Target.eulerAngles = value; }, tweenFinish)
                        .AddTo(this.Target);
                }
            }
            else
            {
                this.Finish();
            }
        }

    }

}

