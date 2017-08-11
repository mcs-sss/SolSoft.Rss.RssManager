using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Feeds.Interop;
using System.Collections;
using System.ComponentModel;
using SolSoft.DataBinding;

namespace SolSoft.Rss.RssManager
{
	public class MainViewModel : IRaisePropertyChanged
	{
		public MainViewModel()
		{
			this.m_currentItemProperty = new NotifyProperty<FeedViewModel>(this, nameof(CurrentItem));

			IFeedsManager feedsManager = new FeedsManager();
			IFeedFolder root = ((IFeedFolder)feedsManager.GetFolder(""));

			FeedFolderViewModel rootWrapper = new FeedFolderViewModel(root);

			this.RootItems = rootWrapper.Subitems;
		}

		/// <summary>
		/// Gets the root items
		/// </summary>
		public IEnumerable RootItems
		{
			get;
			private set;
		}

		NotifyProperty<FeedViewModel> m_currentItemProperty;
		/// <summary>
		/// Gets or sets the current item being edited
		/// </summary>
		public FeedViewModel CurrentItem
		{
			get
			{
				return m_currentItemProperty.Value;
			}
			set
			{
				m_currentItemProperty.SetValue(value);
			}
		}

		
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if(PropertyChanged != null)
				PropertyChanged(this, args);
		}
		void IRaisePropertyChanged.RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

	}
}
