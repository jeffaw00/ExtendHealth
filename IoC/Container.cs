using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class Container
    {
        private Dictionary<Type, iocType> item = new Dictionary<Type, iocType>();

        public void Register<ItemToResolve, Item>(LifestyleType instanceMode = LifestyleType.Transient)
        {
            if(item.ContainsKey(typeof(ItemToResolve)))
            {
                throw new Exception(typeof(ItemToResolve).ToString() + " already exists");
            }

            iocType iType = new iocType(typeof(Item), instanceMode);

            item.Add(typeof(ItemToResolve), iType);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type resType)
        {
            if(!item.ContainsKey(resType))
            {
                throw new Exception("You must register " + resType.ToString() + " first");
            }

            iocType rType = item[resType];

            if (rType.lifestyleType == LifestyleType.Singleton && rType.itemValue != null)
                return rType.itemValue;

            ConstructorInfo cInfo = rType.rType.GetConstructors().First();

            List<ParameterInfo> lParams = cInfo.GetParameters().ToList();
            List<object> rParams = new List<object>();
            foreach(var p in lParams)
            {
                Type t = p.ParameterType;
                object res = Resolve(t);
                rParams.Add(res);
            }

            object objRet = cInfo.Invoke(rParams.ToArray());
            return objRet;
        }
    }

    public enum LifestyleType
    {
        Transient = 1,
        Singleton
    }

    public class iocType
    {
        public Type rType { get; set; }
        public LifestyleType lifestyleType { get; set; }
        public object itemValue { get; set; }

        public iocType(Type rType)
        {
            this.rType = rType;
            this.lifestyleType = LifestyleType.Transient;
            this.itemValue = null;
        }

        public iocType(Type rType, LifestyleType lifestyleType)
        {
            this.rType = rType;
            this.lifestyleType = lifestyleType;
            this.itemValue = null;
        }
    }
}
