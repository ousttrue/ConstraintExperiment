using UnityEngine;
using System;

namespace ConstraintExperiment
{
    class ConstraintSource
    {
        readonly Transform m_transform;

        readonly ConstraintCoordinates m_coords;

        readonly TRS m_initial;

        public Vector3 TranslationDelta
        {
            get
            {
                switch (m_coords)
                {
                    case ConstraintCoordinates.World: return m_transform.position - m_initial.Translation;
                    case ConstraintCoordinates.Local: return m_transform.localPosition - m_initial.Translation;
                    default: throw new NotImplementedException();
                }
            }
        }

        public Quaternion RotationDelta
        {
            get
            {
                switch (m_coords)
                {
                    // 右からかけるか、左からかけるか、それが問題なのだ
                    case ConstraintCoordinates.World: return m_transform.rotation * Quaternion.Inverse(m_initial.Rotation);
                    case ConstraintCoordinates.Local: return m_transform.localRotation * Quaternion.Inverse(m_initial.Rotation);
                    default: throw new NotImplementedException();
                }
            }
        }

        public ConstraintSource(Transform t, ConstraintCoordinates coords)
        {
            m_transform = t;
            m_coords = coords;

            switch (coords)
            {
                case ConstraintCoordinates.World:
                    m_initial = TRS.GetWorld(t);
                    break;

                case ConstraintCoordinates.Local:
                    m_initial = TRS.GetLocal(t);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
