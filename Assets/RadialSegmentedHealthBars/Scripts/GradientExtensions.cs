using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RengeGames.HealthBars.Extensions {

    public static class GradientExtensions {
        public static Texture2D ToTexture2D(this Gradient gradient, int width = 100, int height = 1) {
            if (gradient == null) return new Texture2D(1,1);
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            if (gradient.mode == GradientMode.Fixed) {
                texture.filterMode = FilterMode.Point;
            }
            for (int i = 0; i < width; i++) {
                Color c = gradient.Evaluate((float) i / width);
                for (int j = 0; j < height; j++) {
                    texture.SetPixel(i, j, c);
                }
            }
            texture.Apply();
            return texture;
        }
        public static bool EqualTo(this Gradient a, Gradient b, int samples = 5) {
            if (a.mode != b.mode) return false;
            
            if (a.mode == GradientMode.Fixed) {
                samples *= 2;
            }
            for (int i = 0; i < samples; i++) {
                float time = Random.value;
                if (a.Evaluate(time) != b.Evaluate(time)) {
                    return false;
                }
            }
            return true;
        }
        
        public static Gradient Clone(this Gradient gradient) {
            Gradient clone = new Gradient();
            clone.mode = gradient.mode;
            clone.alphaKeys = (GradientAlphaKey[])gradient.alphaKeys.Clone();
            clone.colorKeys = (GradientColorKey[]) gradient.colorKeys.Clone();
            return clone;
        }
    }
}