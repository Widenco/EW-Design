using EWDesign.Core;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EWDesign.View
{
    /// <summary>
    /// Interaction logic for ComponentEditorDialog.xaml
    /// </summary>
    public partial class ComponentEditorDialog : Window
    {
        private readonly ComponentModel _model;
        public ComponentEditorDialog(ComponentModel model)
        {
            InitializeComponent();
            _model = model;
            GenerateForm();
        }

        private void GenerateForm()
        {
            //Recuperar las propiedades que tengan un binding
            var props = _model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                          .Where(p => p.CanRead && p.CanWrite && 
                          p.GetCustomAttribute<EditablePropertyAttribute>() != null);

            foreach (var prop in props)
            {
                //Filtrando propiedades editables
                var attr = prop.GetCustomAttribute<EditablePropertyAttribute>();
                var displayName = attr?.DisplayName ?? prop.Name;

                //Un label para cada propiedad
                var label = new TextBlock
                {
                    Text = displayName,
                    Margin = new Thickness(0, 10, 0, 2),
                    FontWeight = FontWeights.Bold
                };
                FormPanel.Children.Add(label);

                //Su respectivo tipo de formulario
                FrameworkElement editor = CreateEditor(prop);
                if (editor != null)
                {
                    FormPanel.Children.Add(editor);
                }
            }

            var saveBtn = new Button
            {
                Content = "Save",
                Margin = new Thickness(0, 20, 0, 0),
                IsDefault = true
            };
            saveBtn.Click += (s, e) => this.DialogResult = true;
            FormPanel.Children.Add(saveBtn);
        }

        private FrameworkElement CreateEditor(PropertyInfo prop)
        {
            //Creando un binding segun la propiedad pasada como parametro
            var binding = new Binding(prop.Name)
            {
                Source = _model,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            //Comprobando el tipo de la propiedad devolviendo su respectivo tipo de formulario
            if (prop.PropertyType == typeof(string))
            {
                var tb = new TextBox { MinWidth = 200 };
                tb.SetBinding(TextBox.TextProperty, binding);
                return tb;
            }
            else if (prop.PropertyType == typeof(double))
            {
                var cb = new TextBox { MinWidth = 200 };
                cb.SetBinding(TextBox.TextProperty, binding);
                return cb;
            }
            else if (prop.PropertyType == typeof(Brush))
            {
                // Opcional: para representar color como string
                var colorBinding = new Binding($"{prop.Name}.Color")
                {
                    Source = _model,
                    Mode = BindingMode.TwoWay,
                    Converter = new ColorToHexConverter()
                };
                var tb = new TextBox { MinWidth = 200 };
                tb.SetBinding(TextBox.TextProperty, colorBinding);
                return tb;
            }
            else if (prop.PropertyType == typeof(FontWeight))
            {
                var tb = new ComboBox { Items = { FontWeights.Black, FontWeights.Bold}, MaxWidth = 300 };
                tb.SelectedItem = tb.Items[0];
                tb.SetBinding(ComboBox.SelectedItemProperty, binding);
                return tb;
            }
            else if (prop.PropertyType == typeof(TextAlignment))
            {
                var tb = new ComboBox { Items = { TextAlignment.Center, TextAlignment.Left, TextAlignment.Right }, MaxWidth = 300 };
                tb.SelectedItem = tb.Items[0];
                tb.SetBinding(ComboBox.SelectedItemProperty, binding);
                return tb;
            }
            else
            {
                // No editable o no soportado (por ahora)
                return null;
            }
        }
    }
}
