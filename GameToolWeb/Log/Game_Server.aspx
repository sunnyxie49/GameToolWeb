<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Log_Game_Server, App_Web_game_server.aspx.564c629f" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">   

    function OpenEditItemPopup(server, item_uid) {
        var win = window.open("../Game/edit_item.aspx?server=" + server + "&sid=" + item_uid, "EditItemPopup", 'left=50,top=50,width=590,height=350,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }

    function OpenSummonPopup(server, item_uid) {
        var win = window.open("../game/summon_info.aspx?server=" + server + "&item_id=" + item_uid, "SumonPopup", 'left=50,top=50,width=530,height=450,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }

    $(document).ready(function () {
        $("#<%=txtStart.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });
        $("#<%=txtEnd.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });

        $(".cServerNm strong").text($("[jid='ddlServerNm'] option:selected")[0].text);
        $("[jid='ddlServerNm'] option").click(function () {
            $(".cServerNm strong").text($(this)[0].text);
        });        
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game Server Log</h2>
			</div>
			<table class="table table-bordered table-condensed">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>							
                            <asp:dropdownlist id="ddlServer" jid="ddlServerNm" runat="server"></asp:dropdownlist>
						</th>
						<td class="cServerNm">Now <strong></strong> Server Selected</td>
					</tr>
					<tr>
						<th>Search</th>
						<td>
                            <asp:DropDownList ID="ddlLogFile" runat="server"></asp:DropDownList>							
							<table class="logSearchTable">
								<tbody>
									<tr>
										<th>Date</th>
										<td colspan="3">
											<input type="text" id="txtStart" runat="server" /> ~ <input type="text" id="txtEnd" runat="server" />
										</td>
									</tr>
									<tr>
										<th>Item Code</th>
										<td><input type="text" id="txtItemCode" runat="server" /></td>
										<th>Item Sid</th>
										<td><input type="text" id="txtItemUID" runat="server" /></td>
									</tr>
									<tr>
										<th>Account</th>
										<td><input type="text" id="txtAccount" runat="server" /></td>
										<th>account_id</th>
										<td><input type="text" id="txtAccountNo" runat="server" /></td>
									</tr>
									<tr>
										<th>Character Name</th>
										<td><input type="text" id="txtAvatar" runat="server" /></td>
										<th> Characte Sid</th>
										<td><input type="text" id="txtAvatarNo" runat="server" />&nbsp;<asp:button id="Button1" runat="server" Text="search" onclick="Button1_Click" CssClass="btn btn-primary"></asp:button>&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="excel" CssClass="btn btn-primary" />&nbsp;<label class="checkbox inline"><input type="checkbox" id="cbUseIndex" runat="server" value="option1" checked="checked" /> use index</label></td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<tr>
						<th>Search Type</th>
						<td>
							<table class="table table-bordered">
								<colgroup>
									<col style="width:10%" />
									<col style="width:90%" />
								</colgroup>
								<tbody>
									<tr>
										<th>Item</th>
										<td>                                            
		                                    <asp:checkboxlist id="cblLogType1" runat="server" RepeatColumns="7" CssClass="logSearchTable table-condensed logCheckTable">
		                                        <asp:ListItem Value="2401">Item Acquisition</asp:ListItem>
				                                <asp:ListItem Value="2402">Item destruction</asp:ListItem>
				                                <asp:ListItem Value="2403">Item throw away</asp:ListItem>
				                                <asp:ListItem Value="2404">Store buying</asp:ListItem>
				                                <asp:ListItem Value="2405">Store sale</asp:ListItem>
				                                <asp:ListItem Value="2406">Item use</asp:ListItem>
				                                <asp:ListItem Value="2407">Giving by trade</asp:ListItem>
				                                <asp:ListItem Value="2408">Taking by trade</asp:ListItem>
				                                <asp:ListItem Value="2409">Item Combination</asp:ListItem>
				                                <asp:ListItem Value="2410">Combination result</asp:ListItem>
				                                <asp:ListItem Value="2411">Item enhancement</asp:ListItem>
				                                <asp:ListItem Value="2412">Lak Acquisition</asp:ListItem>
				                                <asp:ListItem Value="2413">Item Deleted</asp:ListItem>
				                                <asp:ListItem Value="2501">Stall sale</asp:ListItem>
				                                <asp:ListItem Value="2502">Stall buying</asp:ListItem>
				                                <asp:ListItem Value="2503">Bank transaction</asp:ListItem>
				                                <asp:ListItem Value="3001">ItempShop(Check)</asp:ListItem>
				                                <asp:ListItem Value="3002">ItempShop(Withdraw)</asp:ListItem>
				                                <asp:ListItem Value="3003">ItempShop(Temp)</asp:ListItem>
                                                <asp:ListItem Value="2414">SOCKET_INFO</asp:ListItem>
				                                <asp:ListItem Value="2415">SOULSTONE_CRAFT</asp:ListItem>
				                                <asp:ListItem Value="2416">SOULSTONE_REPAIR</asp:ListItem>
				                                <asp:ListItem Value="2417">Item Donate</asp:ListItem>
				                                <asp:ListItem Value="2428">Awaken</asp:ListItem>
					                            <asp:ListItem Value="2429">Awaken Deleted</asp:ListItem>
					                            <asp:ListItem Value="2430">Awaken by Script</asp:ListItem>
					                            <asp:ListItem Value="2431">Awaken Deleted by Script</asp:ListItem>
		                                    </asp:checkboxlist>
		                                </td>
									</tr>
									<tr>
										<th>Avatar</th>
										<td>
											<asp:checkboxlist id="cblLogType2" runat="server" RepeatColumns="9" CssClass="logSearchTable table-condensed logCheckTable">
				                                <asp:ListItem Value="2001">Create Avatar</asp:ListItem>
				                                <asp:ListItem Value="2002">Ask Avatar deletion</asp:ListItem>
				                                <asp:ListItem Value="2003">Avatar deletion cancel</asp:ListItem>
				                                <asp:ListItem Value="2004">Avatar deletion</asp:ListItem>
				                                <asp:ListItem Value="2010">Avatar name change</asp:ListItem>
				                                <asp:ListItem Value="2101">Game login</asp:ListItem>
				                                <asp:ListItem Value="2102">Game Logout</asp:ListItem>
				                                <asp:ListItem Value="2103">Avatar Info</asp:ListItem>
				                                <asp:ListItem Value="2301">Lv up</asp:ListItem>
				                                <asp:ListItem Value="2302">Learn Skill</asp:ListItem>
				                                <asp:ListItem Value="2303">Job Lv up</asp:ListItem>
				                                <asp:ListItem Value="2304">Change of professoion</asp:ListItem>
				                                <asp:ListItem Value="2305">PK</asp:ListItem>
				                                <asp:ListItem Value="2306">Death</asp:ListItem>
				                                <asp:ListItem Value="2308">PKMode</asp:ListItem>
				                                <asp:ListItem Value="2309">Resurrection</asp:ListItem>
				                                <asp:ListItem Value="2701">Warp</asp:ListItem>		        
		                                    </asp:checkboxlist>
										</td>
									</tr>
									<tr>
										<th>Guild/Dungeon</th>
										<td>
											<asp:checkboxlist id="cblLogType3" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable">
		                                        <asp:ListItem Value="2901">Create Guild </asp:ListItem>
				                                <asp:ListItem Value="2902">Join Guild</asp:ListItem>
				                                <asp:ListItem Value="2903">Breakup guild</asp:ListItem>
				                                <asp:ListItem Value="2904">Guild Deportation </asp:ListItem>
				                                <asp:ListItem Value="2905">Guild Withdrawal</asp:ListItem>
				                                <asp:ListItem Value="2906">Guild name change</asp:ListItem>
				                                <asp:ListItem Value="2907">Guild Master Change</asp:ListItem>
				                                <asp:ListItem Value="2911">GUILD_INVITE</asp:ListItem>
				                                <asp:ListItem Value="3100">REQUEST_DUNGEON_RAID</asp:ListItem>
				                                <asp:ListItem Value="3101">END_DUNGEON_RAID</asp:ListItem>
				                                <asp:ListItem Value="3102">DUNGEON_CHANGE_OWNER</asp:ListItem>
				                                <asp:ListItem Value="3103">END_DUNGEON_SIEGE</asp:ListItem>
				                                <asp:ListItem Value="3104">SET_DUNGEON_TAX_RATE</asp:ListItem>
				                                <asp:ListItem Value="3105">DRAW_DUNGEON_TAX</asp:ListItem>
				                                <asp:ListItem Value="3107">DROP_DUNGEON_OWNERSHIP</asp:ListItem>
		                                    </asp:checkboxlist>
										</td>
									</tr>
									<tr>
										<th>Auction</th>
										<td>
											<asp:checkboxlist id="cblLogType4" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable">
		                                        <asp:ListItem Value="3300">Bid start</asp:ListItem>
				                                <asp:ListItem Value="3301">Check registered item's amount when Bid starts</asp:ListItem>
				                                <asp:ListItem Value="3302">Bid</asp:ListItem>
				                                <asp:ListItem Value="3303">Buy now</asp:ListItem>
				                                <asp:ListItem Value="3304">Cancellation</asp:ListItem>
				                                <asp:ListItem Value="3305">Sold</asp:ListItem>
				                                <asp:ListItem Value="3306">Not sold</asp:ListItem>
				                                <asp:ListItem Value="3350">Keep item(s)</asp:ListItem>
				                                <asp:ListItem Value="3351">Carry kept item(s)</asp:ListItem>
				                                <asp:ListItem Value="3352">Duration of item keeping is end</asp:ListItem>				
		                                    </asp:checkboxlist>
										</td>
									</tr>
									<tr>
										<th>HunterHolic</th>
										<td>
											<asp:checkboxlist id="cblLogType6" runat="server" RepeatColumns="7" CssClass="logSearchTable table-condensed logCheckTable">
                                                <asp:ListItem Value="3500">Create</asp:ListItem>
				                                <asp:ListItem Value="3501">Join</asp:ListItem>
				                                <asp:ListItem Value="3502">Leave</asp:ListItem>
				                                <asp:ListItem Value="3503">Destroy</asp:ListItem>
				                                <asp:ListItem Value="3504">Begin</asp:ListItem>
				                                <asp:ListItem Value="3505">End</asp:ListItem>
				                                <asp:ListItem Value="3506">Quit</asp:ListItem>	
		                                    </asp:checkboxlist>
										</td>
									</tr>
                                    <tr>
                                        <th>Arena</th>
                                        <td>
                                            <asp:CheckBoxList ID="cblLogType10" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable">
                                                <asp:ListItem Value="3900">Win</asp:ListItem>
				                                <asp:ListItem Value="3901">Lose</asp:ListItem>
				                                <asp:ListItem Value="3902">Leave</asp:ListItem>
				                                <asp:ListItem Value="3903">Kick</asp:ListItem>
				                                <asp:ListItem Value="3904">Death</asp:ListItem>
				                                <asp:ListItem Value="3905">AP Gain</asp:ListItem>
				                                <asp:ListItem Value="3906">AP Use</asp:ListItem>
                                                <asp:ListItem Value="3907">Start</asp:ListItem>
                                                <asp:ListItem Value="3908">End</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
									<tr>
										<th>Party</th>
										<td>
											<asp:checkboxlist id="cblLogType7" runat="server" RepeatColumns="7" CssClass="logSearchTable table-condensed logCheckTable">                                                                                                
                                                <asp:ListItem Value="3400">Create</asp:ListItem>
				                                <asp:ListItem Value="3401">Destroy</asp:ListItem>
				                                <asp:ListItem Value="3402">Invite</asp:ListItem>
				                                <asp:ListItem Value="3403">Join</asp:ListItem>
				                                <asp:ListItem Value="3404">Leave</asp:ListItem>
				                                <asp:ListItem Value="3405">Kick</asp:ListItem>
				                                <asp:ListItem Value="3406">Promote</asp:ListItem>			
		                                    </asp:checkboxlist>
										</td>
									</tr>
									<tr>
										<th>Compete</th>
										<td>
											<asp:checkboxlist id="cblLogType8" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable">
				                                <asp:ListItem Value="3700">[Compete] Request</asp:ListItem>
				                                <asp:ListItem Value="3701">[Compete] Answer</asp:ListItem>
				                                <asp:ListItem Value="3702">[Compete] Begin</asp:ListItem>
				                                <asp:ListItem Value="3703">[Compete] End</asp:ListItem>		
		                                    </asp:checkboxlist>
										</td>
									</tr>
									<tr>
										<th>Creature</th>
										<td>
											<asp:CheckBoxList ID="cblLogType5" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable">
							                    <asp:ListItem Value="2601">Creature Lv up</asp:ListItem>
				                                <asp:ListItem Value="2602">Creature Learn Skill</asp:ListItem>
				                                <asp:ListItem Value="2603">Creature Death</asp:ListItem>
				                                <asp:ListItem Value="2604">Creature Evolution</asp:ListItem>
							                    <asp:ListItem Value="3801">Put the creature in</asp:ListItem>
							                    <asp:ListItem Value="3802">Take the creature back</asp:ListItem>
							                    <asp:ListItem Value="3803">Take care of a creature</asp:ListItem>
							                    <asp:ListItem Value="3800">Creature expired</asp:ListItem>
						                    </asp:CheckBoxList>
										</td>
									</tr>
									<tr>
										<th>ETC</th>
										<td>
											<asp:checkboxlist id="cblLogType9" runat="server" RepeatColumns="6" CssClass="logSearchTable table-condensed logCheckTable etcTable">
		                                        <asp:ListItem Value="2201">CHEAT</asp:ListItem>
				                                <asp:ListItem Value="2307">Taming</asp:ListItem>
				                                <asp:ListItem Value="2351">skill lotto</asp:ListItem>
				                                <asp:ListItem Value="2420">ITEM_EXPIRATION</asp:ListItem>
				                                <asp:ListItem Value="2702">NPC_CONTACT</asp:ListItem>
				                                <asp:ListItem Value="2703">NPC_PROCESS</asp:ListItem>
				                                <asp:ListItem Value="2706">STATE_EXPIRATION</asp:ListItem>
				                                <asp:ListItem Value="2801">Quest start</asp:ListItem>
				                                <asp:ListItem Value="2802">Quest give up</asp:ListItem>
				                                <asp:ListItem Value="2803">Quest finish</asp:ListItem>
				                                <asp:ListItem Value="3200">AUTO_USER_CHECKED</asp:ListItem>
				                                <asp:ListItem Value="110">Client Info </asp:ListItem>
				                                <asp:ListItem Value="3600">RANKING SETTLE</asp:ListItem>
				                                <asp:ListItem Value="3601">RANKING TOP RECORD</asp:ListItem>				                                
		                                    </asp:checkboxlist>											
										</td>
									</tr>
								</tbody>
							</table>
							<p class="text-error">* (Search the selection when you pick up "the selection" )</p>
						</td>
					</tr>
				</tbody>
			</table>
			<p>Result : <asp:literal id="ltrTotalCnt" runat="server" Text="0"></asp:literal>  (Max 1000)</p>
			
                    <asp:Repeater ID="rptGameServer" runat="server">
			<HeaderTemplate>
			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:8%" />
					<col style="width:6%" />
					<col style="width:6%" />
					<col style="width:8%" />
					<col style="width:*" />
					<col style="width:6%" />
				</colgroup>
				<thead>
					<tr>
						<th>Date</th>
						<th>Account</th>
						<th>Avatar</th>
						<th>Action</th>
						<th>Msg</th>
						<th>Item SID</th>
					</tr>
				</thead>
				<tbody>
                    <%=NullResult %>
			</HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal ID="ltrDate" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrAccount" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrAvatar" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrAction" runat="server"></asp:Literal></td>
                                <td class="leftAlign"><asp:Literal ID="ltrMsg" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrSID" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>                  
			<FooterTemplate>				
				</tbody>
			</table>
			</FooterTemplate>      
                    </asp:Repeater>	
		</div>
	</div>
</div>
</asp:Content>

