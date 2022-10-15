using OpenTK.Mathematics;

namespace Lucy3DLib
{
    /// <summary>
    /// Translate - This class contains the translation components of an object's
    /// matrix transform.
    /// </summary>
    class Translate
    {
        // X Translate Attribute
        public float X { get; set; } = 0.0f;

        // Y Translate Attribute
        public float Y { get; set; } = 0.0f;

        // Z Translate Attribute
        public float Z { get; set; } = 0.0f;

        /// <summary>
        /// Move() - Translate the position of an object by off setting its
        /// x, y, z components.
        /// </summary>
        public void Move(float x, float y, float z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        /// <summary>
        /// Calculate() - Calculates the translate matrix component, used when
        /// calculating an object's transform.
        /// </summary>
        public Matrix4 Calculate()
        {
            return (Matrix4.CreateTranslation(X, Y, Z));
        }
    }
}
