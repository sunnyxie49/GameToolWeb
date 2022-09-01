namespace libs
{
    public class link
    {
		private static string accountInfoUrl = "AccountInfo.aspx";

		private static string characterInfoUrl = "character_info.aspx";

		private static string itemInfoUrl = "ItemInfo.aspx";

		public static string Account(int account_id, string account)
		{
			return $"<a href='../Account/{accountInfoUrl}?account_id=?{account_id}'>{account}</a>";
		}

		public static string Character(string server, int character_id, string character_name)
		{
			return $"<a href='../Game/{characterInfoUrl}?server={server}&character_id={character_id}'>{character_name}</a>";
		}

		public static string Character(string server, int character_id, string character_name, string path, bool SeeDelCharacter)
		{
			return string.Format("<a href='{4}?server={1}&character_id={2}&del_character={5}'>{3}</a>", characterInfoUrl, server, character_id, character_name, path, SeeDelCharacter);
		}

		public static string CharacterPopup(string server, int character_id, string character_name, string account)
		{
			return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Game/{0}?server={1}&character_id={2}','character_name_info_{4}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{3}</a>", characterInfoUrl, server, character_id, character_name, account.Replace("@", "_").Trim());
		}

		public static string CharacterPopup(string server, int character_id, string character_name, int account_id, string account)
		{
			return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Game/{0}?server={1}&account_id={2}&character_id={3}','character_name_info_{3}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{4}</a>", characterInfoUrl, server, account_id, character_id, character_name, account.Replace("@", "_").Trim());
		}

		public static string CharacterPopup(string server, int character_id, string character_name, int account_id, string account, string see_delete_character)
		{
			return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Game/{0}?server={1}&account_id={2}&character_id={3}&see_delete_character={6}','character_name_info_{5}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{4}</a>", characterInfoUrl, server, account_id, character_id, character_name, account.Replace("@", "_").Trim(), see_delete_character);
		}

		public static string QuestPopup(string server, int quest_code, string name)
		{
			return $"<a href=\"javascript:;\" onclick=\"window.open('../Game/QuestPopup.aspx?server={server}&quest_code={quest_code}','quest_info','left=50,top=50,width=560,height=400,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{name}</a>";
		}

		public static string PartyPopup(string server, int party_id, string party_name)
		{
			return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Game/party_info.aspx?server={0}&party_id={1}','party_info{1}','left=50,top=50,width=620,height=450,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{2}</a>", server, party_id, party_name);
		}

		public static string GuildPopup(string server, int guild_id, string guild_name)
		{
			return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Game/guild_info.aspx?server={0}&guild_id={1}','guild_info{1}','left=50,top=50,width=710,height=450,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=yes')\">{2}</a>", server, guild_id, guild_name);
		}

		public static string Item(string server, int item_code)
		{
			return string.Format("<a href='{0}?server={1}&item_code={2}'>{2}</a>", itemInfoUrl, server, item_code);
		}
	}
}
