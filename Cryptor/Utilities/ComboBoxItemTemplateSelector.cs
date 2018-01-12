using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cryptor.Utilities
{
    public class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            while (container != null)
            {
                container = VisualTreeHelper.GetParent(container);
                if (container is ComboBoxItem)
                    return DropDownTemplate;
            }
            return SelectedTemplate;
        }
    }
}
