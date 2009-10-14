// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToLambdaExpression
#pragma warning disable 169
using System;
using Rhino.Mocks;

namespace neuralnet.tests
{
    public static class RhinoMocksExtensions
    {
        public static VoidMethodCallOccurance<T> was_told_to<T>(this T mock, Action<T> item)
        {
            return new VoidMethodCallOccurance<T>(mock, item);
        }

        public static void was_never_told_to<T>(this T mock, Action<T> item)
        {
            mock.AssertWasNotCalled(item);
        }
    }

    public class VoidMethodCallOccurance<T>
    {
        private readonly Action<T> _action;
        private readonly T _mock;

        public VoidMethodCallOccurance(T mock, Action<T> action)
        {
            _mock = mock;
            _action = action;
            mock.AssertWasCalled(action);
        }

        public void times(int numberOfTimesTheMethodShouldHaveBeenCalled)
        {
            _mock.AssertWasCalled(_action, y => y.Repeat.Times(numberOfTimesTheMethodShouldHaveBeenCalled));
        }

        public void once()
        {
            times(1);
        }

        public void twice()
        {
            times(2);
        }
    }
}
#pragma warning restore 169
// ReSharper restore InconsistentNaming
// ReSharper restore ConvertToLambdaExpression