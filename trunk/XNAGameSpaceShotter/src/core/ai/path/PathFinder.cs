using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.core.ai.path {
    public class PathFinder {
        Tile[][] tilemap;
        int col;
        int lin;

        List<Tile> utilizados = new List<Tile>();
        LinkedList<Tile> emEspera = new LinkedList<Tile>();
        Tile melhorcaminho;
        Tile alvo;
        Tile start;
        int rndN;
        static Random rnd = new Random();
        public bool caminhoInexistente = false;
        public bool concluido = false;

        public PathFinder(Texture2D redMap) {
            col = redMap.Width;
            lin = redMap.Height;
            Color[] retrievedColor = new Color[col * lin];
            redMap.GetData<Color>(0, new Rectangle(0, 0, col, lin), retrievedColor, 0, retrievedColor.Length);
            tilemap = new Tile[col][];
            for (int i = 0; i < col; i++) {
                tilemap[i] = new Tile[lin];
                for (int j = 0; j < lin; j++) {
                    tilemap[i][j] = new Tile(i, j);
                }
            }
            start = null;
            Tile end = null;
            for (int i = 0; i < col; i++) {
                for (int j = 0; j < lin; j++) {
                    if (retrievedColor[j * col + i].PackedValue != (uint)4278190080) {
                        if (i > 0) {
                            tilemap[i][j].associar(tilemap[i - 1][j]);
                            if (j > 0) {
                                tilemap[i][j].associar(tilemap[i - 1][j - 1]);
                            }
                            if (j < lin - 1) {
                                tilemap[i][j].associar(tilemap[i - 1][j + 1]);
                            }
                        }
                        if (j > 0) {
                            tilemap[i][j].associar(tilemap[i][j - 1]);
                        }
                        if (retrievedColor[j * col + i].R == 255 && retrievedColor[j * col + i].G == 0 && retrievedColor[j * col + i].B == 0) {
                            end = tilemap[i][j];
                        }
                        if (retrievedColor[j * col + i].R == 0 && retrievedColor[j * col + i].G == 0 && retrievedColor[j * col + i].B == 255) {
                            start = tilemap[i][j];
                        }
                        /**
                       start - 4294901760 vermelho
                       end   - 4278190335 azul
                       ????? - 4278255360 verde
                        */
                    } else {
                        tilemap[i][j] = null;
                    }
                }
            }
            Console.WriteLine(">" + (start == null));
            Console.WriteLine(">" + (end == null));
            if (start != null && end != null) {
                setTarget(start.x, start.y, end.x, end.y);
            }

        }

        public Tile getStart() {
            return start;
        }
        public Tile getEnd() {
            return alvo;
        }
        public Tile[][] getMap() {
            return tilemap;
        }

        public PathFinder(Tile[][] map) {
            col = map.Length;
            lin = map[0].Length;
            tilemap = map;
        }
        public PathFinder(int[][] map) {
            col = map.Length;
            lin = map[0].Length;
            tilemap = new Tile[col][];
            //criando um Tilemap completo
            for (int i = 0; i < col; i++) {
                tilemap[i] = new Tile[lin];
                for (int j = 0; j < lin; j++) {
                    tilemap[i][j] = new Tile(i, j);
                }
            }
            //ligando tiles para pathfind e eliminando tiles onde não há caminho
            for (int i = 0; i < col; i++) {
                for (int j = 0; j < lin; j++) {
                    //se o número do mapa é menor que 1, o campo está livre
                    //se o número do mapa é maior que 0, o campo está ocupado com a barreira de tipo igual ao número
                    //se o número do mapa é igual a -1, o campo está reservado por alguma regra como bandeira ou pontos
                    if (map[i][j] < 1) {
                        if (i > 0) {
                            tilemap[i][j].associar(tilemap[i - 1][j]);
                            //barreiras do tipo container não possibilitam andar diagonal ao seu lado
                            //checando por container de cima
                            if (map[i - 1][j] < 10) {
                                //checando por container a esquerda
                                if (j > 0 && map[i][j - 1] < 10) {
                                    tilemap[i][j].associar(tilemap[i - 1][j - 1]);
                                }
                                //checando por container a direita
                                if (j < lin - 1 && map[i][j + 1] < 10) {
                                    tilemap[i][j].associar(tilemap[i - 1][j + 1]);
                                }
                            }
                        }
                        if (j > 0) {
                            tilemap[i][j].associar(tilemap[i][j - 1]);
                        }
                    } else {
                        tilemap[i][j] = null;
                    }
                }
            }
        }

        public Tile getMappedTile(int col, int lin) {
            return tilemap[col][lin];
        }

        public int getCol() {
            return col;
        }

        public int getLin() {
            return lin;
        }

        public void setTarget(int xStart, int yStart, int xEnd, int yEnd) {
            utilizados = new List<Tile>();
            emEspera = new LinkedList<Tile>();
            start = tilemap[xStart][yStart];
            Tile endTile;
            try {
                endTile = tilemap[xEnd][yEnd];
            } catch (Exception e) {
                endTile = null;
            }
            if (endTile == null) {
                endTile = start;
            }
            start.pai = null;
            start.egasto = 0;
            start.distancia = start.calculaDistancia(endTile);
            emEspera.AddFirst(start);
            melhorcaminho = start;
            alvo = endTile;
            concluido = false;
            caminhoInexistente = false;
        }

        public void setTarget(Tile startTile, Tile endTile)//usar quando tem os Tiles ou quando não é um mapa quadrado
        {
            utilizados = new List<Tile>();
            emEspera = new LinkedList<Tile>();
            startTile.pai = null;
            startTile.egasto = 0;
            startTile.distancia = startTile.calculaDistancia(endTile);
            emEspera.AddFirst(startTile);
            melhorcaminho = startTile;
            alvo = endTile;
            concluido = false;
            caminhoInexistente = false;
        }

        public void setImperfectPath(int wrongFactor) {
            rndN = wrongFactor;
        }

        public List<Tile> procuraMenorCaminho(int nroNodos) {
            if (emEspera.Count > 0) {
                List<Tile> result = new List<Tile>();
                int nroAtual = 0;
                Tile escolhido = null;

                //0, 0, Int64.MaxValue, Int64.MaxValue, null
                while (emEspera.Count > 0 && nroNodos > nroAtual) {
                    nroAtual++;
                    escolhido = emEspera.First.Value;
                    emEspera.RemoveFirst();
                    if (melhorcaminho.distancia > escolhido.distancia) {
                        melhorcaminho = escolhido;
                    }
                    if (escolhido.equals(alvo)) {
                        concluido = true;
                        break;
                    }
                    organizaEadiciona(escolhido);
                    utilizados.Add(escolhido);
                }
                if (emEspera.Count == 0) {
                    caminhoInexistente = true;
                }
                do {
                    result.Insert(0, melhorcaminho);
                    melhorcaminho = melhorcaminho.pai;
                } while (melhorcaminho != null);
                melhorcaminho = escolhido;
                //extra, para verificar caminho escolhido
                /*
                Console.WriteLine("");
                int count = 0;
                for (int i = 0; i < col; i++)
                {
                    String s = "";
                    for (int j = 0; j < lin; j++)
                    {
                        if (tilemap[i][j] == null)
                        {
                            s += "[X]";
                        }
                        else if (result.Contains(tilemap[i][j]))
                        {
                            count++;
                            s += "[o]";
                        }
                        else
                        {
                            s += "[ ]";
                        }
                    }
                    Console.WriteLine(s);
                }
                */


                return result;
            } else {
                /*
                Console.WriteLine("");
                for (int i = 0; i < col; i++)
                {
                    String s = "";
                    for (int j = 0; j < lin; j++)
                    {
                        if (tilemap[i][j] == null)
                        {
                            s += "[X]";
                        }
                        else
                        {
                            s += "[ ]";
                        }
                    }
                    Console.WriteLine(s);
                }*/
                concluido = true;
                return new List<Tile>();
            }
        }

        //usa linked list e deixa lista ordenada para agilizar procura do melhor caminho
        private void organizaEadiciona(Tile pai) {
            List<Tile> naoOrganizados = pai.visinhos;
            bool notInserted;//escolhido.getEgasto()+1, (int)(Point.distance(escolhido.getX(),escolhido.getY()-1, xAlvo,yAlvo)
            LinkedListNode<Tile> node;
            Tile aOrganizar;
            for (int i = 0; i < naoOrganizados.Count; i++) {
                notInserted = true;
                aOrganizar = naoOrganizados[i];
                if (!utilizados.Contains(aOrganizar) && !emEspera.Contains(aOrganizar)) {
                    aOrganizar.pai = pai;
                    if (rndN != 1) {
                        aOrganizar.egasto = pai.egasto + rnd.Next(rndN);
                    } else {
                        aOrganizar.egasto = pai.egasto + 1;
                    }
                    aOrganizar.distancia = alvo.calculaDistancia(aOrganizar);
                    node = emEspera.First;
                    while (node != null) {
                        if (aOrganizar.distancia + aOrganizar.egasto < node.Value.distancia + node.Value.egasto) {
                            notInserted = false;
                            emEspera.AddBefore(node, aOrganizar);
                            break;
                        }
                        node = node.Next;
                    };
                    if (notInserted) {
                        emEspera.AddLast(aOrganizar);
                    }
                }
            }
        }
    }
}
/*
Exemplo de como utilizar:
 * 
 * 
            //Prepare o mapa com as posicoes validas e invalidas
            //Sao consideradas validas qualquer posicao com valor menor que 1
            //Na versão atual, é aceito somente mapas retangulares
            int col = 10;
            int lin = 10;
            int[][] map = new int[col][];
            for (int i = 0; i < col; i++)
            {
                map[i] = new int[lin];
                for (int j = 0; j < lin; j++)
                {
                    map[i][j] = 0;
                }
            }
            map[1][0] = 1;
            map[1][1] = 1;
            map[2][2] = 1;
            map[0][4] = 1;
            map[1][4] = 1;
            map[3][5] = 1;
            map[3][6] = 1;
            map[3][7] = 1;
            map[3][8] = 1;
            map[3][9] = 1;
            map[3][4] = 1;
            map[4][4] = 1;
            map[5][4] = 1;
            map[6][4] = 1;
            map[7][4] = 1;
            map[8][4] = 1;
  
  
            //Cria um PathFinder passando o mapa a ser utilizado
            PathFinder pathfinder = new PathFinder(map);
  
            //Determine o ponto inicial da pesquisa
            pathfinder.setTarget(0, 0, 9, 9);
            
            //Repita o processo a seguir em cada ciclo de jogo até a flag concluido ficar ativa
            while (!pathfinder.concluido)
            {
                //Mande o pathfinder procurar N interacoes, ele sempre devolvera o melhor resultado atual
                pathfinder.procuraMenorCaminho(5000);
            }



*/