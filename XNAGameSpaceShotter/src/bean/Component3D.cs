using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameSpaceShotter.src.Primitives3D;
using XNAGameSpaceShotter;
using XNAGameSpaceShotter.src.bean;

namespace Project_Steel_Champions.src.bean {
    public class Component3D: Component {
        protected Model model;
        GeometricPrimitive primitiveModel;
        Color color;

        public Component3D(GameCore game, Texture2D texture)
            : base(game, texture) {
        }

        public Component3D(GameCore game, Model model, Texture2D texture)
            : base(game, texture) {
            if (model != null) {
                this.model = model;
                SetupEffectDefaults(model);
            }
        }
        public Component3D(GameCore game, GeometricPrimitive primitiveModel, Texture2D texture)
            : base(game, texture) {
            this.primitiveModel = primitiveModel;
            this.mygame = game;
            this.texture = texture;
        }
        public Component3D(GameCore game, GeometricPrimitive primitiveModel, Color color)
            : base(game, null) {
            this.primitiveModel = primitiveModel;
            this.mygame = game;
            this.color = color;
        }

        public override void Draw(GameTime gameTime) {
            if (model != null) {
                model.Draw(Matrix.CreateTranslation(position), mygame.viewMatrix, mygame.projectionMatrix);
            } else if (primitiveModel != null) {
                if (texture != null) {
                    primitiveModel.Draw(Matrix.CreateTranslation(position), mygame.viewMatrix, mygame.projectionMatrix, texture, alpha);
                } else {
                    primitiveModel.Draw(Matrix.CreateTranslation(position), mygame.viewMatrix, mygame.projectionMatrix, color);
                }
            }
        }

        private Matrix[] SetupEffectDefaults(Model myModel) {
            Matrix[] absoluteTransforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in myModel.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.EnableDefaultLighting();
                    effect.Projection = mygame.projectionMatrix;
                    effect.View = mygame.viewMatrix;
                    if (texture != null) {
                        effect.Texture = texture;
                        effect.TextureEnabled = true;
                    }
                }
            }
            return absoluteTransforms;
        }

        public static void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms) {
            //Draw the model, a model can have multiple meshes, so loop
            foreach (ModelMesh mesh in model.Meshes) {
                //This is where the mesh orientation is set
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.World = absoluteBoneTransforms[mesh.ParentBone.Index] * modelTransform;
                }
                //Draw the mesh, will use the effects set above.
                mesh.Draw();
            }
        }
        public override void Update(GameTime gameTime) {
        }
    }
}
