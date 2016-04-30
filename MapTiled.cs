using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class MapTiled
    {
        public int height;
        public int width;
        public Tileset[] tilesets;
        public Layer[] layers;
    }

    public class Tileset
    {
        public string name;
        public int tileheight;
        public int tilewidth;
        public int imagewidth;
        public int imageheight;
        public int firstgid;
    }

    public class Layer
    {
        public int[] data;
        public int x;
        public int y;
        public int height;
        public int width;
    }
}
