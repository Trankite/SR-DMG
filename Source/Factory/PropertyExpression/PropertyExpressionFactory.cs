using System.Linq.Expressions;
using System.Reflection;

namespace StarRailDamage.Source.Factory.PropertyExpression
{
    internal class PropertyExpressionFactory
    {
        public static PropertyExpression<TSender, TValue> GetPropertyExpression<TSender, TValue>(params PropertyInfo[] properties)
        {
            Type SenderType = typeof(TSender);
            ParameterExpression KeyParameter = Expression.Parameter(SenderType);
            ParameterExpression ValueParameter = Expression.Parameter(typeof(TValue));
            MemberExpression PropertyAccess = Expression.Property(KeyParameter, properties.First());
            for (int i = 1; i < properties.Length; i++)
            {
                PropertyAccess = Expression.Property(PropertyAccess, properties[i]);
            }
            return new()
            {
                GetValue = Expression.Lambda<Func<TSender, TValue>>(PropertyAccess, KeyParameter).Compile(),
                SetValue = Expression.Lambda<Action<TSender, TValue>>(Expression.Assign(PropertyAccess, ValueParameter), KeyParameter, ValueParameter).Compile()
            };
        }

        public static PropertyInfo[] FindProperty(Type type, params string[] propertyNames)
        {
            PropertyInfo? CurrentProperty;
            if (propertyNames.Length == 1)
            {
                propertyNames = propertyNames.First().Split('.');
            }
            PropertyInfo[] Properties = new PropertyInfo[propertyNames.Length];
            for (int i = 0; i < propertyNames.Length; i++)
            {
                CurrentProperty = type.GetProperty(propertyNames[i]);
                ArgumentNullException.ThrowIfNull(CurrentProperty);
                Properties[i] = CurrentProperty;
                if (i < propertyNames.Length - 1)
                {
                    type = CurrentProperty.PropertyType;
                }
            }
            return Properties;
        }
    }
}