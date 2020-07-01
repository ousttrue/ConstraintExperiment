using System;
using UnityEngine;


namespace ConstraintExperiment
{
    /// <summary>
    /// 対象の初期位置と現在位置の差分(delta)を、自身の初期位置に対してWeightを乗算して加算する。
    /// </summary>
    [DisallowMultipleComponent]
    public class PositionConstraint : MonoBehaviour
    {
        [SerializeField]
        Transform Source;

        /// <summary>
        /// 動作中に変更した場合は動作不定
        /// </summary>
        [SerializeField]
        ConstraintCoordinates FreezeCoordinates;

        [SerializeField]
        AxesMask FreezeAxes;

        [SerializeField]
        [Range(0, 10.0f)]
        float Weight = 1.0f;

        Vector3 m_sourceInitial;
        Vector3 m_worldInitial;
        Vector3 m_localInitial;
        // Matrix4x4 m_worldToLocal;

        void Start()
        {
            if (Source == null)
            {
                return;
            }
            m_sourceInitial = Source.position;
            m_worldInitial = transform.position;
            m_localInitial = transform.localPosition;
        }

        /// <summary>
        /// SourceのUpdateよりも先か後かはその時による。
        /// 厳密に制御するのは無理。
        /// </summary>
        void Update()
        {
            if (Source == null)
            {
                return;
            }

            switch (FreezeCoordinates)
            {
                case ConstraintCoordinates.World:
                    {
                        var delta = Source.position - m_sourceInitial;
                        transform.position = m_worldInitial + FreezeAxes.Freeze(delta * Weight);
                        break;
                    }

                case ConstraintCoordinates.Local:
                    {
                        var delta = Source.position - m_sourceInitial;
                        var localDelta = Quaternion.Inverse(transform.ParentRotation()) * delta;
                        transform.localPosition = m_localInitial + FreezeAxes.Freeze(localDelta * Weight);
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
