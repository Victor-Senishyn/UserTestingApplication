namespace UserTestingApplication.Exceptions
{
    public class TestAlreadyPassedException : Exception
    {
        public TestAlreadyPassedException() { }
        public TestAlreadyPassedException(string message) : base(message) { }
        public TestAlreadyPassedException(string message, Exception innerException) : base(message, innerException) { }
    }

}
