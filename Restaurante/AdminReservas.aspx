<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="AdminReservas.aspx.cs" Inherits="Restaurante.AdminReservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PanelInput" runat="server">
        <asp:Calendar ID="calFecha" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
        <asp:Button ID="btnFecha" runat="server" Text="Enviar" OnClick="btnFecha_Click" />
    </asp:Panel>

    <asp:Panel ID="PanelGrilla" runat="server">
        <asp:GridView ID="GrillaReservas" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="FechaReserva" HeaderText="Fecha" />
                <asp:BoundField DataField="CantPersonas" HeaderText="Cantidad de Personas" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
