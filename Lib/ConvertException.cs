
namespace tilecon.Core
{
    public class ConvertException : Exception
    {
        public ConvertException(string message) : base (message)  { }
    }
  
    public class SizeException : ConvertException {
        public SizeException(string message) : base (message)  { }
    }
}
