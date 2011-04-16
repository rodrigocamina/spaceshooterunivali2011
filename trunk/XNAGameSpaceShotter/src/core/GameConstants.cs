using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGameSpaceShotter.src.core {
    class GameConstants {
        //camera constants
        public const float CameraYDistance = 15000.0f;
        public const float CameraXDistance = 8000;
        public const float CameraHeight = 15000.0f;
        public const float CameraHeightMin = 5000.0f;
        public const float CameraHeightMax = 35000.0f;
        public const float PlayfieldSizeX = 16000f;
        public const float PlayfieldSizeY = 12500f;
        //
        public const float angleStep = (float)Math.PI * 4 / 5;
        public const float distanciaMinimaTile = 1000;

        public const int squareWidth = 1500;
        public const int halfSquareWidth = squareWidth / 2;
        public const int thirdSquareWidth = squareWidth / 3;

    }
}
