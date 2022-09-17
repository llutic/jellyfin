#nullable disable

#pragma warning disable CS1591

using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Lyrics;

namespace MediaBrowser.Providers.Lyric
{
    public class LyricManager : ILyricManager
    {
        private readonly ILyricProvider[] _lyricProviders;

        public LyricManager(IEnumerable<ILyricProvider> lyricProviders)
        {
            _lyricProviders = lyricProviders.ToArray();
        }

        /// <inheritdoc />
        public LyricResponse GetLyrics(BaseItem item)
        {
            foreach (ILyricProvider provider in _lyricProviders)
            {
                var results = provider.GetLyrics(item);
                if (results is not null)
                {
                    return results;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public bool HasLyricFile(BaseItem item)
        {
            foreach (ILyricProvider provider in _lyricProviders)
            {
                if (item is null)
                {
                    continue;
                }

                if (LyricInfo.GetLyricFilePath(provider, item.Path) is not null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
