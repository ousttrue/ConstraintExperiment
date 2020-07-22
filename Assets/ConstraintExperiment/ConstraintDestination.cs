using System;
using UnityEngine;

namespace ConstraintExperiment
{
    class ConstraintDestination
    {
        readonly Transform m_dst;
        readonly ConstraintCoordinates m_coords;

        readonly TRS m_initial;

        public ConstraintDestination(Transform t, ConstraintCoordinates coords)
        {
            m_dst = t;
            m_coords = coords;

            switch (m_coords)
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

        public void ApplyTranslation(Vector3 delta, float weight)
        {
            switch (m_coords)
            {
                case ConstraintCoordinates.World:
                    m_dst.position = m_initial.Translation + delta * weight;
                    break;

                case ConstraintCoordinates.Local:
                    m_dst.localPosition = m_initial.Translation + delta * weight;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
