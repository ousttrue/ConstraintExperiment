using UnityEngine;
using System;

namespace ConstraintExperiment
{
    class ConstraintSource
    {
        readonly Transform m_src;

        readonly ConstraintCoordinates m_coords;

        readonly TRS m_initial;

        public Vector3 TranslationDelta
        {
            get
            {
                switch (m_coords)
                {
                    case ConstraintCoordinates.Local: return m_src.localPosition - m_initial.Translation;
                    case ConstraintCoordinates.World: return m_src.position - m_initial.Translation;
                    default: throw new NotImplementedException();
                }
            }
        }

        public ConstraintSource(Transform t, ConstraintCoordinates coords)
        {
            m_src = t;
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
