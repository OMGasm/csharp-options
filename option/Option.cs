namespace Options;

/// <include file='docs.xml' path='namespace[@name="options"]/Option/class/*'/>
public abstract class Option<T>
{
    public static implicit operator Option<T>(Task<NoneT> t) => new NoneT<T>();

    public override string ToString() => this switch
    {
        NoneT<T> n => "None",
        Some<T> s => $"Some({s.Unwrap()})",
        _ => throw new Exception()
    };

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/IsSome/*'/>
    public abstract bool IsSome();

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/IsSomeAnd/*'/>
    public abstract bool IsSomeAnd(Func<T, bool> f);

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/IsNone/*'/>
    public abstract bool IsNone();

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/AsSpan/*'/>
    public abstract Span<T> AsSpan();

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/Expect/*'/>
    public abstract T Expect(string msg);

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/UnwrapOr/*'/>
    public abstract T UnwrapOr(T alt);

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/UnwrapOrElse/*'/>
    public abstract T UnwrapOrElse(Func<T> f);

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/UnwrapUnchecked/*'/>
    public unsafe abstract T UnwrapUnchecked();

    /// <include file='docs.xml' path='namespace[@name="options"]/Option/Unwrap/*'/>
    public abstract T Unwrap();

}

/// <include file='docs.xml' path='namespace[@name="options"]/Some/class/*'/>
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

/// <include file='docs.xml' path='namespace[@name="options"]/NoneT/class/*'/>
public class NoneT<T> : Option<T>
{
    // public static implicit operator NoneT<T>(NoneT n) => new();

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

/// <include file='docs.xml' path='namespace[@name="options"]/NoneT_stub/class/*'/>
public sealed class NoneT : NoneT<object>
{
    public override object UnwrapOr(object alt) => base.UnwrapOr(alt);
}

/// <include file='docs.xml' path='namespace[@name="options"]/Prelude/class/*'/>
public sealed class Prelude
{
    public static Option<T> Some<T>(T val) => new Some<T>(val);
    public static Task<NoneT> None => Task.FromResult(new NoneT());
}

