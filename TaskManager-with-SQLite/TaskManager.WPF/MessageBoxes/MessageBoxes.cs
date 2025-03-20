using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.WPF.MessageBoxes
{
    public static class MessageBoxes
    {
        public static void ShowInvalidInput(string message)
        {
            MessageBox.Show(
                message,
                "Invalid Input",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void ShowSuccess(string message)
        {
            MessageBox.Show(
                message,
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public static void ShowGreeting(string message, string title)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.None);
        }
    }
}

