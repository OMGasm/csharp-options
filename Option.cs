namespace Options;

public abstract class Option<T>
{
    public static implicit operator Option<T>(Task<NoneT> t) => new NoneT<T>();

    public override string ToString() => this switch
    {
        NoneT<T> n => "None",
        Some<T> s => $"Some({s.Unwrap()})",
        _ => throw new Exception()
    };

    public abstract T Unwrap();
}

public sealed class Some<T> : Option<T>
{
    T val;
    public Some(T val) => this.val = val;

    public override T Unwrap() => val;
};

public sealed class NoneT<T> : Option<T>
{
    public static implicit operator NoneT<T>(NoneT n) => new();
    public override T Unwrap() => throw new NotImplementedException();

}
public sealed class NoneT;

public sealed class Prelude
{
    public static Some<T> Some<T>(T val) => new Some<T>(val);
    public static Task<NoneT> None => Task.FromResult(new NoneT());
}

