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
        public bool Direction = true;
        private ActionState actionState;
        private float counter;
        private int frame;

        enum ActionState
        {
            IDEL,
            WALKING
        }

        public Avatar()
        {

            mesh = new VertexPositionNormalTexture[6];
            mesh[0].Position = new Vector3(-.7f, 1f, 0f);
            mesh[1].Position = new Vector3(.7f, 1f, 0f);
            mesh[2].Position = new Vector3(-.7f, 0f, 1f);

            mesh[3].Position = new Vector3(.7f, 1f, 0f);
            mesh[4].Position = new Vector3(.7f, 0f, 1f);
            mesh[5].Position = new Vector3(-.7f, 0f, 1f);

            mesh[0].TextureCoordinate = new Vector2(0f, 0f);
            mesh[1].TextureCoordinate = new Vector2(1/8f, 0f);
            mesh[2].TextureCoordinate = new Vector2(0f, 1f);
            mesh[3].TextureCoordinate = new Vector2(1/8f, 0f);
            mesh[4].TextureCoordinate = new Vector2(1/8f, 1f);
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

            actionState = ActionState.IDEL;

            //movement
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (map.Hitbox[(int)(avatarPos.Z + 1f - currentSpeed), (int)(avatarPos.X + .2f)] == 0 && map.Hitbox[(int)(avatarPos.Z + 0.6f + currentSpeed), (int)(avatarPos.X + .2f)] == 0)
                {
                    avatarPos.X += currentSpeed;
                    Direction = true;
                    actionState = ActionState.WALKING;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (map.Hitbox[(int)(avatarPos.Z + 0.6f), (int)(avatarPos.X + .2f- currentSpeed)] == 0 && map.Hitbox[(int)(avatarPos.Z + 0.6f), (int)(avatarPos.X - .2f + currentSpeed)] == 0)
                {
                    avatarPos.Z -= currentSpeed;
                    actionState = ActionState.WALKING;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (map.Hitbox[(int)(avatarPos.Z + 1f - currentSpeed), (int)(avatarPos.X - .2f)] == 0 && map.Hitbox[(int)(avatarPos.Z + 0.6f + currentSpeed), (int)(avatarPos.X - .2f)] == 0)
                {
                    avatarPos.X -= currentSpeed;
                    Direction = false;
                    actionState = ActionState.WALKING;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (map.Hitbox[(int)(avatarPos.Z + 1f), (int)(avatarPos.X - .2f + currentSpeed)] == 0 && map.Hitbox[(int)(avatarPos.Z + 1f), (int)(avatarPos.X + .2f - currentSpeed)] == 0)
                {
                    avatarPos.Z += currentSpeed;
                    actionState = ActionState.WALKING;
                }
            }

            SetActionState(gameTime);
        }

        private void SetActionState(GameTime gameTime)
        {
            counter += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (counter >= 0.1f)
            {
                frame++;
                counter = 0;
            }

            switch (actionState)
            {
                case ActionState.IDEL:
                    frame = 0;
                    break;
                case ActionState.WALKING:
                    if(frame == 8)
                    {
                        frame = 2;
                    }
                    break;
            }

            SetCorrectTexCoords();
        }

        private void SetCorrectTexCoords()
        {
            if (Direction)
            {
                mesh[0].TextureCoordinate = new Vector2(frame * 1 / 8f, 0f);
                mesh[1].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 0f);
                mesh[2].TextureCoordinate = new Vector2(frame * 1 / 8f, 1f);
                mesh[3].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 0f);
                mesh[4].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 1f);
                mesh[5].TextureCoordinate = new Vector2(frame * 1 / 8f, 1f);
            }
            else
            {
                mesh[0].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 0f);
                mesh[1].TextureCoordinate = new Vector2(frame * 1 / 8f, 0f);
                mesh[2].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 1f);
                mesh[3].TextureCoordinate = new Vector2(frame * 1 / 8f, 0f);
                mesh[4].TextureCoordinate = new Vector2(frame * 1 / 8f, 1f);
                mesh[5].TextureCoordinate = new Vector2(frame * 1 / 8f + 1 / 8f, 1f);
            }
        }

        public VertexPositionNormalTexture[] getMesh()
        {
            VertexPositionNormalTexture[] meshcopy = new VertexPositionNormalTexture[mesh.Length];
            mesh.CopyTo(meshcopy,0);
            for (int i = 0; i < meshcopy.Length; i++)
            {
                meshcopy[i].Position += avatarPos;
            }

            return meshcopy;
        }
    }
}
