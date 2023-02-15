using System.Threading.Tasks;

namespace FlagCapturing.Configs
{
    public interface IConfigLoader<T> where T: Config
    {
        Task<T> LoadConfigAsync();
    }
}
