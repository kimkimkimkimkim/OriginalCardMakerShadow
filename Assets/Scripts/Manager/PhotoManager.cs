using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using System;
using UniRx;

public class PhotoManager : SingletonMonoBehaviour<PhotoManager>
{
    /// <summary>
    /// 撮影して取得した画像を切り取って返します
    /// </summary>
    /// <param name="aspectRatio">切り取る際のアス比</param>
    /// <param name="callBackAction">切り取り後のテクスチャを引数とした処理</param>
    public void TakePhotoAndCrop(float aspectRatio, Action<Texture> callBackAction)
    {
        if (NativeCamera.IsCameraBusy()) return;

        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Crop(texture, aspectRatio, callBackAction);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                // Destroy(texture, 5f);
            }
            else
            {
                Debug.Log("=== pathがありません ===");
            }
        });

        if (permission == NativeCamera.Permission.Denied)
        {
            // カメラ使用許可が拒否されています
            CommonDialogFactory.Create(new CommonDialogRequest() { title = "確認", body = "カメラの使用を許可してください" })
                .Subscribe();
        }
    }

    /// <summary>
    /// カメラロールから取得した画像を切り取って返します
    /// </summary>
    /// <param name="aspectRatio">切り取る際のアス比</param>
    /// <param name="callBackAction">切り取り後のテクスチャを引数とした処理</param>
    public void PickAndCrop(float aspectRatio, Action<Texture> callBackAction)
    {
        if (NativeGallery.IsMediaPickerBusy()) return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Crop(texture, aspectRatio, callBackAction);

                // Destroy(texture, 5f);
            }
            else
            {
                Debug.Log("==== pathがありません ====");
            }
        }, "Select a PNG image", "image/png");

        if (permission == NativeGallery.Permission.Denied)
        {
            // アルバム使用許可が拒否されています
            CommonDialogFactory.Create(new CommonDialogRequest() { title = "確認", body = "アルバムの使用を許可してください" })
                .Subscribe();
        }
    }

    private void Crop(Texture2D texture, float aspectRatio, Action<Texture> callBackAction)
    {
        ImageCropper.Instance.Show(texture, (bool result, Texture originalImage, Texture2D croppedImage) =>
        {
            // If screenshot was cropped successfully
            if (result)
            {
                Debug.Log("===== クロップ完了 ========");
                callBackAction(croppedImage);
            }
            else
            {
                Debug.Log("===== クロップできませんでした ========");
            }

            // Destroy the screenshot as we no longer need it in this case
            Destroy(texture);
        },
        settings: new ImageCropper.Settings()
        {
            ovalSelection = false,
            autoZoomEnabled = true,
            imageBackground = Color.clear, // transparent background
            selectionMinAspectRatio = aspectRatio,
            selectionMaxAspectRatio = aspectRatio

        },
        croppedImageResizePolicy: (ref int width, ref int height) =>
        {
            // uncomment lines below to save cropped image at half resolution
            //width /= 2;
            //height /= 2;
        });
    }
}
