﻿<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="BookListByCategory.aspx.cs" Inherits="EazyLern.BookStore.User.BookListByCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3> Book details by category</h3><br />
   <asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label><br /> 	

	<asp:GridView ID="gvBookListByCategoryId" runat="server" 
		PageSize="6" CellPadding="10" AutoGenerateColumns="False"
		ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvBookListByCategoryId_SelectedIndexChanged">
			<PagerStyle HorizontalAlign="Center" />
				<HeaderStyle BackColor="#92B700" />
					<HeaderStyle BackColor="#92B700" ForeColor="White" VerticalAlign="Middle" />
		<Columns>
							<asp:TemplateField HeaderText="Sl No.">
					<ItemTemplate>
						<%#Container.DataItemIndex+1 %>
					</ItemTemplate>
					<HeaderStyle BackColor="#92B700" ForeColor="White" />
				</asp:TemplateField>
			<asp:BoundField DataField="Book Title" HeaderText="Title" />
			<asp:BoundField DataField="Book Author" HeaderText="Author" />
			<asp:BoundField DataField="Book Price" HeaderText="Price" />
			<asp:BoundField DataField="Book Special Price" HeaderText="Special Price" />
			<asp:BoundField DataField="Book Description" HeaderText="Summary" />
							<asp:HyperLinkField Text="Details" DataNavigateUrlFields="Book Id" DataNavigateUrlFormatString="BookDetails.aspx?bookId={0}"/>
		</Columns>
		<EmptyDataTemplate>
			No Books in this category
		</EmptyDataTemplate>
	</asp:GridView>

    <br /><br /><br />
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Home</asp:LinkButton>
</asp:Content>
