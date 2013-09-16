using System;

namespace WLG.DAL
{
    [Serializable]
    public class DALException : Exception
    {
        public DALException()
            : base()
        {
        }

        public DALException(string message)
            : base(message)
        {

        }

        public DALException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
