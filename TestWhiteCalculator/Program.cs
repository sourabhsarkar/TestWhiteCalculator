using System;
using System.Diagnostics;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestWhiteCalculator
{
    class Program
    {

        private static string ExeSourceFile = Environment.CurrentDirectory + "\\calc.exe";
        private static Application _application;
        private static Window _mainWindow;

        static void Main(string[] args)
        {
            var psi = new ProcessStartInfo(ExeSourceFile);
            _application = Application.AttachOrLaunch(psi);

            testMultiplication();
        }

        private static void testMultiplication()
        {
            _mainWindow = _application.GetWindow(SearchCriteria.ByText("Calculator"), InitializeOption.NoCache);

            var _pane = _mainWindow.Get<TestStack.White.UIItems.Panel>(SearchCriteria.ByClassName("CalcFrame"));
            TestStack.White.UIItems.Button _digitButton;
            var _resultText = _pane.Get(SearchCriteria.ByAutomationId("150"));

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _digitButton = _pane.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("13" + i));
                    _digitButton.Click();

                    _digitButton = _pane.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("92"));
                    _digitButton.Click();

                    _digitButton = _pane.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("13" + j));
                    _digitButton.Click();

                    _digitButton = _pane.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("121"));
                    _digitButton.Click();

                    if((_resultText.Name).Equals((i*j).ToString()))
                        Console.WriteLine($"{i} * {j} = Expected: {i*j} Calculated: {_resultText.Name} -> Passed");
                    else
                        Console.WriteLine($"{i} * {j} = Expected: {i*j} Calculated: {_resultText.Name} -> Failed");
                }
            }

            Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.ALT);
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.F4);
            Keyboard.Instance.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.ALT);

            Console.ReadLine();
        }
    }
}
