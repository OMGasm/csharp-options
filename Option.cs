namespace Options;

public abstract class Option<T>
{
    public static implicit operator Option<T>(Task<NoneT> t) => new None<T>();

    protected Option() { }

    public override string ToString()
    {
        return this switch
        {
            Some<T> some => $"Some({some.Unwrap()})",
            None<T> none => "None",
            Option<T> _ => throw new Exception()
        };
    }

    public abstract T Unwrap();
}

public sealed class Some<T> : Option<T>
{
    T val;
    public Some(T val) => this.val = val;

    public override T Unwrap() => val;
};

public sealed class None<T> : Option<T>
{
    public override T Unwrap() => throw new Exception();
}

public sealed class NoneT;

public sealed class Prelude
{
    public static Some<T> Some<T>(T val) => new Some<T>(val);
    public static Task<NoneT> None => Task<NoneT>.FromResult(new NoneT());
}

