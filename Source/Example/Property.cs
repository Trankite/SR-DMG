using System.Linq.Expressions;
using System.Reflection;

namespace SR_DMG.Source.Example
{
    public class Property<TKey, TValue>(Func<TKey, TValue> GetFunc, Action<TKey, TValue> SetAction)
    {
        public Func<TKey, TValue> GetValue = GetFunc;

        public Action<TKey, TValue> SetValue = SetAction;

        public static void Initialize<TSetKey, TSetValue>(Dictionary<string, Property<TSetKey, TSetValue>> Properties, Dictionary<string, string> Keys)
        {
            Type Type = typeof(TSetKey);
            foreach (PropertyInfo Prop in Type.GetProperties())
            {
                if (!Keys.TryGetValue(Prop.Name, out string? PropertName)) return;
                ParameterExpression PropParam = Expression.Parameter(Type);
                ParameterExpression ValueParam = Expression.Parameter(typeof(TSetValue));
                MemberExpression PropAccess = Expression.Property(Expression.Convert(PropParam, Type), Prop);
                Properties[PropertName] = new(Expression.Lambda<Func<TSetKey, TSetValue>>(PropAccess, PropParam).Compile(),
                    Expression.Lambda<Action<TSetKey, TSetValue>>(Expression.Assign(PropAccess, ValueParam), PropParam, ValueParam).Compile());
            }
        }
    }
}