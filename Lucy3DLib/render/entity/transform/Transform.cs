using OpenTK.Mathematics;

namespace Lucy3DLib
{
    /// <summary>
    /// Transform - This class contains the translation, scaling, rotation, 
    /// and matrix values for an entity’s transform. The transform metric 
    /// is also calculated by multiplying the identity matrix with the 
    /// rotation, translation, and scaling values.
    /// </summary>
    class Transform
    {
        // Translation Matrix Values
        private Translate translate = null;

        // Scale Matrix Values
        private Scale scale = null;

        // Rotation Matrix Values
        private Rotate rotate = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Transform()
        {
            translate = new Translate();
            scale = new Scale();
            rotate = new Rotate();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public void Move(float x, float y, float z) => translate.Move(x, y, z);

        public void Turn(float x, float y, float z) => rotate.Turn(x, y, z);

        public void Size(float x, float y, float z) => scale.Size(x, y, z);

        public Matrix4 CalcTransform()
        {
            var transform =
                Matrix4.Identity *
                rotate.Calculate() *
                translate.Calculate() *
                scale.Calculate();

            return (transform);
        }
    }
}
