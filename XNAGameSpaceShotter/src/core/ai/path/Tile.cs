using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGameSpaceShotter.src.core.ai.path {
    public class Tile {
        public int x;
        public int y;
        public int egasto;
        public double distancia;
        public Tile pai;
        public List<Tile> visinhos = new List<Tile>();

        public Tile(int x, int y) {
            this.x = x;
            this.y = y;
            egasto = 0;
            distancia = 0;
        }

        public Tile(Tile copy) {
            this.x = copy.x;
            this.y = copy.y;
            this.egasto = copy.egasto;
            this.distancia = copy.distancia;
            this.pai = copy.pai;
            this.visinhos = copy.visinhos;
        }

        public void associar(Tile visinho) {
            if (visinho != null && !visinhos.Contains(visinho)) {
                visinhos.Add(visinho);
                visinho.visinhos.Add(this);
            }
        }
        public bool equals(Tile obj) {
            return obj.x == x && obj.y == y;
        }
        public double calculaDistancia(Tile other) {
            return Math.Sqrt(((x - other.x) * (x - other.x)) + ((y - other.y) * (y - other.y)));
        }
        public double calculaDistanciaAoQuadrado(float otherX, float otherY) {
            float myXmap = x * GameConstants.squareWidth;
            float myYmap = y * GameConstants.squareWidth;
            double result = (((myXmap - otherX) * (myXmap - otherX)) + ((myYmap - otherY) * (myYmap - otherY)));
            int xMap = ((int)otherX) / GameConstants.squareWidth;
            int yMap = ((int)otherY) / GameConstants.squareWidth;
            return result;
        }
    }
}
