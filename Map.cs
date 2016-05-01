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
        public int[,] Hitbox { get; set; }
        public int[,] IdTiles { get; set; }
        public Texture2D Texture { get; set; }

        public Tileset TextureInfo;

        public VertexPositionNormalTexture[] mesh;
        public void generate()
        {
            Hitbox = new int[Tiles.GetLength(0)-1,Tiles.GetLength(1)-1];
            mesh = new VertexPositionNormalTexture[Tiles.GetLength(0) * Tiles.GetLength(1) * 6];
            int width = Tiles.GetLength(0);
            int coloms = TextureInfo.imagewidth / TextureInfo.tilewidth;
            int rows = TextureInfo.imageheight / TextureInfo.tileheight;
            float withTile = 1.0f / coloms, HeightTile = 1.0f / rows;
            int currentId;

            for (int i = 0; i < Tiles.GetUpperBound(0); i++)
            {
                for (int j = 0; j < Tiles.GetUpperBound(1); j++)
                {
                    currentId = IdTiles[i, j] - 1;
                    if (Tiles[i, j] == Tiles[i + 1, j + 1])
                    {
                        mesh[(j * width + i) * 6].Position     = new Vector3(j    ,  Tiles[i, j]        , i);
                        mesh[(j * width + i) * 6 + 2].Position = new Vector3(j    ,  Tiles[i + 1, j ]   , i + 1);
                        mesh[(j * width + i) * 6 + 1].Position = new Vector3(j + 1,  Tiles[i , j + 1]   , i);
                        mesh[(j * width + i) * 6 + 3].Position = new Vector3(j    ,  Tiles[i + 1, j ]   , i + 1);
                        mesh[(j * width + i) * 6 + 5].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        mesh[(j * width + i) * 6 + 4].Position = new Vector3(j + 1,  Tiles[i , j + 1]   , i);

                        mesh[(j * width + i) * 6].TextureCoordinate     = new Vector2((currentId % coloms) * withTile            , (currentId /coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 1].TextureCoordinate = new Vector2((currentId % coloms) * withTile + withTile , (currentId /coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 2].TextureCoordinate = new Vector2((currentId % coloms) * withTile            , (currentId /coloms) * HeightTile  + HeightTile);
                        mesh[(j * width + i) * 6 + 3].TextureCoordinate = new Vector2((currentId % coloms) * withTile            , (currentId /coloms) * HeightTile  + HeightTile);
                        mesh[(j * width + i) * 6 + 4].TextureCoordinate = new Vector2((currentId % coloms) * withTile + withTile , (currentId /coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 5].TextureCoordinate = new Vector2((currentId % coloms) * withTile + withTile , (currentId /coloms) * HeightTile  + HeightTile);
                    }
                    else
                    {
                        mesh[(j * width + i) * 6].Position     = new Vector3(j + 1,  Tiles[i, j + 1]    , i);
                        mesh[(j * width + i) * 6 + 2].Position = new Vector3(j    ,  Tiles[i, j]        , i);
                        mesh[(j * width + i) * 6 + 1].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        mesh[(j * width + i) * 6 + 3].Position = new Vector3(j    ,  Tiles[i, j]        , i);
                        mesh[(j * width + i) * 6 + 5].Position = new Vector3(j    ,  Tiles[i + 1, j]    , i + 1);
                        mesh[(j * width + i) * 6 + 4].Position = new Vector3(j + 1,  Tiles[i + 1, j + 1], i + 1);
                        
                        mesh[(j * width + i) * 6].TextureCoordinate     = new Vector2((currentId % coloms) * withTile + withTile, (currentId / coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 1].TextureCoordinate = new Vector2((currentId % coloms) * withTile + withTile, (currentId / coloms) * HeightTile + HeightTile);
                        mesh[(j * width + i) * 6 + 2].TextureCoordinate = new Vector2((currentId % coloms) * withTile           , (currentId / coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 3].TextureCoordinate = new Vector2((currentId % coloms) * withTile           , (currentId / coloms) * HeightTile);
                        mesh[(j * width + i) * 6 + 4].TextureCoordinate = new Vector2((currentId % coloms) * withTile + withTile, (currentId / coloms) * HeightTile + HeightTile);
                        mesh[(j * width + i) * 6 + 5].TextureCoordinate = new Vector2((currentId % coloms) * withTile           , (currentId / coloms) * HeightTile + HeightTile);
                    }

                    Hitbox[i, j] = Tiles[i, j] == Tiles[i + 1, j]  && Tiles[i, j + 1] == Tiles[i + 1, j + 1] && Tiles[i, j] == Tiles[i + 1, j + 1] ? 0 : 1 ;
                }
            }
        }

        public void createHeightMap(int width , int height)
        {
            Tiles[0, 0] = 0;
            for (int i = 0; i < height; i++)
            {
                for( int j = 0; j < width; j++)
                {
                    switch (IdTiles[i,j])
                    {
                        case 1:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j];
                            Tiles[i + 1, j + 1] = Tiles[i, j] + 1;
                            break;
                        case 2:
                        case 17:
                            Tiles[i + 1, j] = Tiles[i, j] + 1;
                            Tiles[i, j + 1] = Tiles[i, j] ;
                            Tiles[i + 1, j + 1] = Tiles[i, j] + 1;
                            break;
                        case 3:
                            Tiles[i + 1, j] = Tiles[i, j] + 1;
                            Tiles[i, j + 1] = Tiles[i, j];
                            Tiles[i + 1, j + 1] = Tiles[i, j];
                            break;
                        case 4:
                        case 15:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j] + 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j] + 1;
                            break;
                        case 5:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j];
                            Tiles[i + 1, j + 1] = Tiles[i, j];
                            break;
                        case 6:
                        case 13:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j] - 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j] - 1;
                            break;
                        case 7:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j] + 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j] ;
                            break;
                        case 8:
                        case 11:
                            Tiles[i + 1, j] = Tiles[i, j] - 1;
                            Tiles[i, j + 1] = Tiles[i, j] ;
                            Tiles[i + 1, j + 1] = Tiles[i, j] - 1;
                            break;
                        case 9:
                            Tiles[i + 1, j] = Tiles[i, j] - 1;
                            Tiles[i, j + 1] = Tiles[i, j] - 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j] -1;
                            break;
                        case 10:
                            Tiles[i + 1, j] = Tiles[i, j] ;
                            Tiles[i, j + 1] = Tiles[i, j] ;
                            Tiles[i + 1, j + 1] = Tiles[i, j] - 1;
                            break;
                        case 12:
                            Tiles[i + 1, j] = Tiles[i, j] - 1;
                            Tiles[i, j + 1] = Tiles[i, j];
                            Tiles[i + 1, j + 1] = Tiles[i, j];
                            break;
                        case 16:
                            Tiles[i + 1, j] = Tiles[i, j];
                            Tiles[i, j + 1] = Tiles[i, j] - 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j];
                            break;
                        case 18:
                            Tiles[i + 1, j] = Tiles[i, j] + 1;
                            Tiles[i, j + 1] = Tiles[i, j] + 1;
                            Tiles[i + 1, j + 1] = Tiles[i, j] + 1;
                            break;
                        default:
                            Tiles[i + 1, j] = Tiles[i, j] ;
                            Tiles[i, j + 1] = Tiles[i, j] ;
                            Tiles[i + 1, j + 1] = Tiles[i, j] ;
                            break;

                    }
                }
            }
        }

        public void initialize(MapTiled obj)
        {
            int width = obj.width;
            int height = obj.height;

            IdTiles = new int[height, width];

            Layer layer = obj.layers[1];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    IdTiles[i, j] = layer.data[i * width + j] - obj.tilesets[1].firstgid + 1;
                }
            }

            Tiles = new int[height + 1, width + 1];
            createHeightMap(width, height);

            layer = obj.layers[0];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    IdTiles[i, j] = layer.data[i * width + j] - obj.tilesets[0].firstgid + 1;
                }
            }

            TextureInfo = obj.tilesets[0];
            generate();
        }
    }
}
