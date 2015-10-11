using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Tank: BasicModel
    {
        Matrix rotation = Matrix.Identity;
        Matrix translation = Matrix.Identity;
        MousePicking mousepicking;
        Vector3 tankPosition = Vector3.Zero;
        Vector3 tankDirection;
        Vector3 tankDestination;
        double threshold;
        Double orientation = 0;
        ButtonState preMouseLeftButton = Mouse.GetState().LeftButton;
        ModelBone Turret;
        ModelBone Cannon;
        public Tank(Model m, GraphicsDevice device, Camera camera): base (m)
        {
            mousepicking = new MousePicking(device,camera);
            Turret = model.Bones["turret_geo"];
            Cannon = model.Bones["canon_geo"];

        }
        public override void Update(GameTime gameTime)
        {
            Vector3? clickPosition;
            float speed = 100f;
            float time = (gameTime.ElapsedGameTime.Milliseconds) / 1000f;

            if (preMouseLeftButton == ButtonState.Pressed) 
            {
                clickPosition = mousepicking.GetCollisionPosition();
                if (clickPosition.HasValue == true) 
                
                {
                    tankDestination = clickPosition.Value;
                    tankDirection = clickPosition.Value - tankPosition;
                    tankDirection.Normalize();
                }
            }
            threshold = Math.Pow(tankDestination.Z - tankPosition.Z, 2) + Math.Pow((tankDestination.X - tankPosition.X), 2);
           if((threshold>1)&&(Mouse.GetState().LeftButton == ButtonState.Released)){
               Double newOrientation = Math.Atan2(tankDirection.X,tankDirection.Z);
               float rotateDirection;
               float rotationalSpeed = MathHelper.PiOver4;

               if (newOrientation > orientation)
                   rotateDirection = 1;
               else
                   rotateDirection = -1;

               float rotationalVelocity = rotationalSpeed * rotateDirection;
               float rotateAngel = rotationalVelocity * time;

               Double orientationThreshold = MathHelper.PiOver4 / 45;
               if (Math.Abs(newOrientation - orientation) > orientationThreshold)
               {
                   orientation += rotateAngel;
                   rotation *= Matrix.CreateRotationY(rotateAngel);

               }
               else
               {
                   tankPosition += speed * tankDirection * time;
                   translation = Matrix.CreateTranslation(tankPosition);
               }
           }

          Turret.Transform *= Matrix.CreateRotationY(MathHelper.PiOver4/100);
           preMouseLeftButton = Mouse.GetState().LeftButton;
           base.Update(gameTime);
        }
       protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(.1f)*rotation*translation*Matrix.CreateTranslation(0,100,0);
        }

    }
}
