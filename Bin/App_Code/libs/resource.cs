using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace libs
{
    public class resource
    {
		public static DataSet[] ds = new DataSet[4];

		public static DateTime[] updateTime = new DateTime[4];

		public static bool update = false;

		public static void UpdateGameResource(int resource_server_no)
		{
			update = true;
			SqlConnection selectConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[$"resource{resource_server_no}"].ConnectionString);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("dbo.gmtool_v2_get_all_resource", selectConnection);
			if (ds[resource_server_no] == null)
			{
				ds[resource_server_no] = new DataSet();
			}
			else
			{
				ds[resource_server_no].Dispose();
				ds[resource_server_no] = new DataSet();
			}
			sqlDataAdapter.Fill(ds[resource_server_no]);
			DataRow dataRow = ds[resource_server_no].Tables[0].Rows[0];
			int num = dataRow.ItemArray.Length;
			ds[resource_server_no].Tables[0].TableName = "ResourceTableName";
			for (int i = 0; i < num; i++)
			{
				ds[resource_server_no].Tables[i + 1].TableName = ds[resource_server_no].Tables[0].Rows[0][i].ToString();
			}
			ref DateTime reference = ref updateTime[resource_server_no];
			reference = DateTime.Now;
			update = false;
		}

		public static void ResetResourceData()
		{
			for (int i = 0; i < ds.Length; i++)
			{
				ds[i].Clear();
			}
		}

		private static DateTime GetUpdateTime(int resource_server_no)
		{
			return updateTime[resource_server_no];
		}

		public static DataTable GetResourceTable(int resource_server_no, string tableName)
		{
			if (ds[resource_server_no] == null)
			{
				ds[resource_server_no] = new DataSet();
			}
			if (ds[resource_server_no].Tables.Count == 0)
			{
				UpdateGameResource(resource_server_no);
			}
			DataRow dataRow = ds[resource_server_no].Tables["ResourceTableName"].Rows[0];
			int num = dataRow.ItemArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (ds[resource_server_no].Tables["ResourceTableName"].Rows[0][i].ToString() == tableName)
				{
					return ds[resource_server_no].Tables[tableName];
				}
			}
			return null;
		}

		public static string GetStringResource(int resource_server_no, int code)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "StringResource").Select($"code = {code}");
			if (array.Length > 0)
			{
				return array[0]["value"].ToString();
			}
			return "";
		}

		public static DataRow GetQuestResource(int resource_server_no, int id)
		{
			try
			{
				DataRow[] array = GetResourceTable(resource_server_no, "QuestResource").Select($"id = {id}");
				if (array.Length > 0)
				{
					return array[0];
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		public static DataRow GetQuestLinkResource(int resource_server_no, int id)
		{
			try
			{
				DataRow[] array = GetResourceTable(resource_server_no, "QuestLinkResource").Select($"quest_id = {id}");
				if (array.Length > 0)
				{
					return array[0];
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		public static DataRow GetItemResource(int resource_server_no, int id)
		{
			try
			{
				DataRow[] array = GetResourceTable(resource_server_no, "ItemResource").Select($"id = {id}");
				if (array.Length > 0)
				{
					return array[0];
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		public static string GetItemName(int resource_server_no, int id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "ItemResource").Select($"id = {id}");
			if (array.Length > 0)
			{
				if (array[0]["value"] != null)
				{
					return array[0]["value"].ToString();
				}
				return $"[{id}]";
			}
			return $"[{id}]";
		}

		public static DataRow GetJobResource(int resource_server_no, int id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "JobResource").Select($"id = {id}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static void GetJobResource(int resource_server_no, int job_code, out string job_name, out string job_icon)
		{
			DataRow jobResource = GetJobResource(resource_server_no, job_code);
			if (jobResource != null)
			{
				job_name = (string)jobResource["value"];
				job_icon = (string)jobResource["icon_file_name"] + ".jpg";
			}
			else
			{
				job_name = "";
				job_icon = "";
			}
		}

		public static DataRow GetLevelResource(int resource_server_no, int level)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "LevelResource").Select($"level = {level}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static DataRow GetSkillResource(int resource_server_no, int id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "SkillResource").Select($"id = {id}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static DataRow GetSkillJPResource(int resource_server_no, int skill_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "SkillJPResource").Select($"skill_id = {skill_id}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static DataRow GetEnhanceResource(int resource_server_no, int enhance_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "EnhanceResource").Select($"enhance_id = {enhance_id}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static DataRow GetSummonResource(int resource_server_no, int id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "SummonResource").Select($"id = {id}");
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public static string GetSummonName(int resource_server_no, int summon_id)
		{
			string text = summon_id.ToString();
			int num = text.Length - 4;
			if (num < 0)
			{
				num = 0;
			}
			DataRow[] array = GetResourceTable(resource_server_no, "SummonResource").Select($"id = {text.Substring(num, text.Length - num)}");
			if (array.Length > 0)
			{
				return GetStringResource(resource_server_no, (int)array[0]["name_id"]);
			}
			return GetMonsterName(resource_server_no, summon_id);
		}

		public static string GetMonsterName(int resource_server_no, int monster_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "MonsterResource").Select($"id = {monster_id}");
			if (array.Length > 0)
			{
				return string.Format("{0} Lv {1}", GetStringResource(resource_server_no, (int)array[0]["name_id"]), (int)array[0]["level"]);
			}
			return $"({monster_id})";
		}

		public static string GetNpcName(int resource_server_no, int npc_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "NPCResource").Select($"id = {npc_id}");
			if (array.Length > 0)
			{
				return string.Format("{0} {1}", GetStringResource(resource_server_no, (int)array[0]["text_id"]), GetStringResource(resource_server_no, (int)array[0]["name_text_id"]));
			}
			return $"({npc_id})";
		}

		public static string GetTitleResource(int resource_server_no, int name_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "TitleResource").Select($"id = {name_id}");
			if (array.Length > 0)
			{
				return GetStringResource(resource_server_no, (int)array[0]["name_id"]);
			}
			return $"({name_id})";
		}

		public static string GetRaceName(int race)
		{
			return race switch
			{
				0 => "None",
				1 => "Monster",
				2 => "NPC",
				3 => GetStringResource(0, 1),
				4 => GetStringResource(0, 2),
				5 => GetStringResource(0, 3),
				6 => GetStringResource(0, 4),
				7 => GetStringResource(0, 5),
				8 => GetStringResource(0, 6),
				_ => "Unknown",
			};
		}

		public static string GetItemGCode(int gCode)
		{
			return gCode switch
			{
				0 => "Monster",
				1 => "Shop",
				2 => "Quest",
				3 => "Script",
				4 => "Mix",
				5 => "GM",
				6 => "Basic",
				7 => "Trade",
				30 => "<strong>Cash</strong>",
				_ => "Unknown",
			};
		}

		public static string GetItemType(int itemType)
		{
			return itemType switch
			{
				0 => "Etc",
				1 => "Equipment",
				2 => "Card",
				3 => "Consumption",
				4 => "Cube",
				5 => "Charm",
				6 => "Use",
				7 => "Soul Stone",
				_ => "Unknown",
			};
		}

		public static string GetItemRank(int number)
		{
			return number.ToString();
		}

		public static string GetItemGroup(int itemGroup)
		{
			return itemGroup switch
			{
				0 => "Others",
				1 => "Weapon",
				2 => "Costume",
				3 => "Shield",
				4 => "Helmet",
				5 => "Gloves",
				6 => "Boots",
				7 => "Belt",
				8 => "Mantle",
				9 => "Accessory",
				10 => "Skill Card",
				11 => "Unit Card",
				12 => "Spell Card",
				13 => "Creature Card",
				14 => "Hair",
				15 => "Face",
				16 => "Underwear",
				21 => "Strike Cube",
				22 => "Defense Cube",
				23 => "Skill Cube",
				80 => "Cash Item",
				94 => "Item Package",
				95 => "Generic Stone",
				96 => "Skill Material",
				97 => "Combine Material",
				98 => "Bullet",
				99 => "Consumption",
				110 => "Costume",
				120 => "Riding",
				_ => $"Unknown({itemGroup})",
			};
		}

		public static string GetItemWearType(int wearType)
		{
			return wearType switch
			{
				-1 => "X",
				0 => GetStringResource(0, 7198),
				1 => GetStringResource(0, 7199),
				99 => GetStringResource(0, 7198) + " + " + GetStringResource(0, 7199),
				2 => GetStringResource(0, 7194),
				3 => GetStringResource(0, 7192),
				4 => GetStringResource(0, 7196),
				5 => GetStringResource(0, 7197),
				6 => GetStringResource(0, 7200),
				7 => GetStringResource(0, 7195),
				8 => GetStringResource(0, 7201),
				9 => GetStringResource(0, 7203),
				94 => GetStringResource(0, 7203) + "[* 2]",
				11 => GetStringResource(0, 7202),
				12 => GetStringResource(0, 7193),
				13 => GetStringResource(0, 71),
				90 => GetStringResource(0, 71),
				91 => GetStringResource(0, 71),
				92 => GetStringResource(0, 71),
				93 => GetStringResource(0, 71),
				100 => GetStringResource(0, 71),
				14 => $"({GetStringResource(0, 7198)}) {GetStringResource(0, 7198)}",
				15 => $"({GetStringResource(0, 7199)}) {GetStringResource(0, 7199)}",
				16 => $"({GetStringResource(0, 7194)}) {GetStringResource(0, 7194)}",
				17 => $"({GetStringResource(0, 7192)}) {GetStringResource(0, 7192)}",
				18 => $"({GetStringResource(0, 7196)}) {GetStringResource(0, 7196)}",
				19 => $"({GetStringResource(0, 7197)}) {GetStringResource(0, 7197)}",
				20 => $"({GetStringResource(0, 7195)}) {GetStringResource(0, 7195)}",
				21 => $"({GetStringResource(0, 7210)}) {GetStringResource(0, 7210)}",
				22 => "Riding",
				_ => $"Unknown({wearType})",
			};
		}

		public static string GetItemClass(int itemClass)
		{
			return itemClass switch
			{
				0 => "Etc",
				100 => "Etc Weapon",
				101 => GetStringResource(0, 7173),
				102 => GetStringResource(0, 7174),
				103 => GetStringResource(0, 7175),
				104 => GetStringResource(0, 7176),
				105 => GetStringResource(0, 7177),
				106 => GetStringResource(0, 7178),
				107 => GetStringResource(0, 7179),
				108 => GetStringResource(0, 7180),
				109 => GetStringResource(0, 7181),
				110 => GetStringResource(0, 7182),
				111 => GetStringResource(0, 7183),
				112 => GetStringResource(0, 7184),
				113 => GetStringResource(0, 6812),
				200 => GetStringResource(0, 7194),
				201 => "[Fighter]" + GetStringResource(0, 7194),
				202 => "[Hunter]" + GetStringResource(0, 7194),
				203 => "[Magician] " + GetStringResource(0, 7194),
				204 => "[Summoner]" + GetStringResource(0, 7194),
				210 => GetStringResource(0, 7199),
				220 => GetStringResource(0, 7192),
				221 => "[Fighter]" + GetStringResource(0, 7192),
				222 => "[Hunter]" + GetStringResource(0, 7192),
				223 => "[Magician] " + GetStringResource(0, 7192),
				224 => "[Summoner]" + GetStringResource(0, 7192),
				230 => GetStringResource(0, 7197),
				240 => GetStringResource(0, 7196),
				250 => GetStringResource(0, 7200),
				260 => GetStringResource(0, 7195),
				300 => GetStringResource(0, 7197),
				301 => GetStringResource(0, 7203),
				302 => GetStringResource(0, 7202),
				303 => GetStringResource(0, 7201),
				304 => GetStringResource(0, 7193),
				305 => GetStringResource(0, 7193),
				400 => "Boost Chip",
				500 => "Use Type Cash Item",
				501 => "Equip Type Cash Item",
				600 => $"({GetStringResource(0, 7198)}) {GetStringResource(0, 7198)}",
				601 => $"({GetStringResource(0, 7199)}) {GetStringResource(0, 7199)}",
				602 => $"({GetStringResource(0, 7194)}) {GetStringResource(0, 7194)}",
				603 => $"({GetStringResource(0, 7192)}) {GetStringResource(0, 7192)}",
				604 => $"({GetStringResource(0, 7196)}) {GetStringResource(0, 7196)}",
				605 => $"({GetStringResource(0, 7197)}) {GetStringResource(0, 7197)}",
				606 => $"({GetStringResource(0, 7195)}) {GetStringResource(0, 7195)}",
				607 => $"({GetStringResource(0, 7210)}) {GetStringResource(0, 7210)}",
				608 => $"({GetStringResource(0, 7205)}) {GetStringResource(0, 7205)}",
				_ => $"Unknown({itemClass})",
			};
		}

		public static string GetItemTimeType(int itemTimeType)
		{
			string result = "";
			switch (itemTimeType)
			{
				case 0:
					result = "无限";
					break;
				case 1:
					result = "登录";
					break;
				case 2:
					result = "登出";
					break;
			}
			return result;
		}

		public static string GetLimitRace(bool bDeva, bool bAsura, bool bGaia)
		{
			if (bDeva && bAsura && bGaia)
			{
				return "All";
			}
			if (bDeva && !bAsura && !bGaia)
			{
				return GetStringResource(0, 2);
			}
			if (!bDeva && bAsura && !bGaia)
			{
				return GetStringResource(0, 3);
			}
			if (!bDeva && !bAsura && bGaia)
			{
				return GetStringResource(0, 1);
			}
			if (bDeva && bAsura && !bGaia)
			{
				return $"{GetStringResource(0, 2)}, {GetStringResource(0, 3)}";
			}
			if (bDeva && !bAsura && bGaia)
			{
				return $"{GetStringResource(0, 2)}, {GetStringResource(0, 1)}";
			}
			if (!bDeva && bAsura && bGaia)
			{
				return $"{GetStringResource(0, 3)}, {GetStringResource(0, 1)}";
			}
			return "";
		}

		public static string GetLimitJob(bool bFighter, bool bHunter, bool bMagician, bool bSummoner)
		{
			if (bFighter && bHunter && bMagician && bSummoner)
			{
				return "All";
			}
			if (bFighter && !bHunter && !bMagician && !bSummoner)
			{
				return "Fighter";
			}
			if (!bFighter && bHunter && !bMagician && !bSummoner)
			{
				return "Hunter";
			}
			if (!bFighter && !bHunter && bMagician && !bSummoner)
			{
				return "Magician";
			}
			if (!bFighter && !bHunter && !bMagician && bSummoner)
			{
				return "Summoner";
			}
			string text = "?";
			if (bFighter)
			{
				text = "Fighter";
			}
			if (bFighter)
			{
				text = ((text != "") ? ", Hunter" : "Hunter");
			}
			if (bFighter)
			{
				text = ((text != "") ? ", Magician" : "Magician");
			}
			if (bFighter)
			{
				text = ((text != "") ? ", Summoner" : "Summoner");
			}
			return text;
		}

		public static string GetItemEffectBitSetA(decimal type)
		{
			string text = "";
			try
			{
				long num = decimal.ToInt64(type);
				long num2 = 1L;
				int num3 = 1;
				while (num2 <= num)
				{
					if (num3 < 33)
					{
						if ((num & num2) != 0)
						{
							string text2 = "";
							switch (num3)
							{
								case 1:
									text2 = GetStringResource(0, 7101);
									break;
								case 2:
									text2 = GetStringResource(0, 7102);
									break;
								case 3:
									text2 = GetStringResource(0, 7103);
									break;
								case 4:
									text2 = GetStringResource(0, 7104);
									break;
								case 5:
									text2 = GetStringResource(0, 7105);
									break;
								case 6:
									text2 = GetStringResource(0, 7106);
									break;
								case 7:
									text2 = GetStringResource(0, 7107);
									break;
								case 8:
									text2 = GetStringResource(0, 7108);
									break;
								case 9:
									text2 = GetStringResource(0, 7109);
									break;
								case 10:
									text2 = GetStringResource(0, 7110);
									break;
								case 11:
									text2 = GetStringResource(0, 7111);
									break;
								case 12:
									text2 = GetStringResource(0, 7112);
									break;
								case 13:
									text2 = GetStringResource(0, 7113);
									break;
								case 14:
									text2 = GetStringResource(0, 7114);
									break;
								case 15:
									text2 = GetStringResource(0, 7115);
									break;
								case 16:
									text2 = GetStringResource(0, 7116);
									break;
								case 17:
									text2 = GetStringResource(0, 7117);
									break;
								case 18:
									text2 = GetStringResource(0, 7118);
									break;
								case 19:
									text2 = GetStringResource(0, 7119);
									break;
								case 20:
									text2 = GetStringResource(0, 7120);
									break;
								case 21:
									text2 = GetStringResource(0, 7121);
									break;
								case 22:
									text2 = GetStringResource(0, 7122);
									break;
								case 23:
									text2 = GetStringResource(0, 7123);
									break;
								case 24:
									text2 = GetStringResource(0, 7124);
									break;
								case 25:
									text2 = GetStringResource(0, 7125);
									break;
								case 26:
									text2 = GetStringResource(0, 7126);
									break;
								case 27:
									text2 = GetStringResource(0, 7127);
									break;
								case 28:
									text2 = GetStringResource(0, 7128);
									break;
								case 29:
									text2 = GetStringResource(0, 7129);
									break;
								case 30:
									text2 = "";
									break;
								case 31:
									text2 = GetStringResource(0, 7130);
									break;
								case 32:
									text2 = "";
									break;
							}
							if (text != "" && text2 != "")
							{
								text += ", ";
							}
							if (text2 != "")
							{
								text += text2;
							}
						}
						num2 <<= 1;
						num3++;
						continue;
					}
					return text;
				}
				return text;
			}
			catch (Exception ex)
			{
				return text + ex.Message;
			}
		}

		public static string GetItemEffectBitSetB(decimal type)
		{
			string text = "";
			try
			{
				long num = decimal.ToInt64(type);
				long num2 = 1L;
				int num3 = 1;
				while (num2 <= num)
				{
					if (num3 < 33)
					{
						if ((num & num2) != 0)
						{
							string text2 = "";
							switch (num3)
							{
								case 1:
									text2 = GetStringResource(0, 7131);
									break;
								case 2:
									text2 = GetStringResource(0, 7132);
									break;
								case 3:
									text2 = GetStringResource(0, 7133);
									break;
								case 4:
									text2 = GetStringResource(0, 7134);
									break;
								case 5:
									text2 = GetStringResource(0, 7135);
									break;
								case 6:
									text2 = GetStringResource(0, 7136);
									break;
								case 7:
									text2 = GetStringResource(0, 7137);
									break;
								case 8:
									text2 = GetStringResource(0, 1);
									break;
								case 9:
									text2 = GetStringResource(0, 1);
									break;
								case 10:
									text2 = "";
									break;
								case 11:
									text2 = "";
									break;
								case 12:
									text2 = "";
									break;
								case 13:
									text2 = "";
									break;
								case 14:
									text2 = "";
									break;
								case 15:
									text2 = GetStringResource(0, 7138);
									break;
								case 16:
									text2 = GetStringResource(0, 7139);
									break;
								case 17:
									text2 = GetStringResource(0, 7140);
									break;
								case 18:
									text2 = GetStringResource(0, 7141);
									break;
								case 19:
									text2 = GetStringResource(0, 7142);
									break;
								case 20:
									text2 = GetStringResource(0, 7143);
									break;
								case 21:
									text2 = GetStringResource(0, 7144);
									break;
								case 22:
									text2 = GetStringResource(0, 7145);
									break;
								case 23:
									text2 = GetStringResource(0, 7146);
									break;
								case 24:
									text2 = GetStringResource(0, 7147);
									break;
								case 25:
									text2 = GetStringResource(0, 7148);
									break;
								case 26:
									text2 = GetStringResource(0, 7149);
									break;
								case 27:
									text2 = GetStringResource(0, 7150);
									break;
								case 28:
									text2 = GetStringResource(0, 7151);
									break;
								case 29:
									text2 = GetStringResource(0, 7152);
									break;
								case 30:
									text2 = "HP Regen Stop Flag";
									break;
								case 31:
									text2 = "MP Regen Stop Flag";
									break;
								case 32:
									text2 = "";
									break;
							}
							if (text != "" && text2 != "")
							{
								text += ", ";
							}
							if (text2 != "")
							{
								text += text2;
							}
						}
						num2 <<= 1;
						num3++;
						continue;
					}
					return text;
				}
				return text;
			}
			catch (Exception ex)
			{
				return text + ex.Message;
			}
		}

		public static decimal GetEnhance(int enhance, decimal effect1, decimal effect2, decimal effect3, decimal effect4)
		{
			decimal result = 0m;
			for (int i = 1; i <= enhance; i++)
			{
				if (i <= 4)
				{
					result += effect1;
				}
				else if (i <= 8)
				{
					result += effect2;
				}
				else if (i <= 12)
				{
					result += effect3;
				}
				else if (i <= 20)
				{
					result += effect4;
				}
			}
			return result;
		}

		public static string GetItemFlagValue(int flag)
		{
			string text = "";
			_ = 0;
			try
			{
				int num = 1;
				int num2 = 1;
				while (true)
				{
					if (flag < 0 || num <= flag)
					{
						if (num2 >= 33)
						{
							break;
						}
						if ((flag & num) != 0)
						{
							string text2 = "";
							switch (num2)
							{
								case 1:
									text2 = "Captured in Card";
									break;
								case 2:
									text2 = "Something in the slot";
									break;
								case 3:
									text2 = "Put into other item slot";
									break;
								case 4:
									text2 = "Enhancement Failure";
									break;
								case 5:
									text2 = "Event Item";
									break;
								case 32:
									text2 = "Creature is inside";
									break;
							}
							if (text != "" && text2 != "")
							{
								text += " <br /> ";
							}
							if (text2 != "")
							{
								text += $"{text2}";
							}
						}
						num <<= 1;
						num2++;
						continue;
					}
					return text;
				}
				return text;
			}
			catch (Exception ex)
			{
				return text + ex.Message;
			}
		}

		public static string GetItemName(int resource_server_no, int item_code, int item_level, int item_enhance, int user_level)
		{
			DataRow itemResource = GetItemResource(resource_server_no, item_code);
			if (itemResource == null)
			{
				return $"[code : {item_code}]";
			}
			string text = "";
			if ((int)itemResource["type"] != 1)
			{
				text = (((int)itemResource["group"] != 10 && (int)itemResource["group"] != 13 && (int)itemResource["group"] != 22) ? GetStringResource(resource_server_no, (int)itemResource["name_id"]) : string.Format("+{0} {1}", item_enhance, GetStringResource(resource_server_no, (int)itemResource["name_id"]).ToString()));
			}
			else
			{
				int num = (int)itemResource["rank"];
				string text2 = "";
				text2 = num.ToString();
				text = ((item_enhance != 0) ? string.Format("[{0}] +{3} {1} Lv {2}", text2, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level, item_enhance) : string.Format("[{0}] {1} Lv {2}", text2, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level));
			}
			if (item_code >= 900000 && item_code < 1000000)
			{
				return "[Cash] " + text + $"[Item Code : {item_code.ToString()}]";
			}
			return text + $"[Item Code : {item_code.ToString()}]";
		}

		public static string GetItemToolTip(int resource_server_no, int item_code, int item_level, int item_enhance, int user_level)
		{
			DataRow itemResource = GetItemResource(resource_server_no, item_code);
			if (itemResource == null)
			{
				return $"[code : {item_code}]";
			}
			StringBuilder stringBuilder = new StringBuilder(100);
			string str = "";
			stringBuilder.Append(GetStringResource(resource_server_no, (int)itemResource["tooltip_id"]));
			new Regex("<#(?<color>[0-9a-f]+)>");
			Regex regex = new Regex("<size:(?<size>[0-9]+)>");
			while (true)
			{
				Match match = regex.Match(stringBuilder.ToString());
				if (!match.Success)
				{
					break;
				}
				stringBuilder.Replace(string.Format("<size:{0}>", match.Groups["size"]), "");
			}
			int num = (int)itemResource["rank"];
			string text = "";
			text = num.ToString();
			if ((int)itemResource["type"] == 1)
			{
				if (item_enhance == 0)
				{
					str += string.Format("[{0}] {1} Lv {2}", text, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level);
					stringBuilder.Replace("#@itemname@#", string.Format("[{0}] {1} Lv {2}", text, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level));
				}
				else
				{
					str += string.Format("[{0}] +{3} {1} Lv {2}", text, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level, item_enhance);
					stringBuilder.Replace("#@itemname@#", string.Format("[{0}] +{3} {1} Lv {2}", text, GetStringResource(resource_server_no, (int)itemResource["name_id"]), item_level, item_enhance));
				}
			}
			else if ((int)itemResource["group"] == 10)
			{
				str += string.Format("+{0} {1}<br/>", item_enhance, GetStringResource(resource_server_no, (int)itemResource["name_id"]));
				stringBuilder.Replace("#@itemname@#", string.Format("+{0} {1}", item_enhance, GetStringResource(resource_server_no, (int)itemResource["name_id"])));
			}
			else
			{
				str += GetStringResource(resource_server_no, (int)itemResource["name_id"]);
				stringBuilder.Replace("#@itemname@#", GetStringResource(resource_server_no, (int)itemResource["name_id"]));
			}
			int num2 = 0;
			switch (num)
			{
				case 1:
					num2 = 0;
					break;
				case 2:
					num2 = 20;
					break;
				case 3:
					num2 = 50;
					break;
				case 4:
					num2 = 80;
					break;
				case 5:
					num2 = 100;
					break;
				case 6:
					num2 = 120;
					break;
			}
			str = str + "<br>limit_level : " + num2;
			stringBuilder.Replace("#@limit_level@#", num2.ToString());
			int num3 = 0;
			int num4 = 0;
			switch (num)
			{
				case 1:
					num3 = 0;
					break;
				case 2:
					num3 = 3;
					break;
				case 3:
					num3 = 3;
					break;
				case 4:
					num3 = 3;
					break;
				case 5:
					num3 = 2;
					break;
				case 6:
					num3 = 2;
					break;
			}
			num4 = num2 + (item_level - 1) * num3;
			str = str + "<BR>recommend_level :" + num4;
			stringBuilder.Replace("#@recommend_level@#", num4.ToString());
			decimal[] array = new decimal[3];
			decimal[] array2 = array;
			decimal[] array3 = new decimal[3];
			decimal[] array4 = array3;
			decimal[] array5 = new decimal[3];
			decimal[] array6 = array5;
			decimal[] array7 = new decimal[3];
			decimal[] array8 = array7;
			decimal[] array9 = new decimal[3];
			decimal[] array10 = array9;
			decimal[] array11 = new decimal[3];
			decimal[] array12 = array11;
			decimal[] array13 = new decimal[3];
			decimal[] array14 = array13;
			decimal[] array15 = new decimal[3];
			decimal[] array16 = array15;
			decimal[] array17 = new decimal[3];
			decimal[] array18 = array17;
			decimal[] array19 = new decimal[3];
			decimal[] array20 = array19;
			decimal[] array21 = new decimal[3];
			decimal[] array22 = array21;
			decimal[] array23 = new decimal[3];
			decimal[] array24 = array23;
			decimal[] array25 = new decimal[3];
			decimal[] array26 = array25;
			decimal[] array27 = new decimal[3];
			decimal[] array28 = array27;
			decimal[] array29 = new decimal[3];
			decimal[] array30 = array29;
			array = new decimal[3];
			decimal[] array31 = array;
			array = new decimal[3];
			decimal[] array32 = array;
			array = new decimal[3];
			decimal[] array33 = array;
			array = new decimal[3];
			decimal[] array34 = array;
			array = new decimal[3];
			decimal[] array35 = array;
			string text2 = "";
			for (int i = 0; i < 4; i++)
			{
				decimal num5 = (decimal)itemResource["base_var1_" + i];
				decimal num6 = (decimal)(item_level - 1) * (decimal)itemResource["base_var2_" + i];
				switch ((short)itemResource["base_type_" + i])
				{
					case 11:
						array2[0] += num5;
						array2[1] += num6;
						break;
					case 12:
						array4[0] += num5;
						array4[1] += num6;
						break;
					case 13:
						array6[0] += num5;
						array6[1] += num6;
						break;
					case 14:
						array8[0] += num5;
						array8[1] += num6;
						break;
					case 15:
						array10[0] += num5;
						array10[1] += num6;
						break;
					case 16:
						array12[0] += num5;
						array12[1] += num6;
						break;
					case 17:
						array14[0] += num5;
						array14[1] += num6;
						break;
					case 18:
						array16[0] += num5;
						array16[1] += num6;
						break;
					case 19:
						array18[0] += num5;
						array18[1] += num6;
						break;
					case 20:
						array20[0] += num5;
						array20[1] += num6;
						break;
					case 21:
						array22[0] += num5;
						array22[1] += num6;
						break;
					case 22:
						array24[0] += num5;
						array24[1] += num6;
						break;
					case 23:
						array26[0] += num5;
						array26[1] += num6;
						break;
					case 24:
						array28[0] += num5;
						array28[1] += num6;
						break;
					case 25:
						array30[0] += num5;
						array30[1] += num6;
						break;
					case 26:
						array31[0] += num5;
						array31[1] += num6;
						break;
					case 30:
						array32[0] += num5;
						array32[1] += num6;
						break;
					case 31:
						array33[0] += num5;
						array33[1] += num6;
						break;
					case 33:
						array34[0] += num5;
						array34[1] += num6;
						break;
					case 81:
						array35[0] += num5;
						array35[1] += num6;
						break;
				}
			}
			for (int j = 0; j < 4; j++)
			{
				decimal num7 = (decimal)itemResource["opt_var1_" + j];
				switch ((short)itemResource["opt_type_" + j])
				{
					case 11:
						array2[0] += num7;
						break;
					case 12:
						array4[0] += num7;
						break;
					case 13:
						array6[0] += num7;
						break;
					case 14:
						array8[0] += num7;
						break;
					case 15:
						array10[0] += num7;
						break;
					case 16:
						array12[0] += num7;
						break;
					case 17:
						array14[0] += num7;
						break;
					case 18:
						array16[0] += num7;
						break;
					case 19:
						array18[0] += num7;
						break;
					case 20:
						array20[0] += num7;
						break;
					case 21:
						array22[0] += num7;
						break;
					case 22:
						array24[0] += num7;
						break;
					case 23:
						array26[0] += num7;
						break;
					case 24:
						array28[0] += num7;
						break;
					case 25:
						array30[0] += num7;
						break;
					case 26:
						array31[0] += num7;
						break;
					case 30:
						array32[0] += num7;
						break;
					case 31:
						array33[0] += num7;
						break;
					case 33:
						array34[0] += num7;
						break;
					case 81:
						array35[0] += num7;
						break;
					case 96:
						text2 = ((text2 != "") ? (text2 + "<br />" + GetItemEffectBitSetA((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])) : GetItemEffectBitSetA((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j]));
						break;
					case 97:
						text2 = ((text2 != "") ? (text2 + "<br />" + GetItemEffectBitSetB((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])) : GetItemEffectBitSetB((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j]));
						break;
					case 98:
						text2 = ((text2 != "") ? (text2 + "<br />[%]" + GetItemEffectBitSetA((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])) : ("[%]" + GetItemEffectBitSetA((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])));
						break;
					case 99:
						text2 = ((text2 != "") ? (text2 + "<br />[%]" + GetItemEffectBitSetB((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])) : ("[%]" + GetItemEffectBitSetB((decimal)itemResource["opt_var1_" + j], (decimal)itemResource["opt_var2_" + j])));
						break;
				}
			}
			DataRow[] array36 = GetResourceTable(resource_server_no, "EnhanceEffectResource").Select(string.Format("enhance_id = {0}", itemResource["enhance_id"]));
			for (int k = 0; k < array36.Length; k++)
			{
				decimal num8 = 0m;
				for (int l = 1; l < 26 && l <= item_enhance; l++)
				{
					num8 += (decimal)array36[k]["value_" + l.ToString("D2")];
				}
				switch ((short)array36[k]["effect_id"])
				{
					case 11:
						array2[2] += num8;
						break;
					case 12:
						array4[2] += num8;
						break;
					case 13:
						array6[2] += num8;
						break;
					case 14:
						array8[2] += num8;
						break;
					case 15:
						array10[2] += num8;
						break;
					case 16:
						array12[2] += num8;
						break;
					case 17:
						array14[2] += num8;
						break;
					case 18:
						array16[2] += num8;
						break;
					case 19:
						array18[2] += num8;
						break;
					case 20:
						array20[2] += num8;
						break;
					case 21:
						array22[2] += num8;
						break;
					case 22:
						array24[2] += num8;
						break;
					case 23:
						array26[2] += num8;
						break;
					case 24:
						array28[2] += num8;
						break;
					case 25:
						array30[2] += num8;
						break;
					case 26:
						array31[2] += num8;
						break;
					case 30:
						array32[2] += num8;
						break;
					case 31:
						array33[2] += num8;
						break;
					case 33:
						array34[2] += num8;
						break;
					case 81:
						array35[2] += num8;
						break;
				}
			}
			decimal item_balance = 1m;
			if (user_level < num4)
			{
				if (user_level < num2)
				{
					item_balance = 0m;
				}
				else if (num4 > num2)
				{
					item_balance = (decimal)(1.0 - (double)(num4 - user_level) * 1.0 / (double)(num4 - num2) * (1.0 - (double)item_level * 0.03));
				}
			}
			stringBuilder.Replace("#@atk@#", GetItemOptionValue(array2[0], array2[1], array2[2], item_balance));
			stringBuilder.Replace("#@matk@#", GetItemOptionValue(array4[0], array4[1], array4[2], item_balance));
			stringBuilder.Replace("#@hit@#", GetItemOptionValue(array6[0], array6[1], array6[2], item_balance));
			stringBuilder.Replace("#@a_speed@#", GetItemOptionValue(array8[0], array8[1], array8[2], item_balance));
			stringBuilder.Replace("#@def@#", GetItemOptionValue(array10[0], array10[1], array10[2], item_balance));
			stringBuilder.Replace("#@mdef@#", GetItemOptionValue(array12[0], array12[1], array12[2], item_balance));
			stringBuilder.Replace("#@avoid@#", GetItemOptionValue(array14[0], array14[1], array14[2], item_balance));
			stringBuilder.Replace("#@mov_speed@#", GetItemOptionValue(array16[0], array16[1], array16[2], item_balance));
			stringBuilder.Replace("#@block chance@#", GetItemOptionValue(array18[0], array18[1], array18[2], item_balance));
			stringBuilder.Replace("#@maxWT@#", GetItemOptionValue(array20[0], array20[1], array20[2], item_balance));
			stringBuilder.Replace("#@block defence@#", GetItemOptionValue(array22[0], array22[1], array22[2], item_balance));
			stringBuilder.Replace("#@M_speed@#", GetItemOptionValue(array24[0], array24[1], array24[2], item_balance));
			stringBuilder.Replace("#@M_hit@#", GetItemOptionValue(array26[0], array26[1], array26[2], item_balance));
			stringBuilder.Replace("#@M_avoid@#", GetItemOptionValue(array28[0], array28[1], array28[2], item_balance));
			stringBuilder.Replace("#@Skill_recasting@#", GetItemOptionValue(array30[0], array30[1], array30[2], item_balance));
			stringBuilder.Replace("#@Belt_Slot@#", GetItemOptionValue(array31[0], array31[1], array31[2], item_balance));
			stringBuilder.Replace("#@MaxHP@#", GetItemOptionValue(array32[0], array32[1], array32[2], item_balance));
			stringBuilder.Replace("#@MaxMP@#", GetItemOptionValue(array33[0], array33[1], array33[2], item_balance));
			stringBuilder.Replace("#@MP_regen@#", GetItemOptionValue(array34[0], array34[1], array34[2], item_balance));
			stringBuilder.Replace("#@MaxStamina@#", GetItemOptionValue(array35[0], array35[1], array35[2], item_balance));
			stringBuilder.Replace("#@bitset_text@#", text2);
			stringBuilder.Replace("#@add_ability@#", "");
			str = str + "<br>P.Atk :" + GetItemOptionValue(array2[0], array2[1], array2[2], item_balance);
			str = str + "<br>M.Atk :" + GetItemOptionValue(array4[0], array4[1], array4[2], item_balance);
			str = str + "<br>hit :" + GetItemOptionValue(array6[0], array6[1], array6[2], item_balance);
			str = str + "<br>Atk.Spd :" + GetItemOptionValue(array8[0], array8[1], array8[2], item_balance);
			str = str + "<br>P.Def :" + GetItemOptionValue(array10[0], array10[1], array10[2], item_balance);
			str = str + "<br>M.Def :" + GetItemOptionValue(array12[0], array12[1], array12[2], item_balance);
			str = str + "<br> avoid:" + GetItemOptionValue(array14[0], array14[1], array14[2], item_balance);
			str = str + "<br>Mov Spd :" + GetItemOptionValue(array16[0], array16[1], array16[2], item_balance);
			str = str + "<br>block chance :" + GetItemOptionValue(array18[0], array18[1], array18[2], item_balance);
			str = str + "<br>maxWT :" + GetItemOptionValue(array20[0], array20[1], array20[2], item_balance);
			str = str + "<br>block defence :" + GetItemOptionValue(array22[0], array22[1], array22[2], item_balance);
			str = str + "<br>M_speed :" + GetItemOptionValue(array24[0], array24[1], array24[2], item_balance);
			str = str + "<br>M_hit :" + GetItemOptionValue(array26[0], array26[1], array26[2], item_balance);
			str = str + "<br>M_avoid :" + GetItemOptionValue(array28[0], array28[1], array28[2], item_balance);
			str = str + "<br>Skill_recasting :" + GetItemOptionValue(array30[0], array30[1], array30[2], item_balance);
			str = str + "<br>MaxHP :" + GetItemOptionValue(array32[0], array32[1], array32[2], item_balance);
			str = str + "<br>MaxMP :" + GetItemOptionValue(array33[0], array33[1], array33[2], item_balance);
			str = str + "<br>MP_regen :" + GetItemOptionValue(array34[0], array34[1], array34[2], item_balance);
			str = str + "<br>MaxStamina :" + GetItemOptionValue(array35[0], array35[1], array35[2], item_balance);
			str += text2;
			stringBuilder.Replace("@#auto_tooltip#@", str);
			string arg = (string)itemResource["icon_file_name"];
			return string.Format("<dl class='itemInfo'><dt><img src='http://" + HttpContext.Current.Request.Url.Host + "/img/icon/{0}.jpg' /></dt><dd>{1}</dd></dl>", arg, stringBuilder.ToString());
		}

		public static string GetItemOptionValue(decimal base_value, decimal level_value, decimal enhacne_value, decimal item_balance)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (item_balance == 1m)
			{
				stringBuilder.Append((base_value + level_value + enhacne_value).ToString("#0.##"));
				if (enhacne_value >= 1m)
				{
					stringBuilder.Append("<font color='#33CC00'>");
					stringBuilder.AppendFormat(" (+{0})", enhacne_value.ToString("#0.##"));
					stringBuilder.Append("</font>");
				}
			}
			else
			{
				if (level_value * item_balance + base_value + enhacne_value != base_value + level_value + enhacne_value)
				{
					stringBuilder.Append("<font color='red'>");
					stringBuilder.Append((level_value * item_balance + base_value + enhacne_value).ToString("#0.##"));
					stringBuilder.Append("</font>");
					stringBuilder.Append(" / ");
					stringBuilder.Append((base_value + level_value + enhacne_value).ToString("#0.##"));
				}
				else
				{
					stringBuilder.Append((base_value + level_value + enhacne_value).ToString("#0.##"));
				}
				if (enhacne_value >= 1m)
				{
					stringBuilder.Append("<font color='#33CC00'>");
					stringBuilder.AppendFormat(" (+{0})", enhacne_value.ToString("#0.##"));
					stringBuilder.Append("</font>");
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetItemEffectBitSetA(decimal type, decimal value)
		{
			string text = "";
			try
			{
				long num = decimal.ToInt64(type);
				long num2 = 1L;
				int num3 = 1;
				while (num2 <= num)
				{
					if (num3 < 33)
					{
						if ((num & num2) != 0)
						{
							string text2 = "";
							switch (num3)
							{
								case 1:
									text2 = GetStringResource(0, 7101);
									break;
								case 2:
									text2 = GetStringResource(0, 7102);
									break;
								case 3:
									text2 = GetStringResource(0, 7103);
									break;
								case 4:
									text2 = GetStringResource(0, 7104);
									break;
								case 5:
									text2 = GetStringResource(0, 7105);
									break;
								case 6:
									text2 = GetStringResource(0, 7106);
									break;
								case 7:
									text2 = GetStringResource(0, 7107);
									break;
								case 8:
									text2 = GetStringResource(0, 7108);
									break;
								case 9:
									text2 = GetStringResource(0, 7109);
									break;
								case 10:
									text2 = GetStringResource(0, 7110);
									break;
								case 11:
									text2 = GetStringResource(0, 7111);
									break;
								case 12:
									text2 = GetStringResource(0, 7112);
									break;
								case 13:
									text2 = GetStringResource(0, 7113);
									break;
								case 14:
									text2 = GetStringResource(0, 7114);
									break;
								case 15:
									text2 = GetStringResource(0, 7115);
									break;
								case 16:
									text2 = GetStringResource(0, 7116);
									break;
								case 17:
									text2 = GetStringResource(0, 7117);
									break;
								case 18:
									text2 = GetStringResource(0, 7118);
									break;
								case 19:
									text2 = GetStringResource(0, 7119);
									break;
								case 20:
									text2 = GetStringResource(0, 7120);
									break;
								case 21:
									text2 = GetStringResource(0, 7121);
									break;
								case 22:
									text2 = GetStringResource(0, 7122);
									break;
								case 23:
									text2 = GetStringResource(0, 7123);
									break;
								case 24:
									text2 = GetStringResource(0, 7124);
									break;
								case 25:
									text2 = GetStringResource(0, 7125);
									break;
								case 26:
									text2 = GetStringResource(0, 7126);
									break;
								case 27:
									text2 = GetStringResource(0, 7127);
									break;
								case 28:
									text2 = GetStringResource(0, 7128);
									break;
								case 29:
									text2 = GetStringResource(0, 7129);
									break;
								case 30:
									text2 = "";
									break;
								case 31:
									text2 = GetStringResource(0, 7130);
									break;
								case 32:
									text2 = "";
									break;
							}
							if (text != "" && text2 != "")
							{
								text += " / ";
							}
							if (text2 != "")
							{
								text += string.Format("{0} +{1}", text2, value.ToString("#0.##"));
							}
						}
						num2 <<= 1;
						num3++;
						continue;
					}
					return text;
				}
				return text;
			}
			catch (Exception ex)
			{
				return text + ex.Message;
			}
		}

		public static string GetItemEffectBitSetB(decimal type, decimal value)
		{
			string text = "";
			try
			{
				long num = decimal.ToInt64(type);
				long num2 = 1L;
				int num3 = 1;
				while (num2 <= num)
				{
					if (num3 < 33)
					{
						if ((num & num2) != 0)
						{
							string text2 = "";
							switch (num3)
							{
								case 1:
									text2 = GetStringResource(0, 7131);
									break;
								case 2:
									text2 = GetStringResource(0, 7132);
									break;
								case 3:
									text2 = GetStringResource(0, 7133);
									break;
								case 4:
									text2 = GetStringResource(0, 7134);
									break;
								case 5:
									text2 = GetStringResource(0, 7135);
									break;
								case 6:
									text2 = GetStringResource(0, 7136);
									break;
								case 7:
									text2 = GetStringResource(0, 7137);
									break;
								case 8:
									text2 = "";
									break;
								case 9:
									text2 = "";
									break;
								case 10:
									text2 = "";
									break;
								case 11:
									text2 = "";
									break;
								case 12:
									text2 = "";
									break;
								case 13:
									text2 = "";
									break;
								case 14:
									text2 = "";
									break;
								case 15:
									text2 = GetStringResource(0, 7138);
									break;
								case 16:
									text2 = GetStringResource(0, 7139);
									break;
								case 17:
									text2 = GetStringResource(0, 7140);
									break;
								case 18:
									text2 = GetStringResource(0, 7141);
									break;
								case 19:
									text2 = GetStringResource(0, 7142);
									break;
								case 20:
									text2 = GetStringResource(0, 7143);
									break;
								case 21:
									text2 = GetStringResource(0, 7144);
									break;
								case 22:
									text2 = GetStringResource(0, 7145);
									break;
								case 23:
									text2 = GetStringResource(0, 7146);
									break;
								case 24:
									text2 = GetStringResource(0, 7147);
									break;
								case 25:
									text2 = GetStringResource(0, 7148);
									break;
								case 26:
									text2 = GetStringResource(0, 7149);
									break;
								case 27:
									text2 = GetStringResource(0, 7150);
									break;
								case 28:
									text2 = GetStringResource(0, 7151);
									break;
								case 29:
									text2 = GetStringResource(0, 7152);
									break;
								case 30:
									text2 = "HP Regen Stop Flag";
									break;
								case 31:
									text2 = "MP Regen Stop Flag";
									break;
								case 32:
									text2 = "";
									break;
							}
							if (text != "" && text2 != "")
							{
								text += " / ";
							}
							if (text2 != "")
							{
								text += string.Format("{0} +{1}", text2, value.ToString("#0.##"));
							}
						}
						num2 <<= 1;
						num3++;
						continue;
					}
					return text;
				}
				return text;
			}
			catch (Exception ex)
			{
				return text + ex.Message;
			}
		}

		public static string GetMonsterDropTableResource(int resource_server_no, int drop_table_link_id)
		{
			DataRow[] array = GetResourceTable(resource_server_no, "MonsterDropTableResource").Select($" id  = {drop_table_link_id}");
			StringBuilder stringBuilder = new StringBuilder(1000);
			if (array.Length > 0)
			{
				stringBuilder.Append("<table>");
				for (int i = 0; i < 10; i++)
				{
					if (i % 5 == 0)
					{
						stringBuilder.Append("<tr valign='top'>");
					}
					stringBuilder.Append("<td valign='top'>");
					int num = (int)array[0][string.Format("drop_item_id_{0}", i.ToString("0#"))];
					decimal num2 = (decimal)array[0][string.Format("drop_percentage_{0}", i.ToString("0#"))];
					short num3 = (short)array[0][string.Format("drop_min_count_{0}", i.ToString("0#"))];
					short num4 = (short)array[0][string.Format("drop_max_count_{0}", i.ToString("0#"))];
					short num5 = (short)array[0][string.Format("drop_min_level_{0}", i.ToString("0#"))];
					short num6 = (short)array[0][string.Format("drop_max_level_{0}", i.ToString("0#"))];
					if (num > 0)
					{
						stringBuilder.AppendFormat("<b>Item {0}</b><br>", i);
						string text = ((num3 == num4) ? $"{num3}개" : $"{num3}~{num4}개");
						string text2 = ((num5 == num6) ? $"Lv {num3}" : $"Lv {num5}~{num6}");
						stringBuilder.AppendFormat("-{0} ({1}%) {2} : {3}", GetItemName(3, num), (num2 * 100m).ToString("##0.####"), text2, text);
					}
					else if (num < 0)
					{
						stringBuilder.Append(GetDropGroupItem(i, num, num2, num3, num4, num5, num6));
					}
					stringBuilder.Append("</td>");
					if (i % 5 == 4)
					{
						stringBuilder.Append("</tr>");
					}
				}
				stringBuilder.Append("</table>");
				return stringBuilder.ToString();
			}
			return drop_table_link_id.ToString();
		}

		public static string GetDropGroupItem(int no, int item_group, decimal per_group, short min, short max, short min_lv, short max_lv)
		{
			string text = string.Format("<b>Group{0}</b> - {1}%", no, (per_group * 100m).ToString("##0.####"));
			DataTable resourceTable = GetResourceTable(3, "DropGroupResource");
			DataRow[] array = resourceTable.Select($"id = {item_group}");
			if (array.Length > 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int num = (int)array[0][string.Format("drop_item_id_{0}", i.ToString("0#"))];
					decimal d = (decimal)array[0][string.Format("drop_percentage_{0}", i.ToString("0#"))];
					if (num > 0)
					{
						if (text != "")
						{
							text += "<br>";
						}
						string text2 = ((min == max) ? $"{min}" : $"{min}~{max}");
						string text3 = ((min_lv == max_lv) ? $"Lv {min}" : $"Lv {min_lv}~{max_lv}");
						text += string.Format("-{0} ({1}%) {2} : {3}", GetItemName(3, num), (d * 100m * per_group).ToString("##0.####"), text3, text2);
					}
					else
					{
						_ = 0;
					}
				}
			}
			return text;
		}

		public static DataRow GetStateResource(int resource_server_no, int id)
		{
			try
			{
				DataRow[] array = GetResourceTable(resource_server_no, "StateResource").Select($"state_id = {id}");
				if (array.Length > 0)
				{
					return array[0];
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		public static string GetStateName(int resource_server_no, int id)
		{
			try
			{
				DataRow[] array = GetResourceTable(resource_server_no, "StateResource").Select($"state_id = {id}");
				if (array.Length > 0)
				{
					return GetStringResource(resource_server_no, (int)array[0]["name_id"]);
				}
				return id.ToString();
			}
			catch (Exception)
			{
			}
			return id.ToString();
		}
	}
}
