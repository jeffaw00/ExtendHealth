using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using IoC;

namespace IoCTest
{
    [TestFixture]
    public class IoCTesting
    {
        IoC.Container container = new IoC.Container();
        
        [Test]
        public void TestRegister()
        {
            container.Register<IRepository, Repository>(); 
        }

        [Test]
        public void TestRegisterTwice()
        {
            Exception twiceEx = null;
            try
            {
                container.Register<IRepository, Repository>();
            }
            catch(Exception ex)
            {
                twiceEx = ex;
            }
            Assert.IsNotNull(twiceEx);
        }

        [Test]
        public void TestResolve()
        {
            container.Register<IRepository, Repository>();
            container.Resolve<IRepository>(); 
        }

        [Test]
        public void TestResolveNotKnown()
        {
            Exception exNotKnown = null;
            try
            {
                container.Resolve<IRepository>();
            }
            catch(Exception ex)
            {
                exNotKnown = ex;
            }
            Assert.IsNotNull(exNotKnown);
        }
    }
}
