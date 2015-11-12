 <%@ Page Title="Accueil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TemplateTesting._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <img src="Logo_UQAM.jpg" width="1050"   alt="Alternate Text" />
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Bienvenue</h2>
            <p>           
                Projet # 2</br>
                Développement d’un logiciel avec contrôle du code source,</br>
                assemblage du logiciel et tests automatiques</br>
                MGL7460 - Automne 2015
            </p>
            <p>
                <a class="btn btn-default" href="">Plus de details... &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Objectif</h2>
            <p>
                L’objectif de ce travail est de mettre en pratique les différents aspects d’une pratique professionelle
                de développement de logiciels vus dans les premiêres semaines du cours : contrôle
                du code source, assemblage, tests unitaires et tests d’acceptation automatiques.            </p>
            <p>
                <a class="btn btn-default" href="">Plus de details... &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Ce qu’il faut faire</h2>
            <p>
                Pour ce travail, vous devez développer un «petit logiciel», dans le langage de votre choix,
                logiciel pour lequel vous devrez avoir des tests automatiques, tant des tests unitaires que
                des tests d’acceptation,....
            </p>
            <p>
                <a class="btn btn-default" href="">Plus de details... &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
