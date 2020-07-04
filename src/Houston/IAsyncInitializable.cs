using System.Threading.Tasks;

namespace Houston
{
    public interface IAsyncInitializable
    {
        Task InitializeAsync();
    }
}
