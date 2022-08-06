using NeoMath;

namespace Test.NeoMath;

public class CalculatorTest
{
    [Fact]
    public void Simple()
    {
        string line1 = "2+2";
        double res1 = 4;

        Assert.True(Calculator.Calculate(line1) == res1);

        string line2 = "2-2";
        double res2 = 0;

        Assert.True(Calculator.Calculate(line2) == res2);

        string line3 = "2*2";
        double res3 = 4;

        Assert.True(Calculator.Calculate(line3) == res3);

        string line4 = "2/2";
        double res4 = 1;

        Assert.True(Calculator.Calculate(line4) == res4);
    }

    [Fact]
    public void Priority()
    {
        string line1 = "2+2*2";
        double res1 = 6;

        Assert.True(Calculator.Calculate(line1) == res1);

        string line2 = "2*3-4/4";
        double res2 = 5;

        Assert.True(Calculator.Calculate(line2) == res2);

        string line3 = "2/2-3/3+1-1-10/2/5";
        double res3 = -1;

        Assert.True(Calculator.Calculate(line3) == res3);

        string line4 = "1+1+1+1+5/2";
        double res4 = 6.5;

        Assert.True(Calculator.Calculate(line4) == res4);
    }

    [Fact]
    public void PriorityWithBracket()
    {
        string line1 = "2/(4-2)";
        double res1 = 1;

        Assert.True(Calculator.Calculate(line1) == res1);

        string line2 = "(2*2)/(8-6)";
        double res2 = 2;

        Assert.True(Calculator.Calculate(line2) == res2);

        string line3 = "((8-5)/2)-1";
        double res3 = 0.5;

        Assert.True(Calculator.Calculate(line3) == res3);

        string line4 = "(((((((2+2)))))))";
        double res4 = 4;

        Assert.True(Calculator.Calculate(line4) == res4);
    }

    [Fact]
    public void MulWithBracket()
    {
        string line1 = "2(2)";
        double res1 = 4;

        Assert.True(Calculator.Calculate(line1) == res1);

        string line2 = "3(1+1)";
        double res2 = 6;

        Assert.True(Calculator.Calculate(line2) == res2);

        string line3 = "(6)2";
        double res3 = 12;

        Assert.True(Calculator.Calculate(line3) == res3);

        string line4 = "5(((((2)))))2";
        double res4 = 20;

        Assert.True(Calculator.Calculate(line4) == res4);
    }
}