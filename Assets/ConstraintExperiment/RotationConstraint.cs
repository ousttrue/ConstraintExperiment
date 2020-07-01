using UnityEngine;


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
        ConstraintCoordinates FreezeCoordinates;

        [SerializeField]
        AxesMask FreezeAxes;

        [SerializeField]
        [Range(0, 10.0f)]
        float Weight = 1.0f;

        Quaternion m_sourceInitial;
        Quaternion m_worldInitial;
        Quaternion m_localInitial;

        void Start()
        {
            if (Source == null)
            {
                enabled = false;
                return;
            }

            m_sourceInitial = Source.rotation;
            m_worldInitial = transform.rotation;
            m_localInitial = transform.localRotation;
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

            switch (FreezeCoordinates)
            {
                case ConstraintCoordinates.World:
                    {
                        var delta = Quaternion.Inverse(m_sourceInitial) * Source.rotation;
                        var feezed = FreezeAxes.Freeze(delta.eulerAngles);
                        transform.rotation = Quaternion.LerpUnclamped(m_worldInitial, m_worldInitial * Quaternion.Euler(feezed), Weight);
                        break;
                    }

                case ConstraintCoordinates.Local:
                    {
                        var delta = Quaternion.Inverse(m_sourceInitial) * Source.rotation;
                        var localDelta = Quaternion.Inverse(transform.ParentRotation()) * delta;
                        var freezed = FreezeAxes.Freeze(localDelta.eulerAngles);
                        transform.localRotation = Quaternion.LerpUnclamped(m_localInitial, m_localInitial * Quaternion.Euler(freezed), Weight);
                        break;
                    }
            }
        }
    }
}
