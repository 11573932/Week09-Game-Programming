using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class TankEnemy: BasicModel
    {
        Matrix position = Matrix.Identity;
        Matrix direction = Matrix.Identity;
        Matrix desitination = Matrix.Identity;
        ModelBone lfWheel;
        ModelBone rfWheel;
        ModelBone lbWheel;
        ModelBone rbWheel;

        public TankEnemy(Model m, GraphicsDevice device, Camera camera) : base(m)
        {
            rfWheel = model.Bones["r_front_wheel_geo"];
            rbWheel = model.Bones["r_back_wheel_geo"];
            lfWheel = model.Bones["l_front_wheel_geo"];
            lbWheel = model.Bones["l_back_wheel_geo"];
        }
        public override void Update(GameTime gameTime)
        {
            world = position;
        }
       protected override Matrix GetWorld()
        {
            return  Matrix.CreateScale(.1f) * world;
        }
    }
}
