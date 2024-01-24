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

    public abstract bool IsSome();
    public abstract bool IsSomeAnd(Func<T, bool> f);
    public abstract bool IsNone();
    public abstract Span<T> AsSpan();
    public abstract T Expect(string msg);
    public abstract T UnwrapOr(T alt);
    public abstract T UnwrapOrElse(Func<T> f);
    public unsafe abstract T UnwrapUnchecked();
    public abstract T Unwrap();
}

public sealed class Some<T> : Option<T>
{
    T val;
    public Some(T val) => this.val = val;

    public override Span<T> AsSpan() => new Span<T>(new[] { val });

    public override T Expect(string msg) => val;

    public override bool IsNone() => false;

    public override bool IsSome() => true;

    public override bool IsSomeAnd(Func<T, bool> f) => f(val);

    public override T Unwrap() => val;

    public override T UnwrapOr(T alt) => val;

    public override T UnwrapOrElse(Func<T> f) => val;

    public unsafe override T UnwrapUnchecked() => val;
};

public sealed class NoneT<T> : Option<T>
{
    public static implicit operator NoneT<T>(NoneT n) => new();

    public override T Unwrap() => throw new Exception("Unwrap None");

    public override bool IsSome() => false;

    public override bool IsSomeAnd(Func<T, bool> f) => false;

    public override bool IsNone() => true;

    public override Span<T> AsSpan() => Span<T>.Empty;

    public override T Expect(string msg) => throw new Exception(msg);

    public override T UnwrapOr(T alt) => alt;

    public override T UnwrapOrElse(Func<T> f) => f();

    public unsafe override T UnwrapUnchecked() => throw new System.Diagnostics.UnreachableException();
}
public sealed class NoneT;

public sealed class Prelude
{
    public static Some<T> Some<T>(T val) => new Some<T>(val);
    public static Task<NoneT> None => Task.FromResult(new NoneT());
}

