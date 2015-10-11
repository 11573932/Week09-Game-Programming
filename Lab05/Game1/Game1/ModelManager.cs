using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1
{
    class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
   
        List<BasicModel> models = new List<BasicModel>();

        public ModelManager(Game game) : base(game) 
        {
          
        }
     
        public override void Initialize()
        {
           
            base.Initialize();
            
        }
        protected override void LoadContent()
        {
            //models.Add(new BasicModel(
               // Game.Content.Load<Model>(@"Models/Ground/Ground")));
            models.Add(new BasicModel(Game.Content.Load<Model>(@"Models/Ground/Ground")));
            //models.Add(new SkyBox(Game.Content.Load<Model>(@"Models/SkyBox/skybox")));
            models.Add(new Tank(Game.Content.Load<Model>(@"Models/Tank/tank"),((Game1)Game).GraphicsDevice,
                ((Game1)Game).camera
                
                ));
        //    models.Add(new BasicModel(Game.Content.Load<Model>(@"Models/Tank/tank")));
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
           
          
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach(BasicModel model in models)
            {
                model.Draw(((Game1)Game).device,((Game1)Game).camera);
            }
            base.Draw(gameTime);
        }

    }
}
