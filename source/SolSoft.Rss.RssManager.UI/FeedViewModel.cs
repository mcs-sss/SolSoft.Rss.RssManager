using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Feeds.Interop;

namespace SolSoft.Rss.RssManager
{
	/// <summary>
	/// Wraps an RSS Feed Folder
	/// </summary>
	public class FeedViewModel
	{
		private readonly IFeed m_feed;
		public FeedViewModel(IFeed feed)
		{
			m_feed = feed;
		}

		/// <summary>
		/// Gets the name of this feed
		/// </summary>
		public string Name
		{
			get
			{
				return m_feed.Name;
			}
		}

		/// <summary>
		/// Gets or sets the URL of this feed
		/// </summary>
		public string Url
		{
			get
			{
				return m_feed.Url;
			}
			set
			{
				m_feed.Url = value;
			}
		}
	}
}
