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
        Map map;
        int distance;
        Avatar avatar;
        Effect effectOur;
        int sunKelvin;
        bool sunbool;
        Vector3 sunVector;

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

            avatar = new Avatar();

            //avatar + cam ofset
            distance = 3;

            sunKelvin = 60*60*18;
            sunbool = true;
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
            String jsonString = File.ReadAllText("content/map_1.json");
            MapTiled maptiled = JsonConvert.DeserializeObject<MapTiled>(jsonString);

            sunVector = new Vector3(1f,-1f,1f);
            sunVector.Normalize();
            //init mapdata
            map = new Map();
            map.Texture = Content.Load<Texture2D>("Tilesheets\\" + maptiled.tilesets[0].name);
            map.initialize(maptiled);
            
            avatar.avatarPos = map.spawnLocation;
            avatar.texture = Content.Load<Texture2D>("Sprites\\Characters_NPCs\\Main_char_walking_animation");
            effectOur = Content.Load<Effect>("Shaders\\BasicShader");
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

            avatar.Tick(gameTime, map);

            if (sunbool == true)
            {
                sunKelvin++;
            }
            else
            {
                sunKelvin--;
            }

            if(sunKelvin == 60*60*12)
            {
                sunbool = false;
            }
            if(sunKelvin == 0)
            {
                sunbool = true;
            }

            sunVector.X = (float)Math.Cos((double)(sunKelvin / (60d * 60d * 12d))* Math.PI );
            sunVector.Y = (float)Math.Sin((double)(sunKelvin / (60d * 60d * 12d))* Math.PI );
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var cameraPosition = new Vector3(avatar.avatarPos.X, avatar.avatarPos.Y + distance + 1, avatar.avatarPos.Z + distance);
            var cameraLookAtVector = avatar.avatarPos;
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
            effectOur.Parameters["WorldViewProjection"].SetValue(effect.View * effect.Projection);
            effectOur.Parameters["TextureSampler"].SetValue(map.Texture);
            effectOur.Parameters["sunNormal"].SetValue(sunVector);
            foreach (var pass in effectOur.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,map.mesh,0,map.mesh.GetLength(0)/3);
            }
        }

        void DrawAvatar()
        {
            effectOur.Parameters["WorldViewProjection"].SetValue(Matrix.CreateTranslation(avatar.avatarPos) * effect.View * effect.Projection);
            effectOur.Parameters["TextureSampler"].SetValue(avatar.texture);
            foreach (var pass in effectOur.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, avatar.mesh, 0, avatar.mesh.GetLength(0) / 3);
            }
        }
    }
}
