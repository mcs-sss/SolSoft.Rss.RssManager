using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Feeds.Interop;
using System.Collections;

namespace SolSoft.Rss.RssManager
{
	/// <summary>
	/// Wraps an RSS Feed Folder
	/// </summary>
	public class FeedFolderViewModel
	{
		private readonly IFeedFolder m_folder;
		public FeedFolderViewModel(IFeedFolder folder)
		{
			m_folder = folder;
		}

		/// <summary>
		/// Gets the subfolders of this folder
		/// </summary>
		public IEnumerable<FeedFolderViewModel> Subfolders
		{
			get
			{
				var subfolders = ((IEnumerable)m_folder.Subfolders);

				return subfolders
					.Cast<IFeedFolder>()
					.Select(f => new FeedFolderViewModel(f));
			}
		}

		/// <summary>
		/// Gets the feeds in this folder
		/// </summary>
		public IEnumerable<FeedViewModel> Feeds
		{
			get
			{
				var subfolders = ((IEnumerable)m_folder.Feeds);

				return subfolders
					.Cast<IFeed>()
					.Select(f => new FeedViewModel(f));
			}
		}

		/// <summary>
		/// Gets all items (subfolders and feeds in this folder)
		/// </summary>
		public IEnumerable Subitems
		{
			get
			{
				return Subfolders.Cast<object>()
					.Concat(Feeds.Cast<object>());
			}
		}

		/// <summary>
		/// Gets the name of this folder
		/// </summary>
		public string Name
		{
			get
			{
				return m_folder.Name;
			}
		}
	}
}
