using System.Reflection;
using Ninject;
namespace Negocio.Servicios
{
    public class ServicioBase
    {
        public IKernel kernel = new StandardKernel();

        public ServicioBase()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
