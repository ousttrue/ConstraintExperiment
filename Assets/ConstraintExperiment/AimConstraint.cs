using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConstraintExperiment
{
    /// <summary>
    /// WIP
    /// 
    /// Slerp(thisRot * offsetRot(Aim x Up), sourceRot, weight)
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public class AimConstraint : MonoBehaviour
    {
        [SerializeField]
        Transform Source;

        [SerializeField]
        AxesMask FreezeAxes;

        [SerializeField]
        [Range(0, 10.0f)]
        float Weight = 1.0f;

        Quaternion m_selfInitial;

        /// <summary>
        /// Forward
        /// </summary>
        [SerializeField]
        Vector3 AimVector;

        /// <summary>
        /// Up
        /// </summary>
        [SerializeField]
        Vector3 UpVector;

        void Start()
        {
            if (Source is null)
            {
                return;
            }

            // world space
            m_selfInitial = transform.rotation;

            // AimVector, UpVectorから正規直行基底を作り出す
            AimVector.Normalize();
            UpVector.Normalize();
            var right = Vector3.Cross(UpVector, AimVector);
            UpVector = Vector3.Cross(AimVector, right);

            // matrix を得る
            // このオブジェクトとMatrixの回転差分を得る
        }

        /// <summary>
        /// TargetのUpdateよりも先か後かはその時による。
        /// 厳密に制御するのは無理。
        /// </summary>
        void Update()
        {
            if (Source is null)
            {
                return;
            }

            // Sourceに対する回転を得る
            // その回転と初期回転との差分を得る
            // SLERPする
        }
    }
}
