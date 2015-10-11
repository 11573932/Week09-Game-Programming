﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1
{
    class BasicModel
    {
        // test commit
        public Model model { get; protected set; }
       public Matrix world = Matrix.Identity;
        public BasicModel(Model model) {
            this.model = model;
        }
        public virtual void Update(GameTime gameTime) 
        { 
            
        }
        public virtual void Draw(GraphicsDevice device,Camera camera)
        { 
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.World =transforms [mesh.ParentBone.Index]*GetWorld();
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                  
                   
                }
                mesh.Draw();
            }
        }
       protected virtual Matrix GetWorld()
        {
            return Matrix.CreateScale(.3f);
        }
    }
}
