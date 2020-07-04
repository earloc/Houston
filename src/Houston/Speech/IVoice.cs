using System.Threading.Tasks;

namespace Houston.Speech
{
    public interface IVoice
    {
        Task SayAsync(string message);
    }

}
