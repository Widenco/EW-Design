using EWDesign.Components.Models;
using EWDesign.Core;
using EWDesign.Core.Code_Generator;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;

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
            FormPanel.Children.Clear();

            // 1. Renderizar propiedades del componente principal
            AddSection(_model, $"{_model.Type} Properties");

            // 2. Renderizar propiedades de subcomponentes si no son componentes de layout
            if(_model.Type != "NavBar" && _model.Type != "Body" && _model.Type != "Footer")
            {
                foreach (var child in _model.Children)
                {
                    AddSection(child, $"{child.Type} Properties");
                }
            }
           
            // 3. Botón de guardar
            var saveBtn = new Button
            {
                Content = "Guardar",
                Margin = new Thickness(0, 20, 0, 0),
                IsDefault = true
            };
            saveBtn.Click += (s, e) => this.DialogResult = true;
            FormPanel.Children.Add(saveBtn);
        }


        private void AddSection(ComponentModel model, string title)
        {
            var sectionHeader = new TextBlock
            {
                Text = title,
                Margin = new Thickness(0, 20, 0, 10),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline
            };
            FormPanel.Children.Add(sectionHeader);

            var props = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.GetCustomAttribute<EditablePropertyAttribute>() != null);

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<EditablePropertyAttribute>();
                var displayName = attr?.DisplayName ?? prop.Name;

                var label = new TextBlock
                {
                    Text = displayName,
                    Margin = new Thickness(0, 10, 0, 2),
                    FontWeight = FontWeights.Normal
                };
                FormPanel.Children.Add(label);

                FrameworkElement editor = CreateEditor(prop, model);
                
                if (editor != null)
                {
                    FormPanel.Children.Add(editor);
                }
     
            }
        }


        private FrameworkElement CreateEditor(PropertyInfo prop, object model)
        {
            //Creando un binding segun la propiedad pasada como parametro
            var binding = new Binding(prop.Name)
            {
                Source = model,
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
                    Source = model,
                    Mode = BindingMode.TwoWay
                };
                var bt = new ColorCanvas();
                bt.UsingAlphaChannel = false;
                bt.SetBinding(ColorCanvas.SelectedColorProperty, colorBinding);
                return bt;
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
            else if (prop.PropertyType == typeof(ObservableCollection<TextComponent>))
            {
                var panel = new StackPanel();
                var MenuItems = (ObservableCollection<TextComponent>)prop.GetValue(model);

                for (int i = 0; i < MenuItems.Count; i++)
                {
                    var textComponent = MenuItems[i];

                    var tb = new TextBox
                    {
                        Margin = new Thickness(5)
                    };

                    // Binding directo a la propiedad Text del TextComponent
                    tb.SetBinding(TextBox.TextProperty, new Binding("Text")
                    {
                        Source = textComponent,
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

                    panel.Children.Add(tb);
                }

                return panel;
            }
            else
            {
                // No editable o no soportado (por ahora)
                return null;
            }
        }
    }
}
