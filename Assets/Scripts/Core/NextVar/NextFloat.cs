namespace Corebin.Core.NextVar
{
    [System.Serializable]
    public class NextFloat : NextVar<float>
    {
        #region Constructors
        public NextFloat()
        {
            val = 0f;
        }

        public NextFloat(float value)
        {
            val = value;
        }
        #endregion Constructors

        #region Operators
        public static implicit operator float(NextFloat operand)
        {
            return operand == null ? 0f : (float)operand.val;
        }
        #endregion Operators

        #region ToString
        public override string ToString()
        {
            return val.ToString();
        }
        #endregion ToString
    }
}

