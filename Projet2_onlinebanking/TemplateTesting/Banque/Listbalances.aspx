<%@ Page Title="Administration des clients et comptes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listbalances.aspx.cs" Inherits="TemplateTesting.Banque.Listbalances" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>


    <h3>
        Liste des clients et leurs comptes. 
    </h3>

    <p> Nombre de client inscrit: <asp:Label ID="totalclt" ForeColor="YellowGreen" Font-Bold="true" runat="server" Text=""></asp:Label> client(s).<br />
        Montant total en caisse : <asp:Label ID="total" ForeColor="YellowGreen" Font-Bold="true" runat="server" Text=""></asp:Label>.<br />
        Cliquer sur une ligne pour gérer(modifier ou supprimer) indivulement un client.</p><br/>
    
    <asp:Label ID="erreur" runat="server" Text="" ForeColor="Red"></asp:Label>  

        <div>  

            <asp:DataGrid ID="Grid" runat="server" PageSize="10" AllowPaging="True" DataKeyField="Id" 

AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanged="Grid_PageIndexChanged" OnCancelCommand="Grid_CancelCommand"  

OnDeleteCommand="Grid_DeleteCommand" OnEditCommand="Grid_EditCommand" OnUpdateCommand="Grid_UpdateCommand" BorderColor="#000066" BorderWidth="1" EditItemStyle-BackColor="#FFCC00">  


                     <Columns>  

                           <asp:BoundColumn ReadOnly="true" HeaderText="Numero" DataField="id">  </asp:BoundColumn>  

                           <asp:BoundColumn ReadOnly="true" HeaderText="ID Client" DataField="userid">  </asp:BoundColumn>  

                           <asp:BoundColumn HeaderText="Nom" DataField="F_Name">  </asp:BoundColumn>  

                           <asp:BoundColumn HeaderText="Prenom" DataField="L_Name">  </asp:BoundColumn>  

                           <asp:BoundColumn DataField="email" HeaderText="Email">  </asp:BoundColumn>  

                           <asp:BoundColumn DataField="phone" HeaderText="Telephone"> </asp:BoundColumn>  

                           <asp:BoundColumn DataField="adress" HeaderText="Adresse">  </asp:BoundColumn> 
                          
                           <asp:BoundColumn DataField="birthdate" HeaderText="Date Naissance">  </asp:BoundColumn> 

                           <asp:BoundColumn ReadOnly="true" DataField="dateopen" HeaderText="Date ouverture compte">  </asp:BoundColumn>  

                           <asp:BoundColumn ItemStyle-BackColor="YellowGreen" ReadOnly="true" DataField="balance" HeaderText="Balance($)"> </asp:BoundColumn>  

                           <asp:BoundColumn ItemStyle-BackColor="WhiteSmoke" DataField="credit" HeaderText="Credit($)">  </asp:BoundColumn> 

                           <asp:BoundColumn ItemStyle-BackColor="WhiteSmoke" DataField="debit" HeaderText="Debit($)">  </asp:BoundColumn> 

                           <asp:EditCommandColumn EditText="Edit" CancelText="Cancel" UpdateText="Update" HeaderText="Edit">  </asp:EditCommandColumn>  

                           <asp:ButtonColumn CommandName="Delete" HeaderText="Delete" Text="Delete"> </asp:ButtonColumn>  

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

                              <li>Pour ajouter un nouveau compte, veuillez d'abord ajouter le login et mot de passe à fournir au client en cliquant sur le lien ci-dessous
                                   puis, revenez sur ce formulaire pour entrer les données du client. <br /> 
                                  <b><u>NB:</u> Le formulaire serra activé automatiquement.</b>
                                  <a runat="server" href="~/Account/Register">Ajouter client</a>
                                                                                                                                                        </li>

                            </ul>

                          </td> </tr> 

                   <tr runat="server" id="infoajout" visible="false"> <td colspan="6"> 

                           Veuillez continuer l'inscription du client:<b style="color:darkgreen"> <%=Server.UrlDecode(Request.QueryString["client"]) %> </b>s'il vous plait.

                          </td> </tr> 

                       <tr runat="server" id="formajout">  

                           <td>  

                                 <asp:Label ID="name" runat="server" Text="Nom"></asp:Label>  

                                 <asp:TextBox ID="addname" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>  

                            </td>  

                            <td>  

                                  <asp:Label ID="name2" runat="server" Text="Prenom"></asp:Label>  

                                  <asp:TextBox ID="addlname" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>  

                             </td>  

                             <td>  

                                  <asp:Label ID="email" runat="server" Text="E-mail"></asp:Label>  

                                  <asp:TextBox ID="addemail" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>  

                             </td>  

                              
                             <td>  

                                     <asp:Label ID="phone" runat="server" Text="Telephone"></asp:Label>  

                                     <asp:TextBox ID="addphone" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox> 

                              </td>  

                              <td>  

                                      <asp:Label ID="adresse" runat="server" Text="Adresse"></asp:Label>  

                                      <asp:TextBox ID="addadresse" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>  

                               </td>  

                               <td>  

                                      <asp:Label ID="birthdate" runat="server" Text="Date Naissance"></asp:Label>  

                                      <asp:TextBox ID="addbirthdate" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>  


                               </td> 


                         </tr>  

                     </table>  

                     <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-default"  OnClick="btnsubmit_Click" />  

                     <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default"  OnClick="btnReset_Click" />  

                     <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="btn btn-default" OnClick="btnOk_Click" />  

                </div>  

                <div> 
              </div>  

           






<br/>
</asp:Content>