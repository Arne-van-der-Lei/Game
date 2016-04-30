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
        public Tilesets[] tilesets;
        public Layers[] layers;
    }

    public class Tilesets
    {
        public string name;
        public int tileheight;
        public int tilewidth;
        public int firstgrid;
    }

    public class Layers
    {
        public int[] data;
        public int x;
        public int y;
        public int height;
        public int width;
    }
}
