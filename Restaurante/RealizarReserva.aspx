﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="RealizarReserva.aspx.cs" Inherits="Restaurante.RealizarReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelInput" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Para ingresar su reserva, complete los campos requeridos:"></asp:Label>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtNombre" ID="RequiredFieldValidator4" runat="server" ForeColor="Tomato" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtNombre" runat="server" ErrorMessage="Nombre Inválido: Ingrese hasta 2 Palabras sin tildes ni puntos." ValidationExpression="^[^\W\d_]* *[^\W\d_]*$"></asp:RegularExpressionValidator>

        <br />
        <asp:Label ID="lblPersonas" runat="server" Text="Cantidad de Personas:"></asp:Label>
        <asp:TextBox ID="txtPersonas" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtPersonas" ID="RequiredFieldValidator1" runat="server" ForeColor="Tomato" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPersonas" runat="server" ErrorMessage="Cantidad Inválida" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
        <asp:Calendar ID="calFecha" runat="server"></asp:Calendar>
        <br />
        <asp:Label ID="lblHora" runat="server" Text="Hora:  "></asp:Label>
        <asp:TextBox ID="txtHoras" runat="server" Width="26px"></asp:TextBox>
        <asp:Label ID="lblSeparador" runat="server" Text=" : "></asp:Label>
        <asp:TextBox ID="txtMinutos" runat="server" Width="31px"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtHoras" ID="RequiredFieldValidator2" runat="server" ForeColor="Tomato" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtHoras" runat="server" ErrorMessage="Corregir hora (12-24h)" ValidationExpression="^([0-9]|0[0-9]|1[0-9]|2[0-3])$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ControlToValidate="txtMinutos" ID="RequiredFieldValidator3" runat="server" ForeColor="Tomato" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtMinutos" runat="server" ErrorMessage="Corregir minutos" ValidationExpression="^[0-5][0-9]$"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lblMesa" runat="server" Text="Mesa:  "></asp:Label>
        <asp:Label ID="lblMesaData" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnMesa" runat="server" Text="Encontrar Mesa" OnClick="btnMesa_Click"/>
        <br />
    </asp:Panel>
    <asp:Panel  ID="PanelConfirmar" runat="server" Visible="false">
        <asp:Label ID="lblMenu" runat="server" Text="Menúes Solicitados:"></asp:Label>
        <asp:DropDownList ID="lstMenu" runat="server"></asp:DropDownList>
        <asp:Button ID="btnAgregarMenu" runat="server" Text="Agregar" OnClick="btnAgregarMenu_Click" />
        <br />
        <asp:GridView ID="grillaMenus" runat="server" AutoGenerateColumns="false" OnRowCommand="grillaMenus_RowCommand" DataKeyNames ="Id">
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" />
                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar"/>
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnReservar" runat="server" Text="Reservar" OnClick="btnReservar_Click" />
    </asp:Panel>
        
</asp:Content>
