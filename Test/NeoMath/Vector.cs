using NeoMath;

namespace Test.NeoMath;

public class VectorTest
{
    [Fact]
    public void OperationWithVector()
    {
        Vector a1 = new(new double[] { 1, 2, 3, 3});
        Vector b1 = new(new double[] { 0, 1, 0, 3});

        Vector res1 = new(new double[] { 1, 3, 3, 6 });
        Assert.True(a1 + b1 == res1);

        Vector a2 = new(new double[] { 1, 3 });
        Vector b2 = new(new double[] { 2, 5 });

        Vector res2 = new(new double[] { 3, 8 });
        Assert.True(a2 + b2 == res2);

        Vector a3 = new(new double[] { 1, 2, 8 });
        Vector b3 = new(new double[] { -1, -2, 0 });

        Vector res3 = new(new double[] { 2, 4, 8 });
        Assert.True(a3 - b3 == res3);

        Vector a4 = new(new double[] { 1, 2, 3, 3 });
        Vector b4 = new(new double[] { 1, 2, 3, 3 });

        Vector res4 = new(new double[] { 0, 0, 0, 0 });
        Assert.True(a4 - b4 == res4);
    }

    [Fact]
    public void OperationWithBoll()
    {
        Vector a1 = new(new double[] { 1, 2, 3, 3 });
        Vector b1 = new(new double[] { 1, 2, 3, 3 });

        Assert.True(a1 == b1);

        Vector a2 = new(new double[] { 1, 3 });
        Vector b2 = new(new double[] { 1, 1 });

        Assert.True(a2 != b2);

        Vector a3 = new(new double[] { 1, 1, 1, 1 });
        Vector b3 = new(new double[] { 1, 2, 3, 3 });

        Assert.True(a3 <= b3);

        Vector a4 = new(new double[] { 1, 4, 3, 3 });
        Vector b4 = new(new double[] { 1, 2, 3, 3 });

        Assert.True(a4 >= b4);

        Vector a5 = new(new double[] { 1, 3, 3 });
        Vector b5 = new(new double[] { 3, 4, 4 });

        Assert.True(a5 < b5);

        Vector a6 = new(new double[] { 6, 5, 9, 7 });
        Vector b6 = new(new double[] { 1, 2, 3, 3 });

        Assert.True(a6 > b6);
    }

    [Fact]
    public void OperationWithNumber()
    {
        Vector a1 = new(new double[] { 1, 2, 3, 3 });
        int b1 = 2;

        Vector res1 = new(new double[] { 2, 4, 6, 6 });
        Assert.True(a1 * b1 == res1);

        Vector a2 = new(new double[] { 1, 3 });
        int b2 = 0;

        Vector res2 = new(new double[] { 0, 0 });
        Assert.True(b2 * a2 == res2);

        Vector a3 = new(new double[] { 2, 2, 2, 2 });
        int b3 = 2;
    }
}