[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HastaneYonetim.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HastaneYonetim.App_Start.NinjectWebCommon), "Stop")]

namespace HastaneYonetim.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

  
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                kernel.Bind(x =>
                {
                    x.FromThisAssembly()
                        .SelectAllClasses()
                        .BindDefaultInterface();
                });
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}
