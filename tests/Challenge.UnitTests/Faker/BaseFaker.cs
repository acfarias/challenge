using Bogus;
using Moq.AutoMock;

namespace Challenge.UnitTests.Faker
{
    public abstract class BaseFaker
    {
        public AutoMocker AutoMocker;

        protected TObject GenericInstance<TObject>() where TObject : class
        {
            AutoMocker = new AutoMocker();
            return AutoMocker.CreateInstance<TObject>();
        }

        protected static Faker<TObject> FakerObject<TObject>() where TObject : class => new Faker<TObject>();
    }
}