﻿using UnityEngine;


namespace ConstraintExperiment
{
    /// <summary>
    /// 対象の初期回転と現在回転の差分(delta)を、自身の初期回転と自身の初期回転にdeltaを乗算したものに対してWeightでSlerpする。
    /// </summary>
    [DisallowMultipleComponent]
    public class RotationConstraint : MonoBehaviour
    {
        [SerializeField]
        Transform Source;

        [SerializeField]
        SourceCoordinates SourceCoordinate;

        [SerializeField]
        DestinationCoordinates DestinationCoordinate;

        [SerializeField]
        AxesMask FreezeAxes;

        [SerializeField]
        [Range(0, 10.0f)]
        float Weight = 1.0f;

        [SerializeField]
        Transform ModelRoot;

        ConstraintSource m_src;

        ConstraintDestination m_dst;

        /// <summary>
        /// Editorで設定値の変更を反映するために、クリアする
        /// </summary>
        void OnValidate()
        {
            Debug.Log("Validate");
            m_src = null;
            m_dst = null;
        }

        /// <summary>
        /// SourceのUpdateよりも先か後かはその時による。
        /// 厳密に制御するのは無理。
        /// </summary>
        void Update()
        {
            if (Source == null)
            {
                enabled = false;
                return;
            }

            if (m_src == null)
            {
                m_src = new ConstraintSource(Source, SourceCoordinate, ModelRoot);
            }
            if (m_dst == null)
            {
                m_dst = new ConstraintDestination(transform, DestinationCoordinate);
            }

            // 軸制限をしたオイラー角
            var delta = m_src.RotationDelta;
            var fleezed = FreezeAxes.Freeze(delta.eulerAngles);
            var rotation = Quaternion.Euler(fleezed);
            Debug.Log($"{delta} => {rotation}");
            // オイラー角を再度Quaternionへ。weight を加味してSlerpする
            m_dst.ApplyRotation(rotation, Weight);
        }
    }
}
