using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class DataCopier<TSource, TDestination> 
    where TSource : class where TDestination : class
    {
        public static void Copy(TSource source, TDestination destination)
        {
            if (source == null || destination==null)
            {
                throw new Exception("source or destination is not defined in datacopier.cs");
            }

            var parentProperties = source.GetType().GetProperties();
            var childProperties = destination.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && 
                        parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(destination, parentProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}
