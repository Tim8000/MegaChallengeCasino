<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MegaChallengeCasino._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="Image1" runat="server" Height="311px" Width="349px" />
&nbsp;<asp:Image ID="Image2" runat="server" Height="312px" Width="349px" />
&nbsp;<asp:Image ID="Image3" runat="server" Height="312px" Width="349px" />
            <br />
            <br />
            Your Bet :
            <asp:TextBox ID="betValueBox" runat="server" AutoPostBack="True" Width="169px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="rollButton" runat="server" OnClick="rollButton_Click" Text="Pull the leaver" />
&nbsp;<br />
            <br />
        </div>
        <p>
            <asp:Label ID="moneyAmountLabel" runat="server" Text="Label"></asp:Label>
            &nbsp;</p>
        <p>
            <asp:Label ID="moneyLabel" runat="server" Text="Label"></asp:Label>
        </p>
    </form>
    <p>
        1 Cherry - x2 Your Bet</p>
    <p>
        2 Cherries- x3 Your Bet</p>
    <p>
        3 Cherries -x4 Your Bet</p>
    <p>
        3 7&#39;s - Jackpot -x100 Your Bet</p>
    <p>
        &nbsp;</p>
    <p>
        HOWEVER</p>
    <p>
        If there is even one BAR you win nothing</p>
</body>
</html>
