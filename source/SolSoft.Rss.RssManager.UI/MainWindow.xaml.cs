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
using Microsoft.Feeds.Interop;
using System.Collections;
using SolSoft.Common.Extensions;

namespace SolSoft.Rss.RssManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		protected const string FeedDetailsBindingGroup = "FeedBindingGroup";
		public MainWindow()
		{
			InitializeComponent();

			BindingGroup = new System.Windows.Data.BindingGroup()
			{
				Name = FeedDetailsBindingGroup
			};

			//these bindings can be set in code and are thus more resilient to refactoring
			treeViewFeeds.SetBinding(TreeView.ItemsSourceProperty, new Binding(nameof(MainViewModel.RootItems)));

			textBoxName.SetBinding(TextBox.TextProperty, new Binding(nameof(FeedViewModel.Name))
			{
				Mode = BindingMode.OneWay,
				BindingGroupName = FeedDetailsBindingGroup
			});
			textBoxUrl.SetBinding(TextBox.TextProperty, new Binding(nameof(FeedViewModel.Url))
			{
				UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
				BindingGroupName = FeedDetailsBindingGroup
			});

			IsEditing = false;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = new MainViewModel();
		}

		protected MainViewModel ViewModel
		{
			get
			{
				return (MainViewModel)DataContext;
			}
		}

		private void buttonCommit_Click(object sender, RoutedEventArgs e)
		{
			BindingGroup.CommitEdit();
			IsEditing = false;
		}

		private void buttonRevert_Click(object sender, RoutedEventArgs e)
		{
			BindingGroup.CancelEdit();
			IsEditing = false;
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BindingGroup.BeginEdit();
			IsEditing = false;
		}
		private void treeViewFeeds_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			this.ViewModel.CurrentItem = e.NewValue as FeedViewModel;

			BindingGroup.BeginEdit();
			IsEditing = false;
		}

		private void textBoxUrl_TextChanged(object sender, TextChangedEventArgs e)
		{
			IsEditing = true;
		}

		protected bool IsEditing
		{
			get
			{
				return !treeViewFeeds.IsEnabled;
			}
			set
			{
				buttonCommit.IsEnabled = buttonRevert.IsEnabled = value;
				treeViewFeeds.IsEnabled = !value;
			}
		}

		
	}
}
