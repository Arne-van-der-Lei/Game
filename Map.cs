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
        public VertexPositionNormalTexture[] TreeMesh;

        public Vector3 spawnLocation;
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

                    Vector3 normal = calculateNormalPoint(mesh[(j * width + i) * 6].Position, mesh[(j * width + i) * 6 + 1].Position, mesh[(j * width + i) * 6 + 2].Position);

                    mesh[(j * width + i) * 6].Normal = normal * -1;
                    mesh[(j * width + i) * 6 + 2].Normal = normal * -1;
                    mesh[(j * width + i) * 6 + 1].Normal = normal * -1;

                    normal = calculateNormalPoint(mesh[(j * width + i) * 6 + 3].Position, mesh[(j * width + i) * 6 + 4].Position, mesh[(j * width + i) * 6 + 5].Position);

                    mesh[(j * width + i) * 6 + 3].Normal = normal * -1;
                    mesh[(j * width + i) * 6 + 4].Normal = normal * -1;
                    mesh[(j * width + i) * 6 + 5].Normal = normal * -1;

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
                        case 14:
                            Tiles[i + 1, j] = Tiles[i, j] ;
                            Tiles[i, j + 1] = Tiles[i, j] ;
                            Tiles[i + 1, j + 1] = Tiles[i, j];
                            spawnLocation = new Vector3(j + 0.5f, Tiles[i, j], i + 0.5f);
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

            GenerateStatic(obj.layers[2], obj.tilesets[2]);
        }

        Vector3 calculateNormalPoint(Vector3 point,Vector3 point1 , Vector3 point2)
        {
            Vector3 v = point1 - point;
            Vector3 s = point2 - point;

            Vector3 T = Vector3.Cross(v, s);
            T.Normalize();
            return T;
        }

        void GenerateStatic(Layer layer,Tileset tileset)
        {
            TreeMesh = new VertexPositionNormalTexture[layer.data.Length * 3];
            int index = 0;

            int coloms = TextureInfo.imagewidth / TextureInfo.tilewidth;
            int rows = TextureInfo.imageheight / TextureInfo.tileheight;
            float WithTile = 1.0f / coloms, HeightTile = 1.0f / rows;

            for (int i = 0; i < layer.width; i++)
            {
                for(int j = 0; j < layer.height; j++)
                {
                    int data = layer.data[j * layer.width + i] - tileset.firstgid;
                    switch (data )
                    {
                        case 0:
                            TreeMesh[index].Position     = new Vector3(i - 1, Tiles[i, j] + 3, j - 2);
                            TreeMesh[index + 2].Position = new Vector3(i - 1, Tiles[i, j]    , j + 1);
                            TreeMesh[index + 1].Position = new Vector3(i + 2, Tiles[i, j] + 3, j - 2);
                            TreeMesh[index + 3].Position = new Vector3(i - 1, Tiles[i, j]    , j + 1);
                            TreeMesh[index + 5].Position = new Vector3(i + 2, Tiles[i, j]    , j + 1);
                            TreeMesh[index + 4].Position = new Vector3(i + 2, Tiles[i, j] + 3, j - 2);

                            TreeMesh[index].TextureCoordinate = new Vector2(0, 9*HeightTile);
                            TreeMesh[index + 2].TextureCoordinate = new Vector2(0, 15*HeightTile);
                            TreeMesh[index + 1].TextureCoordinate = new Vector2(5*WithTile , 9*HeightTile);
                            TreeMesh[index + 3].TextureCoordinate = new Vector2(0 , 15*HeightTile);
                            TreeMesh[index + 5].TextureCoordinate = new Vector2(5*WithTile , 15*HeightTile);
                            TreeMesh[index + 4].TextureCoordinate = new Vector2(5*WithTile , 9* HeightTile);

                            TreeMesh[index].Normal = new Vector3(0, 1, 0);
                            TreeMesh[index + 1].Normal = new Vector3(0, 1, 0);
                            TreeMesh[index + 2].Normal = new Vector3(0, 1, 0);
                            TreeMesh[index + 3].Normal = new Vector3(0, 1, 0);
                            TreeMesh[index + 4].Normal = new Vector3(0, 1, 0);
                            TreeMesh[index + 5].Normal = new Vector3(0, 1, 0);

                            Hitbox[j, i] = 1;
                            break;
                    }
                    
                    if (data != -tileset.firstgid)
                    {
                        index += 6;
                    }
                }
            }
        }
    }
}
