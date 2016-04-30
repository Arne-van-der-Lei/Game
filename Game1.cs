using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;
        VertexPositionColor[] ava;
        Map map;
        int distance;
        Vector3 avatarPos;
        bool down;
        bool right;
        bool left;
        bool up;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //fullscreen
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            //neirest neighbour
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            //avatar model
            ava = new VertexPositionColor[6];
            ava[0].Position = new Vector3(-.5f, 1f, -.5f);
            ava[1].Position = new Vector3( .5f, 1f, -.5f);
            ava[2].Position = new Vector3(-.5f, 0f,  .5f);

            ava[3].Position = new Vector3( .5f, 1f, -.5f);
            ava[4].Position = new Vector3( .5f, 0f,  .5f);
            ava[5].Position = new Vector3(-.5f, 0f,  .5f);

            ava[0].Color = new Color(127, 127, 127);
            ava[1].Color = new Color(127, 127, 127);
            ava[2].Color = new Color(127, 127, 127);

            ava[3].Color = new Color(127, 127, 127);
            ava[4].Color = new Color(127, 127, 127);
            ava[5].Color = new Color(127, 127, 127);

            //init mapdata -> moet nog geport worden naar file
            map = new Map();
            /*map.Tiles = new int[,]
            {
                {2 , 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1},
                {1 , 1, 1, 1, 1, 2, 2, 1, 0, 0, 0, 1},
                {1 , 0, 0, 0, 1, 2, 2, 1, 0, 0, 0, 1},
                {1 , 0, 0, 0, 1, 2, 2, 1, 0, 0, 1, 1},
                {1 , 0, 0, 0, 1, 2, 2, 1, 0, 0, 1, 1},
                {1 , 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1},
                {1 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1 , 0,-1,-1, 0, 1, 1, 1, 1, 1, 1, 1},
                {1 , 0,-1,-1, 0, 1, 2, 2, 2, 2, 2, 2}
            };
            map.IdTiles = new int[,]
            {
                {2 ,2 ,2 ,2 ,3 ,5 ,4 ,0 ,2 ,2 ,3 },
                {0 ,2 ,2 ,3 ,7 ,5 ,4 ,4 ,5 ,5 ,7 },
                {4 ,9 ,6 ,7 ,7 ,5 ,4 ,4 ,6 ,0 ,15},
                {4 ,6 ,5 ,7 ,7 ,5 ,4 ,4 ,5 ,7 ,5 },
                {4 ,5 ,5 ,7 ,0 ,2 ,0 ,4 ,5 ,0 ,3 },
                {4 ,5 ,9 ,0 ,2 ,2 ,2 ,0 ,5 ,5 ,7 },
                {4 ,9 ,5 ,5 ,6 ,5 ,10,5 ,6 ,5 ,7 }, 
                {4 ,5 ,6 ,5 ,5 ,9 ,5 ,6 ,5 ,5 ,7 },
                {12,2 ,13,3 ,0 ,13,13,13,13,13,15},
                {4 ,4 ,5 ,7 ,7 ,13,13,13,13,13,15}
            };*/


            //avatar + cam ofset
            avatarPos = new Vector3(5.5f, 4, 5.5f);
            distance = 3;

            //shader
            effect = new BasicEffect(GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //spritebatch for 2d stuff
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //map generation + textue setting
            String jsonString = File.ReadAllText("content/map.json");
            MapTiled maptiled = JsonConvert.DeserializeObject<MapTiled>(jsonString);

            map.Texture = Content.Load<Texture2D>(maptiled.tilesets[0].name);
            map.initialize(maptiled);
            map.generate();

            //effect = Content.Load<Effect>("shader");
            //map = Content.Load<Map>("map");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            graphics.ApplyChanges();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //movement
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if(map.Hitbox[(int)(avatarPos.Z+.45f), (int)(avatarPos.X + .5f)] == 0 && map.Hitbox[(int)(avatarPos.Z-.45f), (int)(avatarPos.X + .5f)] == 0)
                {
                    avatarPos.X += 2f*(float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (map.Hitbox[(int)(avatarPos.Z - .5f), (int)(avatarPos.X +.45f)] == 0 && map.Hitbox[(int)(avatarPos.Z - .5f), (int)(avatarPos.X - .45f)] == 0)
                {
                    avatarPos.Z -= 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (map.Hitbox[(int)(avatarPos.Z + .45f), (int)(avatarPos.X - .5f)] == 0 && map.Hitbox[(int)(avatarPos.Z - .45f), (int)(avatarPos.X - .5f)] == 0)
                {
                    avatarPos.X -= 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (map.Hitbox[(int)(avatarPos.Z + .5f), (int)(avatarPos.X - .45f)] == 0 && map.Hitbox[(int)(avatarPos.Z + .5f), (int)(avatarPos.X + .45f)] == 0)
                {
                    avatarPos.Z += 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var cameraPosition = new Vector3(avatarPos.X, avatarPos.Y + distance + 1, avatarPos.Z + distance);
            var cameraLookAtVector = avatarPos;
            var cameraUpVector = Vector3.Up;

            effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);

            float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            float fieldOfView = MathHelper.ToRadians(70f);
            float nearClipPlane = 1;
            float farClipPlane = 1000;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            DrawGround();
            DrawAvatar();
            base.Draw(gameTime);
        }

        void DrawGround()
        {
            effect.TextureEnabled = true;
            effect.Texture = map.Texture;

            //draw map
            effect.World = Matrix.CreateTranslation(0, 0, 0);
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,map.mesh,0,map.mesh.GetLength(0)/3);
            }
        }

        void DrawAvatar()
        {
            effect.World = Matrix.CreateTranslation(avatarPos);
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, ava, 0, ava.GetLength(0) / 3);
            }
        }
    }
}
