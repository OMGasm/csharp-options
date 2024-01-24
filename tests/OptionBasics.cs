namespace tests;

public class OptionBasics
{
    [SetUp]
    public void Setup()
    {
    }



    [Test]
    public void IsSome()
    {
        Option<UInt32> x = Some(2U);
        Assert.True(x.IsSome(), "Some");

        x = None;
        Assert.False(x.IsSome(), "None");
    }

    [Test]
    public void IsSomeAnd()
    {
        Option<UInt32> x = Some(2U);
        Assert.True(x.IsSomeAnd(x => x > 1));

        x = Some(0U);
        Assert.False(x.IsSomeAnd(x => x > 1));

        x = None;
        Assert.False(x.IsSomeAnd(x => x > 1));
    }

    [Test]
    public void IsNone()
    {
        Option<UInt32> x = Some(2U);
        Assert.False(x.IsNone());

        x = None;
        Assert.True(x.IsNone());
    }

    [Test]
    public void AsSpan()
    {
        var x = Some(1234);
        var expected = new[] { 1234 }.AsSpan();
        Assert.That(x.AsSpan().SequenceEqual(expected));

        x = None;
        Assert.That(x.AsSpan().IsEmpty);
    }

    [Test]
    public void Expect()
    {
        var x = Some("value");
        Assert.AreEqual(x.Expect("fruits are healthy"), "value");

        x = None;
        Assert.Throws<Exception>(() => x.Expect("fruits are healthy"));
    }

    [Test]
    public void Unwrap()
    {
        var x = Some("air");
        Assert.AreEqual(x.Unwrap(), "air");

        x = None;
        Assert.Throws<Exception>(() => x.Unwrap());
    }

    [Test]
    public void UnwrapOr()
    {
        var x = Some("car");
        Assert.AreEqual(x.UnwrapOr("bike"), "car");

        x = None;
        Assert.AreEqual(x.UnwrapOr("bike"), "bike");
    }

    [Test]
    public void UnwrapOrElse()
    {
        var k = 10;
        var x = Some(4);
        Assert.AreEqual(x.UnwrapOrElse(() => 2 * k), 4);

        x = None;
        Assert.AreEqual(x.UnwrapOrElse(() => 2 * k), 20);
    }
}
