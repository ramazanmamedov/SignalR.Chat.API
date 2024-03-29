﻿using System;
using System.Collections.Generic;
using System.Linq;
using SignalR.Chat.API.Entities.Core;

namespace SignalR.Chat.API.Web.Infrastructure.Helpers
{
    /// <summary>
    /// Email validation helper
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Validates emails entries
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetValidEmails(string emails)
        {
            if (string.IsNullOrWhiteSpace(emails))
            {
                return null;
            }

            var split = emails.Split(new[] {';', '|', ' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            return split.Where(x => x.IsEmail());
        }
    }
}