namespace Common.Tests
{
    
    public interface ITestResult<T1, TResult>
    {
        TResult Result { get; set; }
        T1 Data { get; set; }
    }
}