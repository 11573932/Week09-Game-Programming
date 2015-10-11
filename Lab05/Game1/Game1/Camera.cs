using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Utilities;
using Microsoft.Xna.Framework.Windows;

namespace Game1
{
   public  class Camera : Microsoft.Xna.Framework.GameComponent
    {
      public Matrix view {get; protected set;}
      public Matrix projection {get; protected set;}

      public Vector3 cameraPosition { get; protected set; }

      Vector3 cameraDirection;

      Vector3 cameraUp;

      Vector3 InitPosition;

      

      MouseState preMouseState;

      private void CreateLookAt()
      {
          view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
      }
       
        public Camera (Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base (game)
    {      // build camera view matrix
        cameraPosition = pos;
        cameraDirection = target-pos;
        cameraDirection.Normalize();
        cameraUp = up;
        CreateLookAt();
        projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,
            (float)Game.Window.ClientBounds.Width /
            (float)Game.Window.ClientBounds.Height,
            1, 3000
            );
        }

      
        public override void Initialize()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width/2,Game.Window.ClientBounds.Height/2);
            preMouseState = Mouse.GetState();
            InitPosition = cameraPosition;
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            cameraDirection = Vector3.Transform(cameraDirection,
               Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) *
               (Mouse.GetState().X - preMouseState.X)));
            cameraDirection = Vector3.Transform(cameraDirection,
                Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp,cameraDirection), (MathHelper.PiOver4 / 45) *
                (Mouse.GetState().Y-preMouseState.Y)));
            preMouseState = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                cameraPosition += cameraDirection;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection ;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection);   
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection);
            }
           // if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
           // {
            //    cameraPosition += cameraUp;
           // }
          //  if (Keyboard.GetState().IsKeyUp(Keys.Space) && cameraPosition.Y>25)
            //{
            //    cameraPosition -= cameraUp;
          //  }
           
            CreateLookAt();
            base.Update(gameTime);
        }
      
   }
}
