using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAGameSpaceShotter.src.core.ai.path {
    public class PathFinderData {
        public List<Tile> utilizados = new List<Tile>();
        public List<Tile> emEspera = new List<Tile>();
        public Tile melhorcaminho;
        public Tile alvo;
        public Tile start;
        public int rndN;
        public bool caminhoInexistente = false;
        public bool concluido = false;
    }
}
