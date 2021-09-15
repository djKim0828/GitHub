namespace EmWorks.DevDrv.KeyenceIL100
{
    internal class Idx
    {
        #region Classes

        public class FunctionResult
        {
            #region Fields

            public const int False = 0;
            public const int True = 1;

            #endregion Fields
        }

        public class IoType
        {
            #region Fields

            public const int input = 0;
            public const int output = 1;
            public const int StateInput = 2;
            public const int StateOutput = 3;

            #endregion Fields
        }

        public class OpenClose
        {
            #region Fields

            public const int Close = 0;
            public const int Error = -1;
            public const int Open = 1;

            #endregion Fields
        }

        #endregion Classes
    }
}