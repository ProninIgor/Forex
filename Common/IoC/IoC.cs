using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Common.IoC
{
    public static class IoC
    {
        private static IKernel _standartNinjectKernel;
        
        private static IKernel _fakeNinjectKernel;

        public static IKernel Standart => _standartNinjectKernel ?? (_standartNinjectKernel = new StandardKernel(new CommonConfigModule()));
        
        public static IKernel Fake => _fakeNinjectKernel;

        /// <summary>
        /// Добавить фейковый модуль
        /// </summary>
        /// <param name="modules"></param>
        public static void SetFake(params INinjectModule[] modules)
        {
            _fakeNinjectKernel = new StandardKernel(modules);
        }

        public static T Get<T>()
        {
            return _standartNinjectKernel.Get<T>();
        }
        
        public static T Get<T>(List<IParameter> parameters)
        {
            if(_standartNinjectKernel == null)
                _standartNinjectKernel = new StandardKernel(new CommonConfigModule());

            return _standartNinjectKernel.Get<T>(parameters.ToArray());
        }

        public static IParameter GetCtorParam(string name, object value)
        {
            return new Ninject.Parameters.ConstructorArgument(name, value);
        }
        
        
    }
}