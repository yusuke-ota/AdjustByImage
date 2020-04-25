using AdjustByImage.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Profiling;

namespace AdjustByImage.Examples.AdjustByImageDemo
{
    [RequireComponent(typeof(ARTrackedImageManager), typeof(ObjectFitting))]
    public class AdjustByImageDemo : MonoBehaviour
    {
        [SerializeField] [Tooltip("動かしたいオブジェクト")]
        private GameObject arObjects;

        #region Values

        private ObjectFitting objectFitting;
        private ARTrackedImageManager arTrackedImageManager;
        private ARTrackedImage trackingImage;

        #endregion

        private void Awake()
        {
            arTrackedImageManager = GetComponent<ARTrackedImageManager>();
            objectFitting = GetComponent<ObjectFitting>();
        }

        private void OnEnable()
        {
            arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        private void OnDisable()
        {
            arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            trackingImage = null;
            
            // // 画像を新規認識時に、位置合わせを行う
            // foreach (var trackedImage in eventArgs.added)
            // {
            //     moveSessionOrigin.move_arObjects_parallel(arObjects, trackedImage);
            // }

            // Tracking数を1に設定すること
            foreach (var trackedImage in eventArgs.updated)
            {
                if (trackedImage.trackingState != TrackingState.Tracking) continue;
                trackingImage = trackedImage;
            }

            if (trackingImage == null) return;
            
            Profiler.BeginSample("位置合わせのコスト");
            objectFitting.AlignObjectsHighFrequency(arObjects, trackingImage, 0.5f, true);
            Profiler.EndSample();
        }
    }
}