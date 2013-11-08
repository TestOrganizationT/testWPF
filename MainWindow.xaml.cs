using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalebdarTest
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			currentDay = new DateTime();
			currentDay = DateTime.Today;

			currentMonthsNameIndex = currentDay.Month;
			showMonthCalendar(currentMonthsNameIndex);
			dayTaskCollection = new Dictionary<DateTime, DayTask>();

			foreach (Button b in CalendarPanel.Children)
			{ 
				b.Click+=new RoutedEventHandler(Button_Click);
			}
		}

		private void ChangeMonthLeft_Click(object sender, RoutedEventArgs e)
		{
			if (currentMonthsNameIndex > 1)
				currentMonthsNameIndex--;
			else
				currentMonthsNameIndex = 12;

			MonthNameLabel.Content = monthsName[currentMonthsNameIndex-1];
			showMonthCalendar(currentMonthsNameIndex);
		}

		private void ChangeMonthRight_Click(object sender, RoutedEventArgs e)
		{
			if (currentMonthsNameIndex < 12)
				currentMonthsNameIndex++;
			else
				currentMonthsNameIndex = 1;

			MonthNameLabel.Content = monthsName[currentMonthsNameIndex-1];
			showMonthCalendar(currentMonthsNameIndex);
		}

		private void showMonthCalendar(int monthIndex)
		{
			startDayOfMonth = new DateTime(2013, monthIndex, 1); 
			startDayNumberOfMonth = (int)startDayOfMonth.DayOfWeek;

			if (startDayNumberOfMonth == 0)
				startDayNumberOfMonth = 7;

			MonthNameLabel.Content = monthsName[monthIndex - 1];

			clearCalendarPanel();

			int j = 0;
			for (int i = startDayNumberOfMonth-1,k=1; i <= DateTime.DaysInMonth(2013, monthIndex)+startDayNumberOfMonth-2; i++,k++)
			{
				Button btn = (Button)CalendarPanel.Children[i];
				btn.Content = k.ToString();
				j = i+1;
			}

			for (int i = 1; j < CalendarPanel.Children.Count; j++)
			{
				Button btn = (Button)CalendarPanel.Children[j];
				btn.Style = (Style)btn.TryFindResource("DayViewBtnNotActive");
				btn.Content = i++;
			}
		}

		private void clearCalendarPanel()
		{
			for (int i = 0; i < CalendarPanel.Children.Count; i++)
			{
				Button btn = (Button)CalendarPanel.Children[i];
				btn.Content = "";
				btn.Style = (Style)btn.TryFindResource("DayViewBtn");
			}

			for (int i = 6; i < CalendarPanel.Children.Count; i+=7)
			{
				Button btn = (Button)CalendarPanel.Children[i];
				btn.Style = (Style)btn.TryFindResource("RestDayViewBtn");
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string buttonContent = ((Button)e.Source).Content.ToString();
			textBoxDayTask.Text = "";
			if (buttonContent != "")
			{
				int day;
				int.TryParse(((Button)e.Source).Content.ToString(), out day);
				selectedDate = new DateTime(2013, currentMonthsNameIndex, day);
			}

			textBoxDayTask.Focus();
		}

		private readonly string[] monthsName = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		private int currentMonthsNameIndex;
		private DateTime currentDay;
		private DateTime selectedDate;

		private DateTime startDayOfMonth;
		private int startDayNumberOfMonth;
		private Dictionary<DateTime, DayTask> dayTaskCollection;

		private void addTast_Click(object sender, RoutedEventArgs e)
		{
			string taskDefinition = textBoxDayTask.Text;
			if (taskDefinition == "")
				return;
			else
			{
				DayTask task = new DayTask(selectedDate, taskDefinition);
				if (!dayTaskCollection.ContainsKey(selectedDate))
				{
					dayTaskCollection.Add(selectedDate, task);
					
				}

				else
				{
					DayTask updatedDayTask;
					dayTaskCollection.TryGetValue(selectedDate, out updatedDayTask);
					updatedDayTask.Task = taskDefinition;
				}
			}

			
		}

		private void clearTextBox_Click(object sender, RoutedEventArgs e)
		{
			textBoxDayTask.Text = "";
		}
		
		private void testMethodForGitHub()
		{
		        MessageBox.Show("Config this method, please);
		        
		}
	}
}
