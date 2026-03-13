using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace TextEditorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        enum FormatingMode
        {
            bold, italic, underline
        }

        private void ToggleFormat(object buttonSender, DependencyProperty property, FormatingMode mode, object activeValue, object normalValue)
        {
            if (!rtb.Selection.IsEmpty)
            {
                rtb.Selection.ApplyPropertyValue(property, ((ToggleButton)buttonSender).IsChecked == true ? activeValue : normalValue);
            }

            else
            {
                switch (mode)
                {
                    case FormatingMode.bold:
                        if (EditingCommands.ToggleBold.CanExecute(null, rtb))
                        {
                            EditingCommands.ToggleBold.Execute(null, rtb);
                        }
                        break;
                    case FormatingMode.italic:
                        if (EditingCommands.ToggleItalic.CanExecute(null, rtb))
                        {
                            EditingCommands.ToggleItalic.Execute(null, rtb);
                        }
                        break;
                    case FormatingMode.underline:
                        if (EditingCommands.ToggleUnderline.CanExecute(null, rtb))
                        {
                            EditingCommands.ToggleUnderline.Execute(null, rtb);
                        }
                        break;
                    default:
                        break;
                }
            }

            //Возвращаем фокус в поле с текстом
            rtb.Focus();
        }

        private void ToggleButtonBold_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormat(sender, TextElement.FontWeightProperty, FormatingMode.bold, FontWeights.Bold, FontWeights.Normal);
        }

        private void ToggleButtonItalic_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormat(sender, TextElement.FontStyleProperty, FormatingMode.italic, FontStyles.Italic, FontStyles.Normal);
        }

        private void ToggleButtonUnderline_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormat(sender, Inline.TextDecorationsProperty, FormatingMode.underline, TextDecorations.Underline, null);
        }
    }
}