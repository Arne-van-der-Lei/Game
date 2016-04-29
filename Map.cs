using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class Map
    {
        public int[,] Tiles { get; set; }
        public int[,] Hitbox { get; private set; }
        public int[,] IdTiles { get; set; }
        public Texture2D Texture { get; set; }

        public VertexPositionTexture[] mesh;
        public void generate()
        {
            Hitbox = new int[Tiles.GetLength(0)-1,Tiles.GetLength(1)-1];
            mesh = new VertexPositionTexture[Tiles.GetLength(0) * Tiles.GetLength(1) * 6];
            int width = Tiles.GetLength(0);
            float withTile =0.25f, HeightTile = 0.1428f;
            for (int i = 0; i < Tiles.GetUpperBound(0); i++)
            {
                for (int j = 0; j < Tiles.GetUpperBound(1); j++)
                {
                    if (Tiles[i, j] == Tiles[i + 1, j + 1])
                    {
                        mesh[(j * width + i) * 6].Position = new Vector3(j,  Tiles[i, j], i);
                        mesh[(j * width + i) * 6 + 2].Position = new Vector3(j,  Tiles[i + 1, j ], i + 1);
                        mesh[(j * width + i) * 6 + 1].Position = new Vector3(j + 1, Tiles[i , j + 1], i);
                        mesh[(j * width + i) * 6 + 3].Position = new Vector3(j,  Tiles[i + 1, j ], i + 1);
                        mesh[(j * width + i) * 6 + 5].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        mesh[(j * width + i) * 6 + 4].Position = new Vector3(j + 1,  Tiles[i , j + 1], i);

                        mesh[(j * width + i) * 6].TextureCoordinate =     new Vector2((IdTiles[i, j] % 4) * withTile            , (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 1].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile + withTile , (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 2].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile            , (IdTiles[i, j] / 4) * HeightTile  + HeightTile);
                        mesh[(j * width + i) * 6 + 3].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile            , (IdTiles[i, j] / 4) * HeightTile  + HeightTile);
                        mesh[(j * width + i) * 6 + 4].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile + withTile , (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 5].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile + withTile , (IdTiles[i, j] / 4) * HeightTile  + HeightTile);
                    }
                    else
                    {
                        mesh[(j * width + i) * 6].Position = new Vector3(j + 1,  Tiles[i, j + 1], i);
                        mesh[(j * width + i) * 6 + 2].Position = new Vector3(j,  Tiles[i, j], i);
                        mesh[(j * width + i) * 6 + 1].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        mesh[(j * width + i) * 6 + 3].Position = new Vector3(j,  Tiles[i, j], i);
                        mesh[(j * width + i) * 6 + 5].Position = new Vector3(j,  Tiles[i + 1, j], i + 1);
                        mesh[(j * width + i) * 6 + 4].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        
                        mesh[(j * width + i) * 6].TextureCoordinate =     new Vector2((IdTiles[i, j] % 4) * withTile + withTile, (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 1].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile + withTile, (IdTiles[i, j] / 4) * HeightTile + HeightTile);
                        mesh[(j * width + i) * 6 + 2].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile           , (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 3].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile           , (IdTiles[i, j] / 4) * HeightTile);
                        mesh[(j * width + i) * 6 + 4].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile + withTile, (IdTiles[i, j] / 4) * HeightTile + HeightTile);
                        mesh[(j * width + i) * 6 + 5].TextureCoordinate = new Vector2((IdTiles[i, j] % 4) * withTile           , (IdTiles[i, j] / 4) * HeightTile + HeightTile);
                    }

                    Hitbox[i, j] = Tiles[i, j] == Tiles[i + 1, j]  && Tiles[i, j + 1] == Tiles[i + 1, j + 1] && Tiles[i, j] == Tiles[i + 1, j + 1] ? 0 : 1 ;
                }
            }
        }

        public void createHeightMap(string map,int width , int height)
        {
            string[] strArr = map.Substring(1,map.Length-2).Split(",",StringSplitOptions.None);
            for (int i = 0; i < width; i++)
            {
                for( int j = 0; j < height; j++)
                {
                    switch ()
                    {

                    }
                }
            }
        }
    }
}
