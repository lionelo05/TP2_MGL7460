<%@ Page Title="Customer Space" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="customerspace.aspx.cs" Inherits="TemplateTesting.Banque.customerspace" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>


    <h3>Votre historique de transaction.</h3>
    <p>Compte ouvert le: <asp:Label ID="dat" ForeColor="YellowGreen" Font-Bold="true" runat="server" Text=""></asp:Label>.<br/>
        Vous pouvez crediter ou debiter votre compte en ligne.</p><br/>

    <asp:Label ID="erreur" runat="server" Text="" ForeColor="Red"></asp:Label>  

        <div>  

            <asp:DataGrid ID="Grid" runat="server" PageSize="10" AllowPaging="True" DataKeyField="Id" 

AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanged="Grid_PageIndexChanged"  BorderColor="#000066" BorderWidth="1" EditItemStyle-BackColor="#FFCC00">  


                     <Columns>  

                           <asp:BoundColumn ReadOnly="true" HeaderText="Num Trans" DataField="Id">  </asp:BoundColumn>  

                           <asp:BoundColumn ReadOnly="true" HeaderText="Date Transaction" DataField="datetrans">  </asp:BoundColumn>  

                           <asp:BoundColumn ItemStyle-BackColor="WhiteSmoke" HeaderText="Credit" DataField="credit">  </asp:BoundColumn>  

                           <asp:BoundColumn ItemStyle-BackColor="WhiteSmoke" HeaderText="Debit" DataField="debit">  </asp:BoundColumn>  

                           <asp:BoundColumn ItemStyle-BackColor="YellowGreen" DataField="Balance" HeaderText="balance">  </asp:BoundColumn>  

                          

                    </Columns>  

                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />  

                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />  

                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  

                    <AlternatingItemStyle BackColor="White" />  

                    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />  

                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />  

              </asp:DataGrid>  

          <br /> 

            

               <table>  

                       <tr runat="server" id="ajout" visible="true"> <td colspan="6"> 

                           <ul runat="server" class="nav navbar-nav">

                              <li>Ajouter le montant de la transaction et cliquer sur le type d'operation desiré.<br /> <br /> </li>

                            </ul>

                          </td> </tr> 

                   

                       <tr runat="server" id="formajout">  

                           <td>  

                                 <asp:Label ID="Lmontant" runat="server" Text="Montant" CssClass="col-md-2 control-label"></asp:Label>  <br /> 

                                 <asp:TextBox ID="montant" runat="server" CssClass="form-control"></asp:TextBox>  

                            </td>  


                         </tr>  

                     </table>  

                     <asp:Button ID="btncredit" runat="server" Text="Crediter" CssClass="btn btn-default" OnClick="btncredit_Click" />  

                     <asp:Button ID="btndebit" runat="server" Text="Debiter" CssClass="btn btn-default" OnClick="btndebit_Click" />  
                </div>  

                <div>  

                      
              </div>  

           






<br/>
</asp:Content>