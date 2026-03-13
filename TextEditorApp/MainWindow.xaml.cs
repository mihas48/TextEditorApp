using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace TextEditorApp
{
    public partial class MainWindow : Window
    {
        private List<ToggleButton> alignmentButtons;
        public MainWindow()
        {
            InitializeComponent();
            alignmentButtons = new List<ToggleButton> {
            togBtnStart,
            togBtnCenter,
            togBtnEnd,
            togBtnJustify
            };
        }

        enum FormatingMode
        {
            bold, italic, underline, subscript, superscript
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

        private void ToggleVariant(object buttonSender, object activeValue, object normalValue)
        {
            if (!rtb.Selection.IsEmpty)
            {
                rtb.Selection.ApplyPropertyValue(Typography.VariantsProperty, ((ToggleButton)buttonSender).IsChecked == true ? activeValue : normalValue);
            }
            else
            {
                ((ToggleButton)buttonSender).IsChecked = !((ToggleButton)buttonSender).IsChecked;
                MessageBox.Show("Сначала выделете текст", "Сообщение", MessageBoxButton.OK);
            }
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

        private void ToggleButtonSubscript_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)togBtnSup.IsChecked)
                togBtnSub.IsChecked = false;
            ToggleVariant(sender, FontVariants.Subscript, FontVariants.Normal);
        }

        private void ToggleButtonSuperscript_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)togBtnSub.IsChecked)
                togBtnSub.IsChecked = false;
            ToggleVariant(sender, FontVariants.Superscript, FontVariants.Normal);
        }

        private void ToggleButtonAlignmentStart_Click(object sender, RoutedEventArgs e)
        {
            rtb.CaretPosition.Paragraph?.TextAlignment = TextAlignment.Left;
            UnCheckAllAligmentFlags();
            togBtnStart.IsChecked = true;
        }

        private void ToggleButtonAlignmentCenter_Click(object sender, RoutedEventArgs e)
        {
            rtb.CaretPosition.Paragraph?.TextAlignment = TextAlignment.Center;
            UnCheckAllAligmentFlags();
            togBtnCenter.IsChecked = true;
        }

        private void ToggleButtonAlignmentEnd_Click(object sender, RoutedEventArgs e)
        {
            rtb.CaretPosition.Paragraph?.TextAlignment = TextAlignment.Right;
            UnCheckAllAligmentFlags();
            togBtnEnd.IsChecked = true;
        }

        private void ToggleButtonAlignmentJustify_Click(object sender, RoutedEventArgs e)
        {
            rtb.CaretPosition.Paragraph?.TextAlignment = TextAlignment.Justify;
            UnCheckAllAligmentFlags();
            togBtnJustify.IsChecked = true;
        }

        private void UnCheckAllAligmentFlags()
        {
            foreach (var button in alignmentButtons)
                button.IsChecked = false;
        }
        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UnCheckAllAligmentFlags();
            switch (rtb.CaretPosition.Paragraph?.TextAlignment)
            {
                case TextAlignment.Left:
                    togBtnStart.IsChecked = true;
                    break;
                case TextAlignment.Right:
                    togBtnEnd.IsChecked = true;
                    break;
                case TextAlignment.Center:
                    togBtnCenter.IsChecked = true;
                    break;
                case TextAlignment.Justify:
                    togBtnJustify.IsChecked = true;
                    break;
                default:
                    break;
            }
        }
    }
}