namespace Common.Tests
{
    public class TestResult<T1, TResult> : ITestResult<T1, TResult>
    {
        public T1 Data { get; set; }
        public TResult Result { get; set; }
    }
}