// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToLambdaExpression
#pragma warning disable 169
using Rhino.Mocks;
using Rhino.Testing.AutoMocking;

namespace neuralnet.tests
{
    public class SpecBase
    {
        private static readonly AutoMockingContainer _autoMockingContainer;
        private static readonly MockRepository _mockRepository;
        
        static SpecBase()
        {
            _mockRepository = new MockRepository();
            _autoMockingContainer = new AutoMockingContainer(_mockRepository);
            _autoMockingContainer.Initialize();
        }

        public static I an<I>() where I : class
        {
            var mock = _autoMockingContainer.Get<I>();
            mock.Replay();
            return mock;
        }

        public static T create<T>()
        {
            return _autoMockingContainer.Create<T>();
        }
    }
}

#pragma warning restore 169
// ReSharper restore InconsistentNaming
// ReSharper restore ConvertToLambdaExpression