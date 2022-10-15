using OpenTK;
using OpenTK.Mathematics;

namespace Lucy3DLib
{
    /// <summary>
    /// Rotate - This class contains the rotation component of an object's 
    /// matrix transform.
    /// </summary>
    class Rotate
    {
        // X Rotate Attribute
        private float X { get; set; } = 0.0f;

        // Y Rotate Attribute
        private float Y { get; set; } = 0.0f;

        // Z Rotate Attribute
        private float Z { get; set; } = 0.0f;

        /// <summary>
        /// Set() - Sets the x, y, z - attributes of an objects rotation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Turn() - Adds or substracts from the rotation components to turn
        /// the object on any axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Turn(float x, float y, float z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        /// <summary>
        /// Calculate() - Calculates the rotate matrix component, used when
        /// calculating an objects transform.
        /// </summary>
        /// <returns></returns>
        public Matrix4 Calculate()
        {
            Matrix4 rotate =
                Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(X)) *
                Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(Y)) *
                Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(Z));

            return (rotate);
        }
    }
}
