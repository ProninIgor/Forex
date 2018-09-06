using System.Collections.Generic;
using Ninject;
using Ninject.Parameters;

namespace Common.IoC
{
    public static class IoC
    {
        private static IKernel _ninjectKernel;

        public static T Get<T>()
        {
            if(_ninjectKernel == null)
                _ninjectKernel = new StandardKernel(new CommonConfigModule());
            
            return _ninjectKernel.Get<T>();
        }
        
        public static T Get<T>(List<IParameter> parameters)
        {
            if(_ninjectKernel == null)
                _ninjectKernel = new StandardKernel(new CommonConfigModule());

            return _ninjectKernel.Get<T>(parameters.ToArray());
        }

        public static IParameter GetCtorParam(string name, object value)
        {
            return new Ninject.Parameters.ConstructorArgument(name, value);
        }
    }
}