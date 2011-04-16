using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace XNAGameSpaceShotter.src.Primitives3D {
    public class Block3D: GeometricPrimitive {
        public float width;
        public float height;
        public float depth;
        /// <summary>
        /// Constructs a new block, using default settings.
        /// </summary>
        public Block3D(GraphicsDevice graphicsDevice)
            : this(graphicsDevice, 1, 0.1f, 1) {
        }


        /// <summary>
        /// Constructs a new block, with the specified sizes.
        /// </summary>
        public Block3D(GraphicsDevice graphicsDevice, float width, float height, float depth) {
            this.width = width;
            this.height = height;
            this.depth = depth;

            // Create edges
            Vector3 edge1 = new Vector3(0, height, 0);
            Vector3 edge2 = new Vector3(0, 0, 0);
            Vector3 edge3 = new Vector3(width, 0, 0);
            Vector3 edge4 = new Vector3(width, height, 0);
            Vector3 edge5 = new Vector3(0, 0, depth);
            Vector3 edge6 = new Vector3(0, height, depth);
            Vector3 edge7 = new Vector3(width, height, depth);
            Vector3 edge8 = new Vector3(width, 0, depth);

            width /= 2;
            height /= 2;
            depth /= 2;
            Vector3[] normals =
            {
                new Vector3(0, 0, depth),
                new Vector3(0, 0, -depth),
                new Vector3(width, 0, 0),
                new Vector3(-width, 0, 0),
                new Vector3(0, height, 0),
                new Vector3(0, -height, 0),
            };
            // Create each face.
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge1, normals[1], new Vector2(0, 1));
            AddVertex(edge2, normals[1], new Vector2(0, 0));
            AddVertex(edge3, normals[1], new Vector2(1, 0));
            AddVertex(edge4, normals[1], new Vector2(1, 1));


            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge5, normals[0], new Vector2(0, 1));
            AddVertex(edge6, normals[0], new Vector2(0, 0));
            AddVertex(edge7, normals[0], new Vector2(1, 0));
            AddVertex(edge8, normals[0], new Vector2(1, 1));

            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge7, normals[2], new Vector2(0, 1));
            AddVertex(edge4, normals[2], new Vector2(0, 0));
            AddVertex(edge3, normals[2], new Vector2(1, 0));
            AddVertex(edge8, normals[2], new Vector2(1, 1));

            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge5, normals[3], new Vector2(0, 1));
            AddVertex(edge2, normals[3], new Vector2(0, 0));
            AddVertex(edge1, normals[3], new Vector2(1, 0));
            AddVertex(edge6, normals[3], new Vector2(1, 1));


            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge7, normals[4], new Vector2(0, 1));
            AddVertex(edge6, normals[4], new Vector2(0, 0));
            AddVertex(edge1, normals[4], new Vector2(1, 0));
            AddVertex(edge4, normals[4], new Vector2(1, 1));


            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 1);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 0);
            AddIndex(CurrentVertex + 2);
            AddIndex(CurrentVertex + 3);
            AddVertex(edge3, normals[5], new Vector2(0, 1));
            AddVertex(edge2, normals[5], new Vector2(0, 0));
            AddVertex(edge5, normals[5], new Vector2(1, 0));
            AddVertex(edge8, normals[5], new Vector2(1, 1));

            InitializePrimitive(graphicsDevice);
        }
    }
}
