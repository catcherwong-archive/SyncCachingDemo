namespace Item.Services
{
    public interface IDemoService
    {
        object Get();
    }

    public class DemoService : IDemoService
    {
        public object Get()
        {
            return "Demo";
        }
    }
}
