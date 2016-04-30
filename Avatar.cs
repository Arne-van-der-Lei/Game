using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    class Avatar
    {

        public Vector3 avatarPos;
        public VertexPositionTexture[] mesh;
        public Texture2D texture;
        public float speed = 4f;

        public void Tick(GameTime gameTime,Map map)
        {
            float currentSpeed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //movement
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (map.Hitbox[(int)(avatarPos.Z + .5f - currentSpeed), (int)(avatarPos.X + .5f)] == 0 && map.Hitbox[(int)(avatarPos.Z - .5f + currentSpeed), (int)(avatarPos.X + .5f)] == 0)
                {
                    avatarPos.X += currentSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (map.Hitbox[(int)(avatarPos.Z - .5f), (int)(avatarPos.X + .5f- currentSpeed)] == 0 && map.Hitbox[(int)(avatarPos.Z - .5f), (int)(avatarPos.X - .5f + currentSpeed)] == 0)
                {
                    avatarPos.Z -= currentSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (map.Hitbox[(int)(avatarPos.Z + .5f - currentSpeed), (int)(avatarPos.X - .5f)] == 0 && map.Hitbox[(int)(avatarPos.Z - .5f + currentSpeed), (int)(avatarPos.X - .5f)] == 0)
                {
                    avatarPos.X -= 4f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (map.Hitbox[(int)(avatarPos.Z + .5f), (int)(avatarPos.X - .5f + currentSpeed)] == 0 && map.Hitbox[(int)(avatarPos.Z + .5f), (int)(avatarPos.X + .5f - currentSpeed)] == 0)
                {
                    avatarPos.Z += 4f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
