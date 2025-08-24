using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SR_DMG.Source.Material
{
    public class Program
    {
        public static void Debug()
        {
            //Mihomo.GetResources(5);
        }

        public static string GetPath(params string[] FileName)
        {
            return Path.Combine([Simple.RootFolder, .. FileName]);
        }

        public static ImageSource GetImage(string FileName)
        {
            return (ImageSource)Application.Current.Resources[FileName];
        }

        public static void SetField<T>(ref T Field, T Value, string PropertyName, Action<string> OnChanged)
        {
            if (EqualityComparer<T>.Default.Equals(Field, Value)) return;
            Field = Value; OnChanged?.Invoke(PropertyName);
        }

        public static T? GetControl<T>(object? sender) where T : DependencyObject
        {
            DependencyObject? Depen = sender as DependencyObject;
            while (Depen != null && Depen is not T)
            {
                Depen = VisualTreeHelper.GetParent(Depen);
            }
            return Depen as T;
        }
    }
}