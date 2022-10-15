using OpenTK.Mathematics;

namespace Lucy3DLib
{
    /// <summary>
    /// Scale - This class contains the scaling components of and object's 
    /// matrix transform.
    /// </summary>
    class Scale
    {
        // X Scale Attribute
        private float X { get; set; } = 1.0f;

        // Y Scale Attribute
        private float Y { get; set; } = 1.0f;

        // Z Scale Attribute
        private float Z { get; set; } = 1.0f;

        /// <summary>
        /// Size() - 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Size(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Calculate() - Calculates the scale matrix component, used when 
        /// calculating an object's transform.
        /// </summary>
        /// <returns></returns>
        public Matrix4 Calculate()
        {
            return (Matrix4.CreateScale(X, Y, Z));
        }
    }
}
