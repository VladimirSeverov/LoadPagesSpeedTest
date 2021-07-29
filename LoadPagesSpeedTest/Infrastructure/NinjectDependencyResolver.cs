using LoadPagesSpeedTest.Models;
using LoadPagesSpeedTest.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LoadPagesSpeedTest.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IRepository<Test>>().To<TestRepository>();
            kernel.Bind<IRepository<TestDetails>>().To<TestDetailsRepository>();
        }
    }
}