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
        public VertexPositionNormalTexture[] mesh;
        public Texture2D texture;
        public float speed = 4f;

        public Avatar()
        {

            mesh = new VertexPositionNormalTexture[6];
            mesh[0].Position = new Vector3(-.5f, 1f, -.5f);
            mesh[1].Position = new Vector3(.5f, 1f, -.5f);
            mesh[2].Position = new Vector3(-.5f, 0f, .5f);

            mesh[3].Position = new Vector3(.5f, 1f, -.5f);
            mesh[4].Position = new Vector3(.5f, 0f, .5f);
            mesh[5].Position = new Vector3(-.5f, 0f, .5f);

            mesh[0].TextureCoordinate = new Vector2(0f, 0f);
            mesh[1].TextureCoordinate = new Vector2(1f, 0f);
            mesh[2].TextureCoordinate = new Vector2(0f, 1f);
            mesh[3].TextureCoordinate = new Vector2(1f, 0f);
            mesh[4].TextureCoordinate = new Vector2(1f, 1f);
            mesh[5].TextureCoordinate = new Vector2(0f, 1f);

            mesh[0].Normal = new Vector3(0f, 1f, 0f);
            mesh[1].Normal = new Vector3(0f, 1f, 0f);
            mesh[2].Normal = new Vector3(0f, 1f, 0f);
            mesh[3].Normal = new Vector3(0f, 1f, 0f);
            mesh[4].Normal = new Vector3(0f, 1f, 0f);
            mesh[5].Normal = new Vector3(0f, 1f, 0f);
        }

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
