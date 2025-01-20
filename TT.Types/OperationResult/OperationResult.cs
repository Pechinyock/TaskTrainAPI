namespace TT.OperationResult;

public readonly struct OperationResult<ValueType, ErrorType>
{
    private readonly bool _success;
    private readonly ValueType _value;
    private readonly ErrorType _error;

    public OperationResult(ValueType value, ErrorType error, bool success)
    {
        _error = error;
        _value = value;
        _success = success;
    }

    public static OperationResult<ValueType, ErrorType> Success(ValueType value) => new (value, default(ErrorType), true);
    public static OperationResult<ValueType, ErrorType> Failure(ErrorType error) => new(default(ValueType), error, false);

    public static implicit operator OperationResult<ValueType, ErrorType>(ValueType value) => Success(value);
    public static implicit operator OperationResult<ValueType, ErrorType>(ErrorType error) => Failure(error);

    public R Match<R>(Func<ValueType, R> OnSuccess, Func<ErrorType, R> OnFailure)
    {
        return _success 
            ? OnSuccess(_value)
            : OnFailure(_error);
    }
}
