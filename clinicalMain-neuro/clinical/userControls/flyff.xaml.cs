﻿using clinical.userControls.dicomUtil;
using Dicom;
using Dicom.Imaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace clinical.userControls
{
    /// <summary>
    /// Interaction logic for flyff.xaml
    /// </summary>
    public partial class flyff : UserControl
    {
        private const float MIN_ZOOMRATIO = 0.2f;
        private const float MAX_ZOOMRATIO = 3f;
        private const float ZOOM_STEP = 0.2f;

        private byte[] raw8BitBuffer;
        private byte[] raw16BitBuffer;
        private int width;
        private int height;
        private int bits;
        private double ww;
        private double wl;

        private float currentRatio = 1.0f;

        public bool HasImage { get; set; } = false;
        private Point LastZoomPoint { get; set; }

        public flyff()
        {
            InitializeComponent();
        }

        /// <summary>
        /// open and show dicom file
        /// </summary>
        /// <param name="dicmFile"></param>
        /// <returns></returns>
        public bool OpenImage(string dicomFile)
        {
            try
            {
                var image = new DicomImage(dicomFile);
#pragma warning disable CS0618
                var pixelData = image.PixelData.GetFrame(0).Data;

                this.bits = image.Dataset.Get<int>(DicomTag.BitsStored);
                this.width = image.Width;
                this.height = image.Height;
                this.ww = image.WindowWidth;
                this.wl = image.WindowCenter;

                SetWindowInfo(ww, wl);

                if (bits > 8)
                {
                    raw16BitBuffer = new byte[width * height * 2];
                    Array.Copy(pixelData, raw16BitBuffer, pixelData.Length);
                }
                else
                {
                    raw8BitBuffer = new byte[width * height];
                    Array.Copy(pixelData, raw8BitBuffer, pixelData.Length);
                }

                var writeableBitmap = ConvertUtil.GetWriteableBitmap(pixelData, this.width, this.height, this.bits);
                var imageSource = ConvertUtil.GetImageSource(writeableBitmap);

                this.image.Source = imageSource;

                HasImage = true;
                ResetZoomPoint();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CloseImage()
        {
            try
            {
                this.image.Source = null;
                HasImage = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SetWindowInfo(double ww, double wl)
        {
            this.lbl_WL.Content = $"WL:{wl}";
            this.lbl_WW.Content = $"WW:{ww}";
        }

        public void ShowWindowInfo()
        {
            stackpanel_Window.Visibility = Visibility.Visible;
        }

        public void HideWindowInfo()
        {
            stackpanel_Window.Visibility = Visibility.Hidden;
        }

        private void ZoomImage(Point point, int delta)
        {
            if (delta == 0)
                return;

            if (delta < 0 && scaleTransform.ScaleX < MIN_ZOOMRATIO)
                return;

            if (delta > 0 && scaleTransform.ScaleX > MAX_ZOOMRATIO)
                return;

            var ratio = 0.0;
            if (delta > 0)
            {
                ratio = scaleTransform.ScaleX * ZOOM_STEP;
            }
            else
            {
                ratio = scaleTransform.ScaleX * -ZOOM_STEP;

            }
            scaleTransform.CenterX = this.image.ActualWidth / 2.0;
            scaleTransform.CenterY = this.image.ActualHeight / 2.0;

            //TODO use animation
            scaleTransform.ScaleX += ratio;
            scaleTransform.ScaleY = Math.Abs(scaleTransform.ScaleX);
        }

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomImage(e.GetPosition(this.image), e.Delta);
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(this.image);

                if (LastZoomPoint.X == 0 && LastZoomPoint.Y == 0)
                {
                    LastZoomPoint = point;
                    return;
                }

                var xPos = point.X - LastZoomPoint.X;
                var yPos = point.Y - LastZoomPoint.Y;

                if (Math.Abs(xPos) < 10 && Math.Abs(yPos) < 10)
                    return;

                //Hit test

                var ratio = currentRatio;
                if (xPos < 0)
                    ratio *= 1.1f;
                else
                    ratio *= 0.9f;

                LimitRatio(ref ratio);

                scaleTransform.CenterX = this.image.ActualWidth / 2.0;
                scaleTransform.CenterY = this.image.ActualHeight / 2.0;

                scaleTransform.ScaleX *= ratio / currentRatio;
                scaleTransform.ScaleY *= ratio / currentRatio;

                currentRatio = ratio;
                LastZoomPoint = point;
            }
        }

        private void LimitRatio(ref float ratio)
        {
            if (ratio > MAX_ZOOMRATIO)
                ratio = MAX_ZOOMRATIO;
            else if (ratio < MIN_ZOOMRATIO)
                ratio = MIN_ZOOMRATIO;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ResetZoomPoint();
        }

        private void ResetZoomPoint()
        {
            LastZoomPoint = new Point();
        }
    }

}
