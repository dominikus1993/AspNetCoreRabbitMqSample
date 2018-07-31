namespace Fib.Service.Services
{
    public class FibCalculator : IFibCalculator
    {
        public int Calculate(int number)
        {
            var x = 0;
            var y = 1;
            var z = 1;
            for (var i = 0; i < number; i++) {
                x = y;
                y = z;
                z = x + y;
            }
            return x;
        }
    }
}